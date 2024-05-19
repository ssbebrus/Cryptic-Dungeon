using System.Configuration;
using System.Reflection;

namespace MyGame;
class MyForm : Form
{
    Size ClientSize = new Size(1300, 1000);
    public MyForm()
    {
        DoubleBuffered = true;
        var tileSize = ClientSize.Height / State.Map.GetLength(0);
        var sprites = new Sprites(tileSize);
        Paint += (sender, args) =>
        {
            args.Graphics.DrawImage(Sprites.Background, 0, 0);
            args.Graphics.TranslateTransform(300, 0);
            if (State.LasersActive)
            {
                foreach (var laser in State.Lasers)
                {
                    if (laser.Y == 0)
                        args.Graphics.DrawImage(sprites.hLaser, 0, tileSize * laser.X + (tileSize / 3));
                }
            }
            for (int i = 0; i < State.Map.GetLength(0); i++)
            {
                for (int j = 0; j < State.Map.GetLength(1); j++)
                {
                    if (State.Map[i, j] == MapCell.Wall)
                        args.Graphics.DrawImage(sprites.Wall, j * tileSize, i * tileSize);
                    if (State.Coins.Contains(new Point(i, j)))
                        args.Graphics.DrawImage(sprites.Coin, j * tileSize, i * tileSize);
                } 
            }     
            args.Graphics.DrawImage(sprites.Hero, State.Position.X * tileSize, State.Position.Y * tileSize);
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
            if (State.GameOver)
            {
                time = 0;
                new State(States.firstMap);
            }
            timeValue.Text = String.Format("{0:0.0}", time);
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
