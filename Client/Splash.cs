using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Splash
    {
        public static void start()
        {
            string txt = Splash.getPath() + "\\splash_txt.txt";
            string[] lines = System.IO.File.ReadAllLines(txt);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        public static string getPath()
        {
            return System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }
    }
}
