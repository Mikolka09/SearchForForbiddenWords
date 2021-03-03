using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace SearchForForbiddenWords
{
    static class Program
    {
        public static int required = 1;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] arg)
        {
            if (arg.Length > 0)
            {
                string path = "D:\\Users\\MIKOLKA\\SearchForForbiddenWords\\SearchForForbiddenWords\\Test\\bin\\Debug\\Test.exe";
                ProcessStartInfo process = new ProcessStartInfo(path);
                Process.Start(process);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                using (Semaphore s = new Semaphore(required, required, "MyApplication"))
                {
                    if (s.WaitOne(TimeSpan.FromSeconds(0.5)))
                        Application.Run(new Form1());
                    else
                        MessageBox.Show("Извините, стоит ограничение на одно открытие окна!", "WARNING",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}

