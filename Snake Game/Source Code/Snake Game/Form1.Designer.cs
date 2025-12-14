namespace Snake_Game
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            gameTimer = new System.Windows.Forms.Timer(components);
            canvas = new PictureBox();
            lblScore = new Label();
            lblHighScore = new Label();
            btnStart = new Button();
            btnPause = new Button();
            lblOverlay = new Label();
            ((System.ComponentModel.ISupportInitialize)canvas).BeginInit();
            SuspendLayout();
            // 
            // gameTimer
            // 
            gameTimer.Interval = 200;
            gameTimer.Tick += GameLoop;
            // 
            // canvas
            // 
            canvas.BackColor = Color.Black;
            canvas.Location = new Point(165, 112);
            canvas.Margin = new Padding(5, 6, 5, 6);
            canvas.Name = "canvas";
            canvas.Size = new Size(701, 620);
            canvas.TabIndex = 0;
            canvas.TabStop = false;
            canvas.Click += canvas_Click;
            canvas.Paint += Canvas_Paint;
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblScore.Location = new Point(60, 25);
            lblScore.Margin = new Padding(5, 0, 5, 0);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(106, 29);
            lblScore.TabIndex = 1;
            lblScore.Text = "Score: 0";
            // 
            // lblHighScore
            // 
            lblHighScore.AutoSize = true;
            lblHighScore.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHighScore.Location = new Point(333, 25);
            lblHighScore.Margin = new Padding(5, 0, 5, 0);
            lblHighScore.Name = "lblHighScore";
            lblHighScore.Size = new Size(167, 29);
            lblHighScore.TabIndex = 2;
            lblHighScore.Text = "High Score: 0";
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.LightGreen;
            btnStart.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStart.Location = new Point(650, 15);
            btnStart.Margin = new Padding(5, 6, 5, 6);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(167, 58);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += StartGame_Click;
            // 
            // btnPause
            // 
            btnPause.Location = new Point(827, 15);
            btnPause.Margin = new Padding(5, 6, 5, 6);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(100, 58);
            btnPause.TabIndex = 4;
            btnPause.Text = "Pause";
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += TogglePause_Click;
            // 
            // lblOverlay
            // 
            lblOverlay.AutoSize = true;
            lblOverlay.BackColor = Color.Black;
            lblOverlay.Font = new Font("Arial", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOverlay.ForeColor = Color.White;
            lblOverlay.Location = new Point(300, 462);
            lblOverlay.Margin = new Padding(5, 0, 5, 0);
            lblOverlay.Name = "lblOverlay";
            lblOverlay.Size = new Size(300, 37);
            lblOverlay.TabIndex = 5;
            lblOverlay.Text = "Press Start to Play";
            lblOverlay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1099, 1050);
            Controls.Add(lblOverlay);
            Controls.Add(btnPause);
            Controls.Add(btnStart);
            Controls.Add(lblHighScore);
            Controls.Add(lblScore);
            Controls.Add(canvas);
            KeyPreview = true;
            Margin = new Padding(5, 6, 5, 6);
            Name = "Form1";
            Text = "Snake Game";
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)canvas).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblHighScore;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label lblOverlay;
    }
}