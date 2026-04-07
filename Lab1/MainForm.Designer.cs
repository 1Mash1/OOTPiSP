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
            btnTriangle = new Button();
            btnElipse = new Button();
            btnSquare = new Button();
            btnCircle = new Button();
            btnRectangle = new Button();
            btnLine = new Button();
            colorDialog = new ColorDialog();
            flowPanel = new FlowLayoutPanel();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            открытьToolStripMenuItem = new ToolStripMenuItem();
            сохранитьToolStripMenuItem = new ToolStripMenuItem();
            очиститьTobtnSelectColorolStripMenuItem = new ToolStripMenuItem();
            плагToolStripMenuItem = new ToolStripMenuItem();
            загрузитьФигуруToolStripMenuItem = new ToolStripMenuItem();
            зашрузитьФункционалToolStripMenuItem = new ToolStripMenuItem();
            btnSelectColor = new ToolStripMenuItem();
            encryptionToolStripMenuItem = new ToolStripMenuItem();
            отключитьToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)canvas).BeginInit();
            flowPanel.SuspendLayout();
            menuStrip1.SuspendLayout();
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
            // btnTriangle
            // 
            btnTriangle.BackColor = Color.LightSkyBlue;
            btnTriangle.Image = Properties.Resources.triangle;
            btnTriangle.Location = new Point(873, 3);
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
            btnElipse.Location = new Point(1005, 3);
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
            btnSquare.Location = new Point(807, 3);
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
            btnCircle.Location = new Point(741, 3);
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
            btnRectangle.Location = new Point(939, 3);
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
            btnLine.Location = new Point(675, 3);
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(60, 60);
            btnLine.TabIndex = 0;
            btnLine.UseVisualStyleBackColor = false;
            btnLine.Click += OnShapeButtonClick;
            // 
            // flowPanel
            // 
            flowPanel.BackColor = Color.Pink;
            flowPanel.Controls.Add(menuStrip1);
            flowPanel.Controls.Add(btnLine);
            flowPanel.Controls.Add(btnCircle);
            flowPanel.Controls.Add(btnSquare);
            flowPanel.Controls.Add(btnTriangle);
            flowPanel.Controls.Add(btnRectangle);
            flowPanel.Controls.Add(btnElipse);
            flowPanel.Dock = DockStyle.Top;
            flowPanel.Location = new Point(0, 0);
            flowPanel.Name = "flowPanel";
            flowPanel.Size = new Size(1082, 125);
            flowPanel.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Orchid;
            menuStrip1.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, очиститьTobtnSelectColorolStripMenuItem, плагToolStripMenuItem, btnSelectColor, encryptionToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(672, 36);
            menuStrip1.TabIndex = 11;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.BackColor = Color.Orchid;
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { открытьToolStripMenuItem, сохранитьToolStripMenuItem });
            файлToolStripMenuItem.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(75, 32);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            открытьToolStripMenuItem.BackColor = Color.Orchid;
            открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            открытьToolStripMenuItem.Size = new Size(198, 32);
            открытьToolStripMenuItem.Text = "Открыть";
            открытьToolStripMenuItem.Click += btnLoad_Click;
            // 
            // сохранитьToolStripMenuItem
            // 
            сохранитьToolStripMenuItem.BackColor = Color.Orchid;
            сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            сохранитьToolStripMenuItem.Size = new Size(198, 32);
            сохранитьToolStripMenuItem.Text = "Сохранить";
            сохранитьToolStripMenuItem.Click += btnSave_Click;
            // 
            // очиститьTobtnSelectColorolStripMenuItem
            // 
            очиститьTobtnSelectColorolStripMenuItem.Name = "очиститьTobtnSelectColorolStripMenuItem";
            очиститьTobtnSelectColorolStripMenuItem.Size = new Size(117, 32);
            очиститьTobtnSelectColorolStripMenuItem.Text = "Очистить";
            очиститьTobtnSelectColorolStripMenuItem.Click += btnClearCanvas_Click;
            // 
            // плагToolStripMenuItem
            // 
            плагToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { загрузитьФигуруToolStripMenuItem, зашрузитьФункционалToolStripMenuItem });
            плагToolStripMenuItem.Name = "плагToolStripMenuItem";
            плагToolStripMenuItem.Size = new Size(100, 32);
            плагToolStripMenuItem.Text = "Плагин";
            // 
            // загрузитьФигуруToolStripMenuItem
            // 
            загрузитьФигуруToolStripMenuItem.BackColor = Color.Orchid;
            загрузитьФигуруToolStripMenuItem.Name = "загрузитьФигуруToolStripMenuItem";
            загрузитьФигуруToolStripMenuItem.Size = new Size(315, 32);
            загрузитьФигуруToolStripMenuItem.Text = "Загрузить фигуру";
            загрузитьФигуруToolStripMenuItem.Click += loadShapeMenuItem_Click;
            // 
            // зашрузитьФункционалToolStripMenuItem
            // 
            зашрузитьФункционалToolStripMenuItem.BackColor = Color.Orchid;
            зашрузитьФункционалToolStripMenuItem.Name = "зашрузитьФункционалToolStripMenuItem";
            зашрузитьФункционалToolStripMenuItem.Size = new Size(315, 32);
            зашрузитьФункционалToolStripMenuItem.Text = "Загрузить функционал";
            зашрузитьФункционалToolStripMenuItem.Click += loadFuncMenuItem_Click;
            // 
            // btnSelectColor
            // 
            btnSelectColor.Name = "btnSelectColor";
            btnSelectColor.Size = new Size(71, 32);
            btnSelectColor.Text = "Цвет";
            btnSelectColor.Click += btnSelectColor_Click;
            // 
            // encryptionToolStripMenuItem
            // 
            encryptionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { отключитьToolStripMenuItem });
            encryptionToolStripMenuItem.Name = "encryptionToolStripMenuItem";
            encryptionToolStripMenuItem.Size = new Size(151, 32);
            encryptionToolStripMenuItem.Text = "Шифрование";
            // 
            // отключитьToolStripMenuItem
            // 
            отключитьToolStripMenuItem.BackColor = Color.Orchid;
            отключитьToolStripMenuItem.Name = "отключитьToolStripMenuItem";
            отключитьToolStripMenuItem.Size = new Size(207, 32);
            отключитьToolStripMenuItem.Text = "Отключить";
            отключитьToolStripMenuItem.Click += отключитьToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Thistle;
            ClientSize = new Size(1082, 753);
            Controls.Add(flowPanel);
            Controls.Add(canvas);
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ООТПиСП ЛБ3 451003 Харкевич";
            ((System.ComponentModel.ISupportInitialize)canvas).EndInit();
            flowPanel.ResumeLayout(false);
            flowPanel.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox canvas;
        private Button btnElipse;
        private Button btnSquare;
        private Button btnCircle;
        private Button btnRectangle;
        private Button btnLine;
        private Button btnTriangle;
        private ColorDialog colorDialog;
        private FlowLayoutPanel flowPanel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private ToolStripMenuItem сохранитьToolStripMenuItem;
        private ToolStripMenuItem очиститьTobtnSelectColorolStripMenuItem;
        private ToolStripMenuItem плагToolStripMenuItem;
        private ToolStripMenuItem btnSelectColor;
        private ToolStripMenuItem encryptionToolStripMenuItem;
        private ToolStripMenuItem отключитьToolStripMenuItem;
        private ToolStripMenuItem загрузитьФигуруToolStripMenuItem;
        private ToolStripMenuItem зашрузитьФункционалToolStripMenuItem;
    }
}