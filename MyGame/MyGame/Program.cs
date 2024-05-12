namespace MyGame;

class MyForm : Form
{
    public static int clientSize = 1000;

    public MyForm()
    {
        DoubleBuffered = true;
        var tileSize = MyForm.clientSize / State.Map.GetLength(0);
        Paint += (sender, args) =>
        {
            for (int i = 0; i < State.Map.GetLength(0); i++)
            {
                for (int j = 0; j < State.Map.GetLength(1); j++)
                {
                    args.Graphics.FillRectangle(State.Map[i, j] == MapCell.Wall ? Brushes.LightCoral : Brushes.Black, j * tileSize, i * tileSize, tileSize, tileSize);
                    if (State.Coins.Contains(new Point(i, j)))
                        args.Graphics.FillEllipse(Brushes.Gold, j * tileSize + tileSize / 4, i * tileSize + tileSize / 4, tileSize / 2, tileSize / 2);
                }
            }
            args.Graphics.FillEllipse(Brushes.Blue, State.Position.X * tileSize, State.Position.Y * tileSize, tileSize, tileSize);
        };
    }
    protected override void OnKeyDown(KeyEventArgs e)
    {
        State.MovePlayer(e);
        Invalidate();
    }
    static void Main()
    {
        var gstate = new State(States.firstMap);
        Application.Run(new MyForm { ClientSize = new Size(clientSize, clientSize) });
    }
}
