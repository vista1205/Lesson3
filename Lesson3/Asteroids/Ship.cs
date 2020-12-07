using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Ship : BaseObject
    {
        private static int maxEnergy = 100;
        private int _energy = maxEnergy;
        public static event Action<string> shipLow;
        public static event Action<string> shipHigh;
        public static event Action<string> shipDie;
        public static event Message MessageDie;

        public int Energy
        {
            get { return _energy; }
        }       

        public void EnergyLow(int n)
        {
            _energy -= n;
            shipLow?.Invoke($"{DateTime.Now}: Корабль получил повреждение {n}");
        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size) { }


        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(new Bitmap(Resources.ship, new Size(Size.Width, Size.Height)), Pos.X, Pos.Y);
        }

        public override void Update()
        {
        }

        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Right()
        {
            if (Pos.X > 0) Pos.X = Pos.X + Dir.X;
        }
        public void Left()
        {            
            if (Pos.X < Game.Width && Pos.X>0) Pos.X = Pos.X - Dir.X;
        }

        public void Die()
        {
            if (MessageDie != null) 
            {
                shipDie?.Invoke($"{DateTime.Now}: Корабль был уничтожен");
                MessageDie.Invoke(); 
            }
        }
            internal void EnergyHigh(int power)
        {
            if (_energy < maxEnergy)
            {
                if (_energy + power > maxEnergy)
                {
                    _energy = maxEnergy;
                }
                else
                {
                    _energy += power;
                }
                shipHigh?.Invoke($"{DateTime.Now}: Корабль восстановил энергию на {power}");
            }
        }
    }
}
