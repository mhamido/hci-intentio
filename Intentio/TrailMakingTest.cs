using Intentio.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Intentio
{
    using Timer = System.Windows.Forms.Timer;
    public partial class TrailMakingTest : Form
    {

        private readonly IUser child;
        private Graph graph;
        private readonly Timer timer = new Timer();
        private readonly Timer endTimer = new Timer() { Interval = 5000 };
        private readonly Timer socketTimer = new Timer();

        private readonly Stopwatch stopwatch = new Stopwatch();
        private LabelClassifier labelClassifier;

        private int timesDistracted = 0;
        private int lettersMistake = 0;
        private int numbersMistake = 0;

        private char nextLetter = 'A';
        private int nextNumber = 1;

        private Sigil NextSigil = Sigil.Number;

        private void SwitchSigil()
        {
            Node.Turn = !Node.Turn;

            if (NextSigil == Sigil.Number)
            {
                ++nextNumber;
                NextSigil = Sigil.Letter;
                return;
            }

            ++nextLetter;
            NextSigil = Sigil.Number;
        }

        #region Attention

        public AttentionReport GenerateReport()
        {
            return new AttentionReport(timesDistracted, stopwatch.Elapsed, lettersMistake, numbersMistake);
        }

        private readonly SoundPlayer correctSoundPlayer = new(Resources.ShortCoinBeep);

        protected void OnCorrectConnection()
        {
            correctSoundPlayer.Play();
        }

        private readonly SoundPlayer incorrectSoundPlayer = new(Resources.Dundun);

        protected void OnIncorrectConnection()
        {
            incorrectSoundPlayer.Play();
        }

        private readonly SoundPlayer loseFocusSoundPlayer = new(Resources.Dundunnn);

        protected void OnLoseFocus()
        {
            Interlocked.Increment(ref timesDistracted);
            //++timesDistracted;
            loseFocusSoundPlayer.Play();
        }

        private readonly SoundPlayer completeSoundPlayer = new(Resources.ShortCorrectBeep);
        protected void OnComplete()
        {
            completeSoundPlayer.Play();
            endTimer.Start();
        }

        #endregion
        public TrailMakingTest(IUser child)
        {
            InitializeComponent();
            InitializeSound();
            this.child = child;
            Load += TrailMakingTest_Load;
            timer.Tick += Timer_Tick;
            MouseMove += TrailMakingTest_MouseMove;
            Paint += TrailMakingTest_Paint;
            // TODO: Load user specific settings and
            // generate a graph based on that
            endTimer.Tick += (_, _) =>
            {
                labelClassifier?.Dispose();
                labelClassifier = null;
                Close();
            };

            socketTimer.Tick += SocketTimer_Tick;
            socketTimer.Interval *= 2;

            labelClassifier = null;
            try
            {
                labelClassifier = new LabelClassifier();
            }
            catch (SocketException)
            {
                Console.WriteLine("Couldn't connect to label socket :(");
            }

            socketTimer.Start();
        }

        string msg = "";

        private void SocketTimer_Tick(object sender, EventArgs e)
        {
            //var pos = await Task.Run(() => labelClassifier?.ReceiveAsync());
            //msg = pos;
            //switch (pos)
            //{
            //    case "left":
            //    case "right":
            //        OnLoseFocus();
            //        break;

            //    default:
            //        break;
            //}
        }

        private void TrailMakingTest_Paint(object sender, PaintEventArgs e)
        {
            DrawBuffered(e.Graphics);
        }

        private Bitmap off;

        private void DrawBuffered(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            Draw(g2);
            g.DrawImage(off, 0, 0);
        }

        List<Point> mousePositions = new List<Point>();

        const float Radius = (float)(Node.Diameter) / 2.0f;
        private void TrailMakingTest_MouseMove(object sender, MouseEventArgs e)
        {
            float x = e.X;
            float y = e.Y;

            mousePositions.Add(e.Location);

            foreach (var node in graph.Nodes)
            {
                float dx = x - node.Location.X;
                float dy = y - node.Location.Y;
                float delta = (float)Math.Sqrt(dx * dx + dy * dy);

                if (node.IsDead) continue;

                // Mouse is inside the node
                if (delta <= Radius)
                {
                    switch (NextSigil)
                    {
                        case Sigil.Number:
                            {
                                if (node.Value is int && node.Value == nextNumber)
                                {
                                    OnCorrectConnection();
                                    node.IsDead = true;
                                    SwitchSigil();
                                }
                                else
                                {
                                    if (node.Value is int) ++numbersMistake;
                                    else ++lettersMistake;

                                    OnIncorrectConnection();
                                }
                                break;
                            }
                        case Sigil.Letter:
                            {
                                if (node.Value is char && node.Value == nextLetter)
                                {
                                    OnCorrectConnection();
                                    node.IsDead = true;
                                    SwitchSigil();
                                }
                                else
                                {
                                    if (node.Value is not int) ++numbersMistake;
                                    else ++lettersMistake;

                                    OnIncorrectConnection();
                                }
                                break;
                            }
                    }
                    break;
                }
            }
        }

        private void InitializeSound()
        {
            var sounds = new SoundPlayer[] {
                completeSoundPlayer,
                incorrectSoundPlayer,
                correctSoundPlayer,
                incorrectSoundPlayer,
                loseFocusSoundPlayer
            };

            completeSoundPlayer.Load();
            incorrectSoundPlayer.Load();
            correctSoundPlayer.Load();
            incorrectSoundPlayer.Load();
            incorrectSoundPlayer.Load();

            foreach (var sound in sounds)
            {
                sound.Stream.Position = 0;
            }
        }
        bool finished = false;

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (finished) return;

            bool allDead = true;
            foreach (var node in graph.Nodes)
            {
                if (!node.IsDead)
                {
                    allDead = false;
                    break;
                }
            }

            if (allDead)
            {
                finished = true;
                OnComplete();
            }

            DrawBuffered(CreateGraphics());
        }

        private void TrailMakingTest_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            off = new Bitmap(Width, Height);
            graph = new Graph(12, Width, Height);
            timer.Start();
            stopwatch.Start();
        }

        private void Draw(Graphics g)
        {
            g.Clear(DefaultBackColor);
            g.DrawString(msg, DefaultFont, Brushes.Black, 0, 25);
            foreach (var point in mousePositions)
            {
                const float Size = 5.0f;
                g.FillEllipse(Brushes.SlateGray, point.X - Size / 2, point.Y - Size / 2, Size, Size);
            }

            foreach (var (from, to, visible) in graph.Edges)
            {
                if (visible) g.DrawLine(Pens.Red, from.Location, to.Location);
            }

            foreach (var node in graph.Nodes)
            {
                node.Draw(g);
            }

            g.DrawString(stopwatch.Elapsed.ToString(), SystemFonts.DefaultFont, Brushes.Black, 0, 0);
        }
    }

    public enum Sigil
    {
        Number,
        Letter,
        Shape,
    }

    public record Node(
        PointF Location,
        Sigil Enscryption,
        dynamic Value
    )
    {
        public const int Diameter = 75;
        public bool IsDead = false;
        public static bool Turn = true; // when Turn is true then if Sigil == Number then Green

        public void Draw(Graphics g)
        {
            Brush brush;
            if (Turn)
            {
                brush = (Enscryption == Sigil.Number) ? Brushes.LightGreen : Brushes.LightPink;
            }
            else
            {
                brush = (Enscryption == Sigil.Number) ? Brushes.LightPink : Brushes.LightGreen;
            }

            g.FillEllipse(
                !IsDead ? brush : Brushes.Gray,
                Location.X - Diameter / 2,
                Location.Y - Diameter / 2,
                Diameter,
                Diameter);

            //g.DrawString(
            //    Value.ToString(),
            //    SystemFonts.DefaultFont,
            //    Brushes.Black,
            //    Location.X - SystemFonts.DefaultFont.Size,
            //    Location.Y);

            using var sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(
                Value.ToString(),
                SystemFonts.DefaultFont,
                Brushes.Black,
                Location.X,
                Location.Y,
                sf);
        }

    }

    public class Graph
    {
        private readonly Random random = new();
        public readonly List<Node> Nodes = new();
        public readonly List<(Node, Node, bool)> Edges = new(); // TODO: draw edges when the child completes them

        private int Width;
        private int Height;

        public Graph(
            int vertices,
            int width,
            int height,
            bool includeNumbers = true,
            bool includeLetters = true)
        {
            int nextNumber = 1;
            char nextLetter = 'A';

            Width = width;
            Height = height;

            while (vertices > 0)
            {
                if (includeNumbers)
                {
                    var numberNode = FindPlaceForNode(Sigil.Number, nextNumber++);
                    if (numberNode != null) Nodes.Add(numberNode);
                    vertices--;
                }

                if (includeLetters)
                {
                    var letterNode = FindPlaceForNode(Sigil.Letter, nextLetter == 'Z' ? (nextLetter = 'A') : nextLetter++);
                    if (letterNode != null) Nodes.Add(letterNode);
                    vertices--;
                }
            }
        }
#nullable enable
        private Node? FindPlaceForNode(Sigil Enscryption, dynamic Value)
        {
            // Give it a 100 tries to find a suitable spot, otherwise just fail.
            for (int i = 0; i < 100; i++)
            {
                float x = random.Next(Node.Diameter, Width - Node.Diameter);
                float y = random.Next(Node.Diameter, Height - Node.Diameter);

                bool foundSpot = true;
                foreach (Node existingNode in Nodes)
                {
                    float dx = (x - existingNode.Location.X);
                    float dy = (y - existingNode.Location.Y);
                    float delta = (float)Math.Sqrt(dx * dx + dy * dy); // PERF: maybe remove sqrt?

                    // Nodes overlap, look for another place to put them.
                    if (delta <= 2 * Node.Diameter)
                    {
                        foundSpot = false;
                        break;
                    }
                }

                // If this was reachable, then we found a good spot for it.
                if (foundSpot) return new Node(
                    new PointF(x, y),
                    Enscryption,
                    Value
                    );
            }

            // No space :(
            return null;
        }
#nullable disable
    }

}
