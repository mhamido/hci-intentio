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
using System.Web.Services.Description;
using System.Windows.Forms;
using TUIO;

namespace Intentio
{
    public class TuioDemo : Form, TuioListener
    {
        private TuioClient client;
        public Dictionary<long, TuioObject> objectList;
        public Dictionary<long, TuioCursor> cursorList;
        public Dictionary<long, TuioBlob> blobList;

        public static int width, height;
        private int window_width = 640;
        private int window_height = 480;
        private int window_left = 0;
        private int window_top = 0;
        private int screen_width = Screen.PrimaryScreen.Bounds.Width;
        private int screen_height = Screen.PrimaryScreen.Bounds.Height;

        private bool fullscreen;
        private bool verbose;

        public TuioDemo(int port)
        {
            verbose = false;
            fullscreen = false;
            width = window_width;
            height = window_height;

            this.ClientSize = new System.Drawing.Size(width, height);
            this.Name = "TuioDemo";
            this.Text = "TuioDemo";

            this.Closing += new CancelEventHandler(Form_Closing);
            this.KeyDown += new KeyEventHandler(Form_KeyDown);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                            ControlStyles.UserPaint |
                            ControlStyles.DoubleBuffer, true);

            objectList = new Dictionary<long, TuioObject>(128);
            cursorList = new Dictionary<long, TuioCursor>(128);
            blobList = new Dictionary<long, TuioBlob>(128);

            client = new TuioClient(port);
            client.addTuioListener(this);

            client.connect();
        }

        private void Form_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            if (e.KeyData == Keys.F1)
            {
                if (fullscreen == false)
                {

                    width = screen_width;
                    height = screen_height;

                    window_left = this.Left;
                    window_top = this.Top;

                    this.FormBorderStyle = FormBorderStyle.None;
                    this.Left = 0;
                    this.Top = 0;
                    this.Width = screen_width;
                    this.Height = screen_height;

                    fullscreen = true;
                }
                else
                {

                    width = window_width;
                    height = window_height;

                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.Left = window_left;
                    this.Top = window_top;
                    this.Width = window_width;
                    this.Height = window_height;

                    fullscreen = false;
                }
            }
            else if (e.KeyData == Keys.Escape)
            {
                this.Close();

            }
            else if (e.KeyData == Keys.V)
            {
                verbose = !verbose;
            }

        }

        private void Form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client.removeTuioListener(this);

            client.disconnect();
            System.Environment.Exit(0);
        }

        public void addTuioObject(TuioObject o)
        {
            lock (objectList)
            {
                objectList.Add(o.SessionID, o);
            }
            if (verbose) Console.WriteLine("add obj " + o.SymbolID + " (" + o.SessionID + ") " + o.X + " " + o.Y + " " + o.Angle);
        }

        public void updateTuioObject(TuioObject o)
        {

            if (verbose) Console.WriteLine("set obj " + o.SymbolID + " " + o.SessionID + " " + o.X + " " + o.Y + " " + o.Angle + " " + o.MotionSpeed + " " + o.RotationSpeed + " " + o.MotionAccel + " " + o.RotationAccel);
        }

        public void removeTuioObject(TuioObject o)
        {
            lock (objectList)
            {
                objectList.Remove(o.SessionID);
            }
            if (verbose) Console.WriteLine("del obj " + o.SymbolID + " (" + o.SessionID + ")");
        }

        public void addTuioCursor(TuioCursor c)
        {
            lock (cursorList)
            {
                cursorList.Add(c.SessionID, c);
            }
            if (verbose) Console.WriteLine("add cur " + c.CursorID + " (" + c.SessionID + ") " + c.X + " " + c.Y);
        }

        public void updateTuioCursor(TuioCursor c)
        {
            if (verbose) Console.WriteLine("set cur " + c.CursorID + " (" + c.SessionID + ") " + c.X + " " + c.Y + " " + c.MotionSpeed + " " + c.MotionAccel);
        }

        public void removeTuioCursor(TuioCursor c)
        {
            lock (cursorList)
            {
                cursorList.Remove(c.SessionID);
            }
            if (verbose) Console.WriteLine("del cur " + c.CursorID + " (" + c.SessionID + ")");
        }

        public void addTuioBlob(TuioBlob b)
        {
            lock (blobList)
            {
                blobList.Add(b.SessionID, b);
            }
            if (verbose) Console.WriteLine("add blb " + b.BlobID + " (" + b.SessionID + ") " + b.X + " " + b.Y + " " + b.Angle + " " + b.Width + " " + b.Height + " " + b.Area);
        }

        public void updateTuioBlob(TuioBlob b)
        {

            if (verbose) Console.WriteLine("set blb " + b.BlobID + " (" + b.SessionID + ") " + b.X + " " + b.Y + " " + b.Angle + " " + b.Width + " " + b.Height + " " + b.Area + " " + b.MotionSpeed + " " + b.RotationSpeed + " " + b.MotionAccel + " " + b.RotationAccel);
        }

        public void removeTuioBlob(TuioBlob b)
        {
            lock (blobList)
            {
                blobList.Remove(b.SessionID);
            }
            if (verbose) Console.WriteLine("del blb " + b.BlobID + " (" + b.SessionID + ")");
        }

        public void refresh(TuioTime frameTime)
        {
            Invalidate();
        }

    }

    public partial class TrailMakingTest : Form
    {
        private Bitmap off;
        private readonly IUser child;
        private Graph graph;
        private readonly Timer timer = new Timer();
        private readonly Stopwatch stopwatch = new Stopwatch();

        private int timesDistracted = 0;

        private int port = 3333;
        TuioDemo app;

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
            DrawDubble(this.CreateGraphics());
        }

        private void TrailMakingTest_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            graph = new Graph(12, Width, Height);
            app = new TuioDemo(port);
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

            Font font = new Font("Arial", 10.0f);
            SolidBrush fntBrush = new SolidBrush(Color.White);
            SolidBrush bgrBrush = new SolidBrush(Color.FromArgb(0, 0, 64));
            SolidBrush curBrush = new SolidBrush(Color.FromArgb(192, 0, 192));
            SolidBrush objBrush = new SolidBrush(Color.FromArgb(64, 0, 0));
            SolidBrush blbBrush = new SolidBrush(Color.FromArgb(64, 64, 64));
            Pen curPen = new Pen(new SolidBrush(Color.Blue), 1);
            int width = 640, height = 480;
            g.FillRectangle(bgrBrush, new Rectangle(0, 0, width, height));

            if (app.cursorList.Count > 0)
            {
                lock (app.cursorList)
                {
                    foreach (TuioCursor tcur in app.cursorList.Values)
                    {
                        List<TuioPoint> path = tcur.Path;
                        TuioPoint current_point = path[0];

                        for (int i = 0; i < path.Count; i++)
                        {
                            TuioPoint next_point = path[i];
                            g.DrawLine(curPen, current_point.getScreenX(width), current_point.getScreenY(height), next_point.getScreenX(width), next_point.getScreenY(height));
                            current_point = next_point;
                        }
                        g.FillEllipse(curBrush, current_point.getScreenX(width) - height / 100, current_point.getScreenY(height) - height / 100, height / 50, height / 50);
                        g.DrawString(tcur.CursorID + "", font, fntBrush, new PointF(tcur.getScreenX(width) - 10, tcur.getScreenY(height) - 10));
                    }
                }
            }
            if (app.objectList.Count > 0)
            {
                lock (app.objectList)
                {
                    foreach (TuioObject tobj in app.objectList.Values)
                    {
                        int ox = tobj.getScreenX(width);
                        int oy = tobj.getScreenY(height);
                        int size = height / 10;

                        g.TranslateTransform(ox, oy);
                        g.RotateTransform((float)(tobj.Angle / Math.PI * 180.0f));
                        g.TranslateTransform(-ox, -oy);

                        g.FillRectangle(objBrush, new Rectangle(ox - size / 2, oy - size / 2, size, size));

                        g.TranslateTransform(ox, oy);
                        g.RotateTransform(-1 * (float)(tobj.Angle / Math.PI * 180.0f));
                        g.TranslateTransform(-ox, -oy);

                        g.DrawString(tobj.SymbolID + "", font, fntBrush, new PointF(ox - 10, oy - 10));
                    }
                }
            }
            if (app.blobList.Count > 0)
            {
                lock (app.blobList)
                {
                    foreach (TuioBlob tblb in app.blobList.Values)
                    {
                        int bx = tblb.getScreenX(width);
                        int by = tblb.getScreenY(height);
                        float bw = tblb.Width * width;
                        float bh = tblb.Height * height;

                        g.TranslateTransform(bx, by);
                        g.RotateTransform((float)(tblb.Angle / Math.PI * 180.0f));
                        g.TranslateTransform(-bx, -by);

                        g.FillEllipse(blbBrush, bx - bw / 2, by - bh / 2, bw, bh);

                        g.TranslateTransform(bx, by);
                        g.RotateTransform(-1 * (float)(tblb.Angle / Math.PI * 180.0f));
                        g.TranslateTransform(-bx, -by);

                        g.DrawString(tblb.BlobID + "", font, fntBrush, new PointF(bx, by));
                    }
                }
            }
        }

        private void DrawDubble(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            Draw(g2);
            g.DrawImage(off, 0, 0);
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
