using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        // Settings
        private const int TileSize = 25;

        // DYNAMIC GRID: Calculated based on canvas size
        private int GridWidth;
        private int GridHeight;

        // Variables
        private List<SnakePart> snake = new List<SnakePart>();
        private SnakePart food = new SnakePart();
        private Direction currentDirection = Direction.Right;
        private Direction nextDirection = Direction.Right;
        private GameState currentState = GameState.StartScreen;
        private Random random = new Random();
        private int score = 0;
        private int highScore = 0;

        public Form1()
        {
            InitializeComponent();

            // Fix overlay parent so transparency works
            lblOverlay.Parent = canvas;
            CenterOverlay();
        }

        // 1. INPUT HANDLING (ProcessCmdKey prevents button focus issues)
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (currentState == GameState.Playing)
            {
                switch (keyData)
                {
                    case Keys.Up:
                        if (currentDirection != Direction.Down) nextDirection = Direction.Up;
                        return true;
                    case Keys.Down:
                        if (currentDirection != Direction.Up) nextDirection = Direction.Down;
                        return true;
                    case Keys.Left:
                        if (currentDirection != Direction.Right) nextDirection = Direction.Left;
                        return true;
                    case Keys.Right:
                        if (currentDirection != Direction.Left) nextDirection = Direction.Right;
                        return true;
                    case Keys.P:
                        TogglePause();
                        return true;
                }
            }
            else if (currentState == GameState.GameOver && keyData == Keys.Space)
            {
                StartNewGame();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // 2. REQUIRED PLACEHOLDERS (To satisfy the Designer)
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Logic handled in ProcessCmdKey
        }

        // --- THIS FIXES YOUR ERROR ---
        private void canvas_Click(object sender, EventArgs e)
        {
            // If user clicks the game board, ensure the form has focus so keys work
            this.Focus();
        }

        // 3. UI EVENTS
        private void StartGame_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void TogglePause_Click(object sender, EventArgs e)
        {
            TogglePause();
        }

        // 4. GAME LOGIC
        private void StartNewGame()
        {
            // Snap canvas size to perfect multiple of TileSize
            int remainderW = canvas.Width % TileSize;
            int remainderH = canvas.Height % TileSize;
            canvas.Width -= remainderW;
            canvas.Height -= remainderH;

            // Calculate Grid Dimensions dynamically
            GridWidth = canvas.Width / TileSize;
            GridHeight = canvas.Height / TileSize;

            // Reset Game
            snake.Clear();
            currentDirection = Direction.Right;
            nextDirection = Direction.Right;
            score = 0;
            gameTimer.Interval = 200;

            // Start in middle
            int startX = GridWidth / 2;
            int startY = GridHeight / 2;
            snake.Add(new SnakePart { X = startX, Y = startY });
            snake.Add(new SnakePart { X = startX - 1, Y = startY });
            snake.Add(new SnakePart { X = startX - 2, Y = startY });

            GenerateFood();
            currentState = GameState.Playing;
            lblOverlay.Visible = false;
            gameTimer.Start();
            UpdateUI();

            this.Focus(); // Ensure keys work immediately
        }

        private void GameLoop(object sender, EventArgs e)
        {
            currentDirection = nextDirection;
            SnakePart head = snake[0];
            SnakePart newHead = new SnakePart { X = head.X, Y = head.Y };

            switch (currentDirection)
            {
                case Direction.Up: newHead.Y--; break;
                case Direction.Down: newHead.Y++; break;
                case Direction.Left: newHead.X--; break;
                case Direction.Right: newHead.X++; break;
            }

            if (CheckCollision(newHead))
            {
                GameOver();
                return;
            }

            snake.Insert(0, newHead);

            if (newHead.X == food.X && newHead.Y == food.Y)
            {
                score += 10;
                GenerateFood();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }

            UpdateUI();
        }

        private bool CheckCollision(SnakePart newHead)
        {
            if (newHead.X < 0 || newHead.X >= GridWidth ||
                newHead.Y < 0 || newHead.Y >= GridHeight)
            {
                return true;
            }

            foreach (var part in snake)
            {
                if (part.X == newHead.X && part.Y == newHead.Y) return true;
            }

            return false;
        }

        private void GenerateFood()
        {
            int maxX = GridWidth;
            int maxY = GridHeight;

            while (true)
            {
                food = new SnakePart { X = random.Next(0, maxX), Y = random.Next(0, maxY) };
                bool onSnake = false;
                foreach (var part in snake)
                {
                    if (part.X == food.X && part.Y == food.Y) onSnake = true;
                }
                if (!onSnake) break;
            }
        }

        private void GameOver()
        {
            currentState = GameState.GameOver;
            gameTimer.Stop();
            if (score > highScore) highScore = score;
            lblOverlay.Text = $"Game Over\nScore: {score}\nPress Start or Space";
            lblOverlay.Visible = true;
            CenterOverlay();
            UpdateUI();
        }

        private void TogglePause()
        {
            if (currentState == GameState.Playing)
            {
                currentState = GameState.Paused;
                gameTimer.Stop();
                lblOverlay.Text = "PAUSED";
                lblOverlay.Visible = true;
            }
            else if (currentState == GameState.Paused)
            {
                currentState = GameState.Playing;
                gameTimer.Start();
                lblOverlay.Visible = false;
            }
            CenterOverlay();
        }

        private void UpdateUI()
        {
            lblScore.Text = "Score: " + score;
            lblHighScore.Text = "High Score: " + highScore;
            canvas.Invalidate();
        }

        private void CenterOverlay()
        {
            lblOverlay.Location = new Point(
                (canvas.Width - lblOverlay.Width) / 2,
                (canvas.Height - lblOverlay.Height) / 2
            );
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillEllipse(Brushes.Red, new Rectangle(food.X * TileSize, food.Y * TileSize, TileSize, TileSize));

            for (int i = 0; i < snake.Count; i++)
            {
                Brush b = (i == 0) ? Brushes.LimeGreen : Brushes.Green;
                g.FillRectangle(b, new Rectangle(snake[i].X * TileSize, snake[i].Y * TileSize, TileSize, TileSize));
                g.DrawRectangle(Pens.Black, new Rectangle(snake[i].X * TileSize, snake[i].Y * TileSize, TileSize, TileSize));
            }
        }
    }

    // Helper classes
    public enum GameState { StartScreen, Playing, Paused, GameOver }
    public enum Direction { Up, Down, Left, Right }
    public class SnakePart { public int X { get; set; } public int Y { get; set; } }
}