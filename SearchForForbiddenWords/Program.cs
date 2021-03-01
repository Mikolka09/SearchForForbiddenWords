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
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if DEBUG
            //CreateConsole();
#endif
            using (Semaphore s = new Semaphore(1, 1, "MyApplication"))
            {
                if (s.WaitOne(TimeSpan.FromSeconds(0.5)))
                    Application.Run(new Form1());
                else
                    MessageBox.Show("Извините, копии приложения открывать запрещено!", "WARNING", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        static void CreateConsole()
        {
            var t = new Thread(() =>
            {
                AllocConsole();
                for (; ; )
                {
                    var cmd = Console.ReadLine();
                    if (cmd.ToLower() == "quit") break;
                    if (cmd.ToLower() == "start")
                    {
                        string path = "D:\\Users\\MIKOLKA\\SearchForForbiddenWords\\SearchForForbiddenWords\\Test\\bin\\Debug\\Test.exe";
                        ProcessStartInfo process = new ProcessStartInfo(path);
                        Process p = Process.Start(process);
                    }
                }
                FreeConsole();
            });
            t.IsBackground = true;
            t.Start();
        }
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
    }
}

