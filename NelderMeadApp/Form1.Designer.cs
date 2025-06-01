namespace NelderMeadApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label labelX1;
        private System.Windows.Forms.Label labelY1;
        private System.Windows.Forms.Label labelX2;
        private System.Windows.Forms.Label labelY2;
        private System.Windows.Forms.Label labelX3;
        private System.Windows.Forms.Label labelY3;
        private System.Windows.Forms.Label labelAlpha;
        private System.Windows.Forms.TextBox textBoxAlpha;
        private System.Windows.Forms.Label labelGamma;
        private System.Windows.Forms.TextBox textBoxGamma;
        private System.Windows.Forms.Label labelRho;
        private System.Windows.Forms.TextBox textBoxRho;
        private System.Windows.Forms.Label labelSigma;
        private System.Windows.Forms.TextBox textBoxSigma;
        private System.Windows.Forms.Label labelEps;
        private System.Windows.Forms.Label labelMaxIter;
        private System.Windows.Forms.Label labelFunction;

        private System.Windows.Forms.TextBox textBoxX1;
        private System.Windows.Forms.TextBox textBoxY1;
        private System.Windows.Forms.TextBox textBoxX2;
        private System.Windows.Forms.TextBox textBoxY2;
        private System.Windows.Forms.TextBox textBoxX3;
        private System.Windows.Forms.TextBox textBoxY3;
        private System.Windows.Forms.TextBox textBoxEps;
        private System.Windows.Forms.TextBox textBoxMaxIter;
        private System.Windows.Forms.TextBox textBoxFunction;

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonOptimize;
        private System.Windows.Forms.Button buttonNextStep;
        private System.Windows.Forms.Button buttonPreviousStep;

        private System.Windows.Forms.PictureBox pictureBox1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            labelX1 = new Label();
            labelY1 = new Label();
            labelX2 = new Label();
            labelY2 = new Label();
            labelX3 = new Label();
            labelY3 = new Label();
            labelAlpha = new Label();
            labelGamma = new Label();
            labelRho = new Label();
            labelSigma = new Label();
            labelEps = new Label();
            labelMaxIter = new Label();
            labelFunction = new Label();
            textBoxX1 = new TextBox();
            textBoxY1 = new TextBox();
            textBoxX2 = new TextBox();
            textBoxY2 = new TextBox();
            textBoxX3 = new TextBox();
            textBoxY3 = new TextBox();
            textBoxAlpha = new TextBox();
            textBoxGamma = new TextBox();
            textBoxRho = new TextBox();
            textBoxSigma = new TextBox();
            textBoxEps = new TextBox();
            textBoxMaxIter = new TextBox();
            textBoxFunction = new TextBox();
            buttonStart = new Button();
            buttonOptimize = new Button();
            buttonNextStep = new Button();
            buttonPreviousStep = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // labelX1
            // 
            labelX1.AutoSize = true;
            labelX1.Location = new Point(10, 10);
            labelX1.Name = "labelX1";
            labelX1.Size = new Size(29, 20);
            labelX1.TabIndex = 0;
            labelX1.Text = "X1:";
            // 
            // labelY1
            // 
            labelY1.AutoSize = true;
            labelY1.Location = new Point(120, 10);
            labelY1.Name = "labelY1";
            labelY1.Size = new Size(28, 20);
            labelY1.TabIndex = 2;
            labelY1.Text = "Y1:";
            // 
            // labelX2
            // 
            labelX2.AutoSize = true;
            labelX2.Location = new Point(10, 40);
            labelX2.Name = "labelX2";
            labelX2.Size = new Size(29, 20);
            labelX2.TabIndex = 4;
            labelX2.Text = "X2:";
            // 
            // labelY2
            // 
            labelY2.AutoSize = true;
            labelY2.Location = new Point(120, 40);
            labelY2.Name = "labelY2";
            labelY2.Size = new Size(28, 20);
            labelY2.TabIndex = 6;
            labelY2.Text = "Y2:";
            // 
            // labelX3
            // 
            labelX3.AutoSize = true;
            labelX3.Location = new Point(10, 70);
            labelX3.Name = "labelX3";
            labelX3.Size = new Size(29, 20);
            labelX3.TabIndex = 8;
            labelX3.Text = "X3:";
            // 
            // labelY3
            // 
            labelY3.AutoSize = true;
            labelY3.Location = new Point(120, 70);
            labelY3.Name = "labelY3";
            labelY3.Size = new Size(28, 20);
            labelY3.TabIndex = 10;
            labelY3.Text = "Y3:";
            // 
            // labelAlpha
            // 
            labelAlpha.AutoSize = true;
            labelAlpha.Location = new Point(10, 100);
            labelAlpha.Name = "labelAlpha";
            labelAlpha.Size = new Size(51, 20);
            labelAlpha.TabIndex = 23;
            labelAlpha.Text = "Alpha:";
            // 
            // labelGamma
            // 
            labelGamma.AutoSize = true;
            labelGamma.Location = new Point(120, 100);
            labelGamma.Name = "labelGamma";
            labelGamma.Size = new Size(64, 20);
            labelGamma.TabIndex = 25;
            labelGamma.Text = "Gamma:";
            // 
            // labelRho
            // 
            labelRho.AutoSize = true;
            labelRho.Location = new Point(240, 100);
            labelRho.Name = "labelRho";
            labelRho.Size = new Size(38, 20);
            labelRho.TabIndex = 27;
            labelRho.Text = "Rho:";
            // 
            // labelSigma
            // 
            labelSigma.AutoSize = true;
            labelSigma.Location = new Point(340, 100);
            labelSigma.Name = "labelSigma";
            labelSigma.Size = new Size(54, 20);
            labelSigma.TabIndex = 29;
            labelSigma.Text = "Sigma:";
            // 
            // labelEps
            // 
            labelEps.AutoSize = true;
            labelEps.Location = new Point(240, 44);
            labelEps.Name = "labelEps";
            labelEps.Size = new Size(60, 20);
            labelEps.TabIndex = 12;
            labelEps.Text = "Epsilon:";
            // 
            // labelMaxIter
            // 
            labelMaxIter.AutoSize = true;
            labelMaxIter.Location = new Point(384, 44);
            labelMaxIter.Name = "labelMaxIter";
            labelMaxIter.Size = new Size(66, 20);
            labelMaxIter.TabIndex = 14;
            labelMaxIter.Text = "Max Iter:";
            // 
            // labelFunction
            // 
            labelFunction.AutoSize = true;
            labelFunction.Location = new Point(240, 10);
            labelFunction.Name = "labelFunction";
            labelFunction.Size = new Size(68, 20);
            labelFunction.TabIndex = 16;
            labelFunction.Text = "Function:";
            // 
            // textBoxX1
            // 
            textBoxX1.Location = new Point(40, 7);
            textBoxX1.Name = "textBoxX1";
            textBoxX1.Size = new Size(60, 27);
            textBoxX1.TabIndex = 1;
            // 
            // textBoxY1
            // 
            textBoxY1.Location = new Point(150, 7);
            textBoxY1.Name = "textBoxY1";
            textBoxY1.Size = new Size(60, 27);
            textBoxY1.TabIndex = 3;
            // 
            // textBoxX2
            // 
            textBoxX2.Location = new Point(40, 37);
            textBoxX2.Name = "textBoxX2";
            textBoxX2.Size = new Size(60, 27);
            textBoxX2.TabIndex = 5;
            // 
            // textBoxY2
            // 
            textBoxY2.Location = new Point(150, 37);
            textBoxY2.Name = "textBoxY2";
            textBoxY2.Size = new Size(60, 27);
            textBoxY2.TabIndex = 7;
            // 
            // textBoxX3
            // 
            textBoxX3.Location = new Point(40, 67);
            textBoxX3.Name = "textBoxX3";
            textBoxX3.Size = new Size(60, 27);
            textBoxX3.TabIndex = 9;
            // 
            // textBoxY3
            // 
            textBoxY3.Location = new Point(150, 67);
            textBoxY3.Name = "textBoxY3";
            textBoxY3.Size = new Size(60, 27);
            textBoxY3.TabIndex = 11;
            // 
            // textBoxAlpha
            // 
            textBoxAlpha.Location = new Point(64, 97);
            textBoxAlpha.Name = "textBoxAlpha";
            textBoxAlpha.Size = new Size(50, 27);
            textBoxAlpha.TabIndex = 24;
            textBoxAlpha.Text = "1.0";
            // 
            // textBoxGamma
            // 
            textBoxGamma.Location = new Point(184, 97);
            textBoxGamma.Name = "textBoxGamma";
            textBoxGamma.Size = new Size(50, 27);
            textBoxGamma.TabIndex = 26;
            textBoxGamma.Text = "2.0";
            // 
            // textBoxRho
            // 
            textBoxRho.Location = new Point(284, 97);
            textBoxRho.Name = "textBoxRho";
            textBoxRho.Size = new Size(50, 27);
            textBoxRho.TabIndex = 28;
            textBoxRho.Text = "0.5";
            // 
            // textBoxSigma
            // 
            textBoxSigma.Location = new Point(400, 97);
            textBoxSigma.Name = "textBoxSigma";
            textBoxSigma.Size = new Size(50, 27);
            textBoxSigma.TabIndex = 30;
            textBoxSigma.Text = "0.5";
            // 
            // textBoxEps
            // 
            textBoxEps.Location = new Point(306, 41);
            textBoxEps.Name = "textBoxEps";
            textBoxEps.Size = new Size(60, 27);
            textBoxEps.TabIndex = 13;
            textBoxEps.Text = "0.001";
            // 
            // textBoxMaxIter
            // 
            textBoxMaxIter.Location = new Point(456, 40);
            textBoxMaxIter.Name = "textBoxMaxIter";
            textBoxMaxIter.Size = new Size(60, 27);
            textBoxMaxIter.TabIndex = 15;
            textBoxMaxIter.Text = "200";
            // 
            // textBoxFunction
            // 
            textBoxFunction.Location = new Point(314, 7);
            textBoxFunction.Name = "textBoxFunction";
            textBoxFunction.Size = new Size(360, 27);
            textBoxFunction.TabIndex = 17;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(698, 5);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(75, 31);
            buttonStart.TabIndex = 18;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonOptimize
            // 
            buttonOptimize.Location = new Point(698, 42);
            buttonOptimize.Name = "buttonOptimize";
            buttonOptimize.Size = new Size(75, 30);
            buttonOptimize.TabIndex = 19;
            buttonOptimize.Text = "Optimize";
            buttonOptimize.UseVisualStyleBackColor = true;
            buttonOptimize.Click += buttonOptimize_Click;
            // 
            // buttonNextStep
            // 
            buttonNextStep.Location = new Point(790, 42);
            buttonNextStep.Name = "buttonNextStep";
            buttonNextStep.Size = new Size(75, 30);
            buttonNextStep.TabIndex = 20;
            buttonNextStep.Text = "Next Step";
            buttonNextStep.UseVisualStyleBackColor = true;
            buttonNextStep.Click += buttonNextStep_Click;
            // 
            // buttonPreviousStep
            // 
            buttonPreviousStep.Enabled = true;
            buttonPreviousStep.Location = new Point(790, 5);
            buttonPreviousStep.Name = "buttonPreviousStep";
            buttonPreviousStep.Size = new Size(75, 31);
            buttonPreviousStep.TabIndex = 31;
            buttonPreviousStep.Text = "Previous";
            buttonPreviousStep.Click += buttonPreviousStep_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(10, 153);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(878, 487);
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 127);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 22;
            label1.Text = "Результат";
            // 
            // Form1
            // 
            ClientSize = new Size(900, 650);
            Controls.Add(label1);
            Controls.Add(labelX1);
            Controls.Add(textBoxX1);
            Controls.Add(labelY1);
            Controls.Add(textBoxY1);
            Controls.Add(labelX2);
            Controls.Add(textBoxX2);
            Controls.Add(labelY2);
            Controls.Add(textBoxY2);
            Controls.Add(labelX3);
            Controls.Add(textBoxX3);
            Controls.Add(labelY3);
            Controls.Add(textBoxY3);
            Controls.Add(labelAlpha);
            Controls.Add(textBoxAlpha);
            Controls.Add(labelGamma);
            Controls.Add(textBoxGamma);
            Controls.Add(labelRho);
            Controls.Add(textBoxRho);
            Controls.Add(labelSigma);
            Controls.Add(textBoxSigma);
            Controls.Add(labelEps);
            Controls.Add(textBoxEps);
            Controls.Add(labelMaxIter);
            Controls.Add(textBoxMaxIter);
            Controls.Add(labelFunction);
            Controls.Add(textBoxFunction);
            Controls.Add(buttonStart);
            Controls.Add(buttonOptimize);
            Controls.Add(buttonPreviousStep);
            Controls.Add(buttonNextStep);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Nelder-Mead Optimization";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
    }
}