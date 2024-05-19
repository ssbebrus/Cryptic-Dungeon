namespace MyGame;

class Sprites
{
    public Bitmap Wall;
    public Bitmap Coin;
    public static Bitmap Background = new Bitmap(Image.FromFile("img\\back.png"), 1300, 1000);
    public Bitmap hLaser;
    public Bitmap Hero;
    public Sprites(int tileSize)
    {
        var appDir = Environment.CurrentDirectory;
        Wall = new Bitmap(Image.FromFile("img\\wall.png"), tileSize, tileSize);
        Coin = new Bitmap(Image.FromFile("img\\Coin.png"), tileSize, tileSize);
        hLaser = new Bitmap(Image.FromFile("img\\laser.png"), 1000, tileSize / 2);
        Hero = new Bitmap(Image.FromFile("img\\hero.png"), tileSize, tileSize);
    }
}
