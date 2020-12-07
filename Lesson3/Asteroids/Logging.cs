using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Logging
    {
        internal static void Log(string Message)
        {
            using (var sw = new System.IO.StreamWriter("data.log", true))
            {
                Debug.WriteLine(Message);
                sw.WriteLine(Message);
            }
        }
    }
}
