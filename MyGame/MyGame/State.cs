namespace MyGame;

public class State
{
    public static MapCell[,] Map;
    public static Point Position;
    public static HashSet<Point> Coins;
    public static int Score;

    public State(string fromLines)
    {
        var parts = fromLines.Split('|');
        var map = parts[0].Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        var height = map.Length;
        var width = map.Any() ? map[0].Length : 0;
        Map = new MapCell[height, width];
        for(int i = 0;i < height; i++)
        {
            for(int j = 0;j < width; j++)
            {
                Map[i,j] = map[i][j] == '#' ? MapCell.Wall : MapCell.Empty;
            }
        }
        var coins = parts[1].Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        Coins = new HashSet<Point>();
        foreach (var coin in coins)
        {
            var cords = coin.Split();
            Coins.Add(new Point(int.Parse(cords[0]), int.Parse(cords[1])));
        }
        Position = new Point(1, 1);
        Score = 0;
    }

    public static void MovePlayer(KeyEventArgs e)
    {
        var k = e.KeyCode;
        if (k == Keys.A)
        {
            for (int j = Position.X; j >= 0; j--)
            {
                if (Map[Position.Y, j] == MapCell.Empty)
                {
                    var currentPoint = new Point(Position.Y, j);
                    if (Coins.Contains(currentPoint))
                    {
                        Coins.Remove(currentPoint);
                        Score++;
                    }
                    continue;
                }
                Position = new Point(j + 1, Position.Y);
                break;
            }
        }
        else if (k == Keys.D)
        {
            for (int j = Position.X; j < Map.GetLength(1); j++)
            {
                if (Map[Position.Y, j] == MapCell.Empty)
                {
                    var currentPoint = new Point(Position.Y, j);
                    if (Coins.Contains(currentPoint))
                    {
                        Coins.Remove(currentPoint);
                        Score++;
                    }
                    continue;
                } 
                Position = new Point(j - 1, Position.Y);
                break;
            }
        }
        else if (k == Keys.W)
        {
            for (int i = Position.Y; i >= 0; i--)
            {
                if (Map[i, Position.X] == MapCell.Empty)
                {
                    var currentPoint = new Point(i, Position.X);
                    if (Coins.Contains(currentPoint))
                    {
                        Coins.Remove(currentPoint);
                        Score++;
                    }
                    continue;
                }
                Position = new Point(Position.X, i + 1);
                break;
            }
        }
        else if (k == Keys.S)
        {
            for (int i = Position.Y; i < Map.GetLength(0); i++)
            {
                if (Map[i, Position.X] == MapCell.Empty)
                {
                    var currentPoint = new Point(i, Position.X);
                    if (Coins.Contains(currentPoint))
                    {
                        Coins.Remove(currentPoint);
                        Score++;
                    }
                    continue;
                }
                Position = new Point(Position.X, i - 1);
                break;
            }
        }
    }
}
