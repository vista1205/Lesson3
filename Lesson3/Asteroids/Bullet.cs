using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Bullet : BaseObject
    {
        public static event Action<string> buuletOut;
        public static event Action<string> bulletDest;
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size) { }


        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(new Bitmap(Resources.laserRed011, new Size(Size.Width, Size.Height)), Pos.X, Pos.Y);
        }

        public override void Update()
        {
            Pos.X = Pos.X + 35;
        }
        public bool BullOutScreen()
        {
            if (Pos.X > Game.Width)
            {
                buuletOut?.Invoke($"{DateTime.Now}: Пуля вышла за пределы экрана");
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Destroy()
        {
            bulletDest?.Invoke($"{DateTime.Now}: Пуля уничтожена");
        }
    }
}
