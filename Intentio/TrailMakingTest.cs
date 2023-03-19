using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intentio
{
    public partial class TrailMakingTest : Form
    {
        private readonly IUser child;
        private Graph graph;
        private readonly Timer timer = new Timer();
        private readonly Stopwatch stopwatch = new Stopwatch();

        private int timesDistracted = 0;

        public TrailMakingTest(IUser child)
        {
            InitializeComponent();
            this.child = child;
            Load += TrailMakingTest_Load;
            timer.Tick += Timer_Tick;
            // TODO: Load user specific settings and
            // generate a graph based on that
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Draw(CreateGraphics());
        }

        private void TrailMakingTest_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            graph = new Graph(12, Width, Height);
            timer.Start();
            stopwatch.Start();
        }

        private void Draw(Graphics g)
        {
            g.Clear(DefaultBackColor);

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
        dynamic Value,
        Brush Brush
    )
    {
        public const int Diameter = 75;

        public void Draw(Graphics g)
        {
            g.FillEllipse(
                Brush,
                Location.X - Diameter / 2,
                Location.Y - Diameter / 2,
                Diameter,
                Diameter);

            g.DrawString(
                Value.ToString(),
                SystemFonts.DefaultFont,
                Brushes.Black,
                Location.X,
                Location.Y);
        }

    }

    public class Graph
    {
        private readonly Random random = new(Seed: 0);

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
                    var numberNode = FindPlaceForNode(Sigil.Number, nextNumber++, Brushes.LightPink);
                    if (numberNode != null) Nodes.Add(numberNode);
                    vertices--;
                }

                if (includeLetters)
                {
                    var letterNode = FindPlaceForNode(Sigil.Letter, nextLetter == 'Z' ? (nextLetter = 'A') : nextLetter++, Brushes.LightGreen);
                    if (letterNode != null) Nodes.Add(letterNode);
                    vertices--;
                }
            }
        }
#nullable enable
        private Node? FindPlaceForNode(Sigil Enscryption, dynamic Value, Brush Brush)
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
                    float dy = (x - existingNode.Location.Y);
                    float delta = (float)Math.Sqrt(dx * dx + dy * dy); // PERF: maybe remove sqrt?

                    // Nodes overlap, look for another place to put them.
                    if (delta <= 2 * Node.Diameter)
                    {
                        foundSpot = false;
                        break;
                    }
                }

                // If this was reachable, then we found a good spot for it.
                if (foundSpot) return new Node(new PointF(x, y), Enscryption, Value, Brush);
            }

            // No space :(
            return null;
        }
#nullable disable
    }

}
