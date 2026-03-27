namespace MyShape
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            canvas = new PictureBox();
            panel1 = new Panel();
            btnSelectColor = new Button();
            btnClearCanvas = new Button();
            btnTriangle = new Button();
            btnElipse = new Button();
            btnSquare = new Button();
            btnCircle = new Button();
            btnRectangle = new Button();
            btnLine = new Button();
            colorDialog = new ColorDialog();
            ((System.ComponentModel.ISupportInitialize)canvas).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // canvas
            // 
            canvas.Anchor = AnchorStyles.Bottom;
            canvas.BackColor = Color.White;
            canvas.Location = new Point(41, 141);
            canvas.Name = "canvas";
            canvas.Size = new Size(1000, 600);
            canvas.TabIndex = 0;
            canvas.TabStop = false;
            canvas.Paint += canvas_Paint;
            canvas.MouseDown += canvas_MouseDown;
            canvas.MouseMove += canvas_MouseMove;
            canvas.MouseUp += canvas_MouseUp;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Pink;
            panel1.Controls.Add(btnSelectColor);
            panel1.Controls.Add(btnClearCanvas);
            panel1.Controls.Add(btnTriangle);
            panel1.Controls.Add(btnElipse);
            panel1.Controls.Add(btnSquare);
            panel1.Controls.Add(btnCircle);
            panel1.Controls.Add(btnRectangle);
            panel1.Controls.Add(btnLine);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1082, 125);
            panel1.TabIndex = 1;
            // 
            // btnSelectColor
            // 
            btnSelectColor.BackColor = Color.Orchid;
            btnSelectColor.FlatStyle = FlatStyle.Flat;
            btnSelectColor.Font = new Font("Forte", 16F);
            btnSelectColor.Location = new Point(941, 42);
            btnSelectColor.Name = "btnSelectColor";
            btnSelectColor.Size = new Size(100, 45);
            btnSelectColor.TabIndex = 7;
            btnSelectColor.Text = "Color";
            btnSelectColor.UseVisualStyleBackColor = false;
            btnSelectColor.Click += btnSelectColor_Click;
            // 
            // btnClearCanvas
            // 
            btnClearCanvas.BackColor = Color.Orchid;
            btnClearCanvas.FlatStyle = FlatStyle.Flat;
            btnClearCanvas.Font = new Font("Forte", 16F);
            btnClearCanvas.Location = new Point(791, 42);
            btnClearCanvas.Name = "btnClearCanvas";
            btnClearCanvas.Size = new Size(100, 45);
            btnClearCanvas.TabIndex = 6;
            btnClearCanvas.Text = "Clear";
            btnClearCanvas.UseVisualStyleBackColor = false;
            btnClearCanvas.Click += btnClearCanvas_Click;
            // 
            // btnTriangle
            // 
            btnTriangle.BackColor = Color.LightSkyBlue;
            btnTriangle.Image = Properties.Resources.triangle;
            btnTriangle.Location = new Point(440, 27);
            btnTriangle.Name = "btnTriangle";
            btnTriangle.Size = new Size(60, 60);
            btnTriangle.TabIndex = 5;
            btnTriangle.UseVisualStyleBackColor = false;
            btnTriangle.Click += OnShapeButtonClick;
            // 
            // btnElipse
            // 
            btnElipse.BackColor = Color.LightSkyBlue;
            btnElipse.Image = Properties.Resources.elipse;
            btnElipse.Location = new Point(541, 27);
            btnElipse.Name = "btnElipse";
            btnElipse.Size = new Size(60, 60);
            btnElipse.TabIndex = 4;
            btnElipse.UseVisualStyleBackColor = false;
            btnElipse.Click += OnShapeButtonClick;
            // 
            // btnSquare
            // 
            btnSquare.BackColor = Color.LightSkyBlue;
            btnSquare.Image = Properties.Resources.square;
            btnSquare.Location = new Point(335, 27);
            btnSquare.Name = "btnSquare";
            btnSquare.Size = new Size(60, 60);
            btnSquare.TabIndex = 3;
            btnSquare.UseVisualStyleBackColor = false;
            btnSquare.Click += OnShapeButtonClick;
            // 
            // btnCircle
            // 
            btnCircle.BackColor = Color.LightSkyBlue;
            btnCircle.ForeColor = Color.White;
            btnCircle.Image = Properties.Resources.circle;
            btnCircle.Location = new Point(233, 27);
            btnCircle.Name = "btnCircle";
            btnCircle.Size = new Size(60, 60);
            btnCircle.TabIndex = 2;
            btnCircle.UseVisualStyleBackColor = false;
            btnCircle.Click += OnShapeButtonClick;
            // 
            // btnRectangle
            // 
            btnRectangle.BackColor = Color.LightSkyBlue;
            btnRectangle.Image = Properties.Resources.rectangle;
            btnRectangle.Location = new Point(131, 27);
            btnRectangle.Name = "btnRectangle";
            btnRectangle.Size = new Size(60, 60);
            btnRectangle.TabIndex = 1;
            btnRectangle.UseVisualStyleBackColor = false;
            btnRectangle.Click += OnShapeButtonClick;
            // 
            // btnLine
            // 
            btnLine.BackColor = Color.LightSkyBlue;
            btnLine.Image = Properties.Resources.diagonal_line;
            btnLine.Location = new Point(41, 27);
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(60, 60);
            btnLine.TabIndex = 0;
            btnLine.UseVisualStyleBackColor = false;
            btnLine.Click += OnShapeButtonClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Thistle;
            ClientSize = new Size(1082, 753);
            Controls.Add(panel1);
            Controls.Add(canvas);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ООТПиСП ЛБ2 451003 Харкевич";
            ((System.ComponentModel.ISupportInitialize)canvas).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox canvas;
        private Panel panel1;
        private Button btnElipse;
        private Button btnSquare;
        private Button btnCircle;
        private Button btnRectangle;
        private Button btnLine;
        private Button btnTriangle;
        private Button btnClearCanvas;
        private Button btnSelectColor;
        private ColorDialog colorDialog;
    }
}