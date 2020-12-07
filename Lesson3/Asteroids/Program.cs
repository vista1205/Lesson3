using System;
using System.Windows.Forms;

namespace Asteroids
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var form = new Form()
            {
                MinimumSize = new System.Drawing.Size(800, 600),
                MaximumSize = new System.Drawing.Size(800, 600),
                MaximizeBox = false,
                MinimizeBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                Text = "Asteroids"
            };

            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}
