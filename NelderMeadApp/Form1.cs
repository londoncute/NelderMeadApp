using NelderMeadWrapper;
using Parser;
using System.Globalization;

namespace NelderMeadApp
{
    public partial class Form1 : Form
    {
        private IReadOnlyList<SimplexPoint[]> historyOfOptimize;

        private int currentStep = -1;
        private double minX, maxX, minY, maxY;
        private int outCount;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {            
            try
            {
                // Reset the visualization state

                currentStep = -1;
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                buttonNextStep.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during start: " + ex.Message);
            }
        }

        private void buttonOptimize_Click(object sender, EventArgs e)
        {
            try
            {
                var ci = CultureInfo.InvariantCulture;

                // Parse input parameters for initial simplex points and algorithm settings
                double x1 = double.Parse(textBoxX1.Text, ci);
                double y1 = double.Parse(textBoxY1.Text, ci);
                double x2 = double.Parse(textBoxX2.Text, ci);
                double y2 = double.Parse(textBoxY2.Text, ci);
                double x3 = double.Parse(textBoxX3.Text, ci);
                double y3 = double.Parse(textBoxY3.Text, ci);

                var parameters = new NelderMeadParams
                {
                    Alpha = double.Parse(textBoxAlpha.Text, ci),
                    Gamma = double.Parse(textBoxGamma.Text, ci),
                    Rho = double.Parse(textBoxRho.Text, ci),
                    Sigma = double.Parse(textBoxSigma.Text, ci),
                    Step = 1.0,
                    MaxIter = int.Parse(textBoxMaxIter.Text, ci),
                    Eps = double.Parse(textBoxEps.Text, ci)
                };

                string functionInput = textBoxFunction.Text.Trim();
                if (string.IsNullOrEmpty(functionInput))
                {
                    MessageBox.Show("Please enter a function.");
                    return;
                }
                // FunctionPtr func = TestFunction1;

                FunctionPtr func = FunctionParser.ParseFunction(functionInput);
                //NelderMeadParams parameters = NelderMeadParams.Default;
               
                // Считывание начальной точки
                Vector2D initialGuess = new Vector2D(0, 0);

                // Выполнение оптимизации
                var optimizer = new NelderMeadOptimizer();
                optimizer.SetParameters(parameters);
                Vector2D result = optimizer.Optimize(initialGuess, func);

                // тут мы достаем историю 

                historyOfOptimize = optimizer.GetHistory();

                label1.Text = $"Min: ({result.x:F4}, {result.y:F4})";

                if (historyOfOptimize.Count == 0)
                {
                    MessageBox.Show("Optimization did not produce any iterations.");
                    return;
                }

                // Determine bounding box of all simplex points for scaling the drawing
                minX = maxX = historyOfOptimize[0][0].Point.x;
                minY = maxY = historyOfOptimize[0][0].Point.y;

                ComputeBounds();

                // Draw the initial simplex (iteration 0)
                currentStep = 0;
                DrawSimplex(currentStep);

                // Enable Next Step button if there are further iterations
                buttonNextStep.Enabled = (historyOfOptimize.Count > 1);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for points and parameters.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void buttonNextStep_Click(object sender, EventArgs e)
        {

            try
            {
                if (historyOfOptimize.Count == 0)
                    return;
                if (currentStep < historyOfOptimize.Count - 1)
                {
                    currentStep++;
                    DrawSimplex(currentStep);
                }
                if (currentStep >= historyOfOptimize.Count - 1)
                {
                    buttonNextStep.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on next step: " + ex.Message);
            }
        }

        private void buttonPreviousStep_Click(object sender, EventArgs e)
        {
            if (currentStep <= 0)
                return;

            currentStep--;
            DrawSimplex(currentStep);

            buttonNextStep.Enabled = true;

        }

        private static double AdjustRange(ref double min, ref double max, double margin)
        {
            double span = max - min;
            if (span == 0)
            {
                span = 1;
                min -= 0.5;
                max += 0.5;

            }

            double extra = span * margin;
            min -= extra;
            max += extra;

            return max - min;
        }

        private void ComputeBounds()
        {
            const double margin = 0.1;
            double dx = AdjustRange(ref minX, ref maxX, margin);
            double dy = AdjustRange(ref minY, ref maxY, margin);
        }

        private Point MapToPixel(double x, double y, double minX, double maxY, double scaleX, double scaleY)
        {
            int px = 10 + (int)((x - minX) * scaleX);
            int py = 10 + (int)((maxY - y) * scaleY);
            return new Point(px, py);

        }

        private void DrawSimplex(int step)
        {
            if (step < 0 || step >= historyOfOptimize.Count)
                return;

            double x1 = historyOfOptimize[step][0].Point.x, y1 = historyOfOptimize[step][0].Point.y;
            double x2 = historyOfOptimize[step][1].Point.x, y2 = historyOfOptimize[step][1].Point.y;
            double x3 = historyOfOptimize[step][2].Point.x, y3 = historyOfOptimize[step][2].Point.y;
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;

            // Compute scaling factors
            double scaleX = (width - 20) / (maxX - minX);
            double scaleY = (height - 20) / (maxY - minY);

            // Map coordinates to picture box pixels (with 10px margin)
            Point p1 = MapToPixel(x1, y1, minX, maxY, scaleX, scaleY);
            Point p2 = MapToPixel(x2, y2, minX, maxY, scaleX, scaleY);
            Point p3 = MapToPixel(x3, y3, minX, maxY, scaleX, scaleY);
            int px1 = p1.X, py1 = p1.Y;
            int px2 = p2.X, py2 = p2.Y;
            int px3 = p3.X, py3 = p3.Y;

            // Prepare the drawing surface
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(pictureBox1.BackColor);
                g.DrawLine(Pens.Blue, px1, py1, px2, py2);
                g.DrawLine(Pens.Blue, px2, py2, px3, py3);
                g.DrawLine(Pens.Blue, px3, py3, px1, py1);
                int r = 4;
                int graphicsWidth = r * 2;
                int graphicsHeight = graphicsWidth;
                g.FillEllipse(Brushes.Red, px1 - r, py1 - r, graphicsWidth, graphicsHeight);
                g.FillEllipse(Brushes.Red, px2 - r, py2 - r, graphicsWidth, graphicsHeight);
                g.FillEllipse(Brushes.Red, px3 - r, py3 - r, graphicsWidth, graphicsHeight);
            }
            pictureBox1.Image = bmp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}