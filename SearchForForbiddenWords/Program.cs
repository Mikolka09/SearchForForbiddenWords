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
        static void Main()
        {
            Semaphore s = null;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //if (MessageBox.Show("Открыть как гафический интерфейс (ДА) или консольно(НЕТ)", "Сообщение об открытии",
            //           MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //{
                //using (s = new Semaphore(required, required, "MyApplication"))
                //{
                //    if (s.WaitOne(TimeSpan.FromSeconds(0.5)))
#if DEBUG
                        CreateConsole();
#endif
            //    else
            //        MessageBox.Show("Извините, копии приложения открывать запрещено!", "WARNING",
            //            MessageBoxButtons.OK, MessageBoxIcon.Warning);                //}
            //}
            //else
            //{
            using (s = new Semaphore(required, required, "MyApplication"))
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

