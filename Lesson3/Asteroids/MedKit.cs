using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class MedKit : BaseObject
    {
        public int Power { get; set; }
        Random rnd = new Random();
        Bitmap image = new Bitmap(Properties.Resources.medkit);
        public MedKit(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = Size.Width / 2;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.Y;
            if (Pos.X < 0 - Size.Width)
            {
                Recreate();
            }
        }
        public void Recreate()
        {
            Pos.X = Game.Width + Size.Width;
            Pos.Y = Convert.ToInt32((rnd.NextDouble() * (0.9 - 0.1) + 0.1) * (double)Game.Height);
        }
    }
}
