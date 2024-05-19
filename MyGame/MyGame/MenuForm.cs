namespace MyGame;

class MenuForm : Form
{
    Size ClientSize = new Size(1300, 1000);
    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.DrawImage(Sprites.Background, 0, 0);
    }
    public MenuForm()
    {
        var gameName = new Label()
        {
            Text = "Cryptic Dungeon",
            Location = new System.Drawing.Point(385, 100),
            Size = new Size(600, 200),
            Font = new Font("Comic Sans MS", 50),
            ForeColor = Color.Gold,
            BackColor = Color.Transparent
        };
        var newGameButton = new Button()
        {
            Text = "New Game",
            Size = new Size(500, 200),
            BackColor = Color.LightCoral,
            ForeColor = Color.White,
            Font = new Font("Comic Sans MS", 50),
            Location = new System.Drawing.Point(400, 300),
        };
        newGameButton.Click += (sender, args) =>
        {
            var gameForm = new MyForm() { ClientSize = ClientSize };
            Hide();
            gameForm.ShowDialog();
            Show();
        };
        var exitGameButton = new Button()
        {
            Text = "Exit",
            Size = new Size(500, 200),
            BackColor = Color.LightCoral,
            ForeColor = Color.White,
            Font = new Font("Comic Sans MS", 50),
            Location = new System.Drawing.Point(400, 600),
        };
        exitGameButton.Click += (sender, args) =>
        {
            Application.Exit();
        };
        Controls.Add(newGameButton);
        Controls.Add(exitGameButton);
        Controls.Add(gameName);
    }
}
