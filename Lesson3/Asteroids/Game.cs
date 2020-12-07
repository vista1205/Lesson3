using Asteroids.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids
{
    static class Game
    {
        static BaseObject[] _asteroids;
        static BaseObject[] _stars;
        static Bullet _bullet;
        private static MedKit[] _medkit;
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(45, 50));
        private static int _score;

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        private static Timer timer;

        public static int Width { get; set; }
        public static int Height { get; set; }
        static Game()
        {
        }
        public static void Init(Form form)
        {          
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();

            timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;

            form.KeyDown += Form_KeyDown;

            Ship.MessageDie += Finish;
        }

        private static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("The End!", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 21), new Point(5, 0), new Size(54, 9));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
            if (e.KeyCode == Keys.Right) _ship.Right();
            if (e.KeyCode == Keys.Left) _ship.Left();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);

            foreach (var obj in _stars)
                obj.Draw();

            Buffer.Graphics.DrawImage(new Bitmap(Resources.planet, new Size(200, 200)), 100, 100);

            foreach (var obj in _asteroids)
                if (obj != null) obj.Draw();

            if (_bullet != null)
                _bullet.Draw();

            if (_ship != null)
                _ship.Draw();

            if (_ship != null)
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            foreach(MedKit obj in _medkit)
            {
                obj?.Draw();
            }
            Buffer.Graphics.DrawString("Скорость: " + _score, SystemFonts.DefaultFont, Brushes.White, 80, 0);

            Buffer.Render();
        }

        public static void Load()
        {
            //_bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(54, 9));

            var random = new Random();
            _asteroids = new Asteroid[15];
            for (int i = 0; i < _asteroids.Length; i++)
            {
                var size = random.Next(10, 40);
                _asteroids[i] = new Asteroid(new Point(600, i * 20), new Point(-i, -i), new Size(size, size));
            }
            _stars = new Star[20];
            for (int i = 0; i < _stars.Length; i++)
                _stars[i] = new Star(new Point(600, i * 40), new Point(-i, -i), new Size(3, 3));
            _medkit = new MedKit[3];
            for(int i = 0; i < 3; i++)
            {
                int size = random.Next(15, 30);
                int widthPosition = Width;
                int heightPosition = Convert.ToInt32(random.NextDouble() * (double)Game.Height);
                int speed = random.Next(-8, -1);
                _medkit[i] = new MedKit(new Point(widthPosition, heightPosition), new Point(-i, -i), new Size(size, size));
            }
        }

        public static void Update()
        {
            //foreach (var asteroid in _asteroids)
            //{
            //    asteroid.Update();
            //    if (asteroid.Collision(_bullet))
            //    {

            //    }
            //}
            for( int i = 0; i < _asteroids.Length; i++)
            {             
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _asteroids[i] = null;
                    _bullet = null;
                    _score++;
                    continue;
                }
                if (!_ship.Collision(_asteroids[i])) continue;
                var rnd = new Random();
                if (_ship != null)
                    _ship.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0)
                    if (_ship != null)
                        _ship.Die();
            }

            foreach (var obj in _stars)
                obj.Update();

            if (_bullet != null)
                _bullet.Update();
            foreach (MedKit obj in _medkit) obj.Update();
            for (int i = 0; i < _medkit.Length; i++)
            {
                if (_ship.Collision(_medkit[i]))
                {
                    _medkit[i].Recreate();
                    _ship.EnergyHigh(_medkit[i].Power);
                    System.Media.SystemSounds.Exclamation.Play();
                }
            }
        }

    }
}
