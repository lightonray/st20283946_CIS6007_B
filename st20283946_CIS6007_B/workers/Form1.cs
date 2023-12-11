using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace workers
{
    public partial class Form1 : Form
    {
        private object lockObject = new object();
        

        private const int circleRadius = 4;
        private const int circleSpacing = 10;

        private int numberOfCircles = 1001;
        private int numberOfWorkers = 100; // 4 // 20 [BASED ON THE NUMBER OF THE WORKERS]
        private int circlePaintingTime = 20;

        private HashSet<int> paintedIndices = new HashSet<int>();

        public Form1()
        {
            InitializeComponent();

            listBox2.Items.Add($"{numberOfCircles}");
            listBox1.Items.Add($"{numberOfWorkers}");
        }


        private void PaintCircle(int workerId, int circleId)
        {
            Color color = GetThreadColor(workerId);

            lock (lockObject)
            {
                paintedIndices.Add(circleId);
            }

            UpdateProgress($"Worker {workerId} is painting Circle {circleId}");

            // Get the number of circles that can fit in rows and columns
            int columns = pictureBox1.Width / (2 * circleRadius + circleSpacing);
            int rows = pictureBox1.Height / (2 * circleRadius + circleSpacing);

            int row = (circleId - 1) / columns;
            int col = (circleId - 1) % columns;

            int x = circleRadius + col * (circleRadius * 2 + circleSpacing);
            int y = circleRadius * 3 + row * (circleRadius * 2 + circleSpacing);

            using (Graphics g = pictureBox1.CreateGraphics())
            {
                DrawCircle(g, x, y, color);
            }


            Thread.Sleep(circlePaintingTime); // Simulate circle painting time
        }

        private void DrawCircle(Graphics g, int x, int y, Color color)
        {
            using (Brush brush = new SolidBrush(color))
            {
                g.FillEllipse(brush, x - circleRadius, y - circleRadius, 2 * circleRadius, 2 * circleRadius);
            }
        }

        private Color GetThreadColor(int workerId)
        {
            Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Pink }; // Add more colors if needed
            return colors[workerId % colors.Length];
        }

         private void UpdateProgress(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    listBoxProgress.Items.Add(message);
                    listBoxProgress.SelectedIndex = listBoxProgress.Items.Count - 1;
                }));
            }
            else
            {
                listBoxProgress.Items.Add(message);
                listBoxProgress.SelectedIndex = listBoxProgress.Items.Count - 1;
            }
        }

        private void UpdateListBox3(string message)
        {
            if (listBox3.InvokeRequired)
            {
                listBox3.Invoke(new Action(() => listBox3.Items.Add(message)));
            }
            else
            {
                listBox3.Items.Add(message);
            }
        }

        private void ParallelPaintCircles(int numberOfCircles, int numberOfWorkers)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < numberOfWorkers; i++)
            {
                int workerStartIndex = i * (numberOfCircles / numberOfWorkers);
                int workerEndIndex = (i == numberOfWorkers - 1) ? numberOfCircles : (i + 1) * (numberOfCircles / numberOfWorkers);

                Thread thread = new Thread(() =>
                {
                    for (int circleId = workerStartIndex; circleId < workerEndIndex; circleId++)
                    {
                        if (!paintedIndices.Contains(circleId))
                        {
                            PaintCircle(Thread.CurrentThread.ManagedThreadId, circleId);
                        }
                    }
                });

                threads.Add(thread);
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join(); // Wait for threads to finish
            }

            stopwatch.Stop();

            UpdateProgress("Circles were painted");
            UpdateProgress("Completed Circles Count: " + paintedIndices.Count);

            UpdateListBox3($"Elapsed Time: {stopwatch.ElapsedMilliseconds} ms");
        }

        private void btnStartPainting_Click_1(object sender, EventArgs e)
        {
            listBoxProgress.Items.Clear();
            UpdateProgress("Start Painting Circles...");

            Task.Run(() => ParallelPaintCircles(numberOfCircles, numberOfWorkers));
        }


    }
}
