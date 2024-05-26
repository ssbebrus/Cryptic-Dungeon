using System.Configuration;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace MyGame;
class MyForm : Form
{
    static Size clientSize = new Size(1300, 1000);
    static string[] Maps = [States.firstMap, States.secondMap, States.thirdMap];
    static int currentMap;
    static int totalScore;

    public MyForm()
    {
        currentMap = 0;
        totalScore = 0;
        DoubleBuffered = true;
        var hTileSize = clientSize.Height / State.Map.GetLength(1);
        var vTileSize = clientSize.Height / State.Map.GetLength(0);
        var sprites = new Sprites(hTileSize, vTileSize);

        Paint += (sender, args) =>
        {
            args.Graphics.DrawImage(Sprites.Background, 0, 0);
            args.Graphics.TranslateTransform(200, 0);
            if (State.LasersActive)
            {
                foreach (var laser in State.Lasers)
                {
                    if (laser.Y == 0)
                        args.Graphics.DrawImage(sprites.hLaser, 0, vTileSize * laser.X + (vTileSize / 3));
                }
            }
            args.Graphics.TranslateTransform(100, 0);
            for (int i = 0; i < State.Map.GetLength(0); i++)
            {
                for (int j = 0; j < State.Map.GetLength(1); j++)
                {
                    if (State.Map[i, j] == MapCell.Wall)
                        args.Graphics.DrawImage(sprites.Wall, j * hTileSize, i * vTileSize);
                    if (State.Coins.Contains(new Point(i, j)))
                        args.Graphics.DrawImage(sprites.Coin, j * hTileSize, i * vTileSize);
                    if (State.Exit == new Point(i, j))
                        args.Graphics.DrawImage(sprites.Exit, j * hTileSize, i * vTileSize);
                } 
            }     
            args.Graphics.DrawImage(sprites.Hero, State.Position.X * hTileSize, State.Position.Y * vTileSize);
        };

        var timeLabel = new Label()
        {
            Text = "TIME:",
            Size = new Size(250, 70),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            Font = new Font("Comic Sans MS", 40),
            Location = new System.Drawing.Point(20, 20)
        };
        var timeValue = new Label()
        {
            Text = "0",
            Location = new System.Drawing.Point(timeLabel.Left, timeLabel.Bottom),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            Font = new Font("Comic Sans MS", 40),
            Size = new Size(250, 70)
        };
        var scoresLabel = new Label()
        {
            Text = "SCORE:",
            ForeColor = Color.Gold,
            BackColor = Color.Transparent,
            Font = new Font("Comic Sans MS", 40),
            Size = new Size(250, 70),
            Location = new System.Drawing.Point(timeValue.Left, timeValue.Bottom)
        };
        var scores = new Label()
        {
            Text = "0",
            ForeColor = Color.Gold,
            BackColor = Color.Transparent,
            Font = new Font("Comic Sans MS", 40),
            Size = new Size(250, 70),
            Location = new System.Drawing.Point(scoresLabel.Left, scoresLabel.Bottom)
        };
        var instructions = new Label()
        {
            Text = @"Controls:
W-Up
A-Left
S-Down
D-Right",
            BackColor = Color.Transparent,
            ForeColor = Color.Blue,
            Font = new Font("Comic Sans MS", 30),
            Size = new Size(250, 500),
            Location = new System.Drawing.Point(scores.Left, scores.Bottom)
        };


        double time = 0;
        var timer = new System.Windows.Forms.Timer();
        timer.Interval = 100;
        timer.Tick += (sender, args) =>
        {
            time += 0.1;
            if ((int)time % 2 == 1)
                State.LasersActive = true;
            else
                State.LasersActive = false;
            if (State.LasersActive && (State.Lasers.Contains(new Point(State.Position.Y, 0)) 
            || State.Lasers.Contains(new Point(0, State.Position.X))))
                State.GameOver = true;
            if (State.LevelComplete)
            {
                if (currentMap + 1 < Maps.Count())
                {
                    currentMap++;
                    totalScore += State.Score;
                    new State(Maps[currentMap]);
                    hTileSize = clientSize.Height / State.Map.GetLength(1);
                    vTileSize = clientSize.Height / State.Map.GetLength(0);
                    sprites = new Sprites(hTileSize, vTileSize);
                }
                else
                {
                    totalScore += State.Score;
                    timer.Stop();
                    MessageBox.Show($"Вы прошли игру! Ваше время: {timeValue.Text} Вы набрали {totalScore} очков");
                    currentMap = 0;
                    new State(Maps[currentMap]);
                    Close();
                }
                
            }
            if (State.GameOver)
            {
                new State(Maps[currentMap]);
            }

            timeValue.Text = string.Format("{0:0.0}", time);
            scores.Text = State.Score.ToString();
            Invalidate();
        };
        
        Controls.Add(timeLabel);
        Controls.Add(timeValue);
        Controls.Add(scoresLabel);
        Controls.Add(scores);
        Controls.Add(instructions);
        timer.Start();
    }
    protected override void OnKeyDown(KeyEventArgs e)
    {
        State.MovePlayer(e);
        Invalidate();
    }
    static void Main()
    {
        new State(States.firstMap);
        var form = new MenuForm { ClientSize = new Size(1300, 1000) };
        Application.Run(form);
    }
}
