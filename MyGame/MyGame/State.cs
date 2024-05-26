namespace MyGame;

public class State
{
    public static MapCell[,] Map { get; private set; }
    public static Point Position { get; private set; }
    public static HashSet<Point> Coins { get; private set; }
    public static int Score { get; private set; }
    public static HashSet<Point> Lasers { get; private set; }
    public static bool LasersActive { get; set; }
    public static bool GameOver { get; set; }
    public static Point Exit { get; private set; }
    public static bool LevelComplete { get; private set; }

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

        var lasers = parts[2].Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        Lasers = new HashSet<Point>();
        foreach (var laser in lasers)
        {
            var cords = laser.Split();
            if (cords[1] == "row")
                Lasers.Add(new Point(int.Parse(cords[0]), 0));
            else
                Lasers.Add(new Point(0, int.Parse(cords[0])));
        }

        var exitCords = parts[3].Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).First().Split();
        Exit = new Point(int.Parse(exitCords[0]), int.Parse(exitCords[1]));

        var playerCords = parts[4].Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).First().Split();
        Position = new Point(int.Parse(playerCords[0]), int.Parse(playerCords[1]));

        LevelComplete = false;
        Score = 0;
        GameOver = false;
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
                    if (LasersActive && Lasers.Contains(new Point(0, j)))
                        GameOver = true;
                    if (Exit == currentPoint)
                        LevelComplete = true;
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
                    if (LasersActive && Lasers.Contains(new Point(0, j)))
                        GameOver = true;
                    if (Exit == currentPoint)
                        LevelComplete = true;
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
                    if (LasersActive && Lasers.Contains(new Point(i, 0)))
                        GameOver = true;
                    if (Exit == currentPoint)
                        LevelComplete = true;
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
                    if (LasersActive && Lasers.Contains(new Point(i, 0)))
                        GameOver = true;
                    if (Exit == currentPoint)
                        LevelComplete = true;
                    continue;
                }
                Position = new Point(Position.X, i - 1);
                break;
            }
        }
    }
}
