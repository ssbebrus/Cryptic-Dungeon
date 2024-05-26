namespace MyGame;

class Sprites
{
    public readonly Bitmap Wall;
    public readonly Bitmap Coin;
    public readonly static Bitmap Background = new Bitmap(Image.FromFile("img\\back.png"), 1300, 1000);
    public readonly Bitmap hLaser;
    public readonly Bitmap Hero;
    public readonly Bitmap Exit;
    public Sprites(int hTileSize, int vTileSize)
    {
        Wall = new Bitmap(Image.FromFile("img\\wall.png"), hTileSize, vTileSize);
        Coin = new Bitmap(Image.FromFile("img\\Coin.png"), hTileSize, vTileSize);
        hLaser = new Bitmap(Image.FromFile("img\\laser.png"), 1200, vTileSize / 2);
        Hero = new Bitmap(Image.FromFile("img\\hero.png"), hTileSize, vTileSize);
        Exit = new Bitmap(Image.FromFile("img\\exit.png"), hTileSize, vTileSize);
    }
}
