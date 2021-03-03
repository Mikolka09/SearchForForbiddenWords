using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;

namespace Test
{
    public class ForbiddenFile
    {
        public string Way;
        public string Name;
        public string Size;
        public int CountChange;

        public override string ToString()
        {
            return $"  Имя файла - {Name}\n  Путь к файлу - {Way}\n  Размер файла - {Size} байт\n" +
                $"  Количество запрещенных слов - {CountChange}";
        }
    }

    public class ForbiddenWord
    {
        public string Name;
        public int Count = 0;
    }

    class Program
    {
        public static BindingList<ForbiddenWord> listWords = new BindingList<ForbiddenWord>();
        public static BindingList<string> listTxtFiles = new BindingList<string>();
        public static BindingList<ForbiddenFile> listForbiddenFiles = new BindingList<ForbiddenFile>();
        public static string pathCopy = "D:\\CopyForbiddenFile";
        public static string pathChange = "D:\\ChangeForbiddenFile";
        public static int required = 1;
        public static string words = "";
        public static string path = "";

        static void Main(string[] args)
        {
            Semaphore s = null;
            try
            {
                s = Semaphore.OpenExisting("MyApplication");
            }
            catch (Exception) { }
            if (s == null)
                s = new Semaphore(required, required, "MyApplication");
            if (!s.WaitOne(10))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(20,20);
                Console.WriteLine("Извините, стоит ограничение на одно открытие окна!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                Process.GetCurrentProcess().Kill();
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n\tДОБРО ПОЖАЛОВАТЬ В ПРОГРАММУ ПО ПОИСКУ ЗАПРЕЩЕННЫХ СЛОВ НА КОМПЬЮТЕРЕ\n" +
                                  "\t---------------------------------------------------------------------\n\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\tПодождите, пожалуйста, идет загрузка приложения\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t\t\t");
            for (int i = 0; i < 30; i++)
            {
                Console.Write("#");
                Thread.Sleep(200);
            }
            Console.Clear();
            CapProgram();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\tВведите список запрещенных слов для поиска:\n" +
                              "\t\t1. Ввести в ручную с клавиатуры\n\t\t2. Загрузить список с файла\n");
            Console.Write("\tСделайте выбор: ");
            Console.ForegroundColor = ConsoleColor.White;
            int var = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            CapProgram();
            switch (var)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("\tВведите список запрещенных слов через пробел: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    words = Console.ReadLine();
                    string[] text = words.Split();
                    foreach (var item in text)
                    {
                        listWords.Add(new ForbiddenWord() { Name = item.ToLower() });
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tСлова добавлены в список!");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\tПодождите минутку, идет поиск и составление отчета!");
                    Thread.Sleep(4000);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("\tВведите полный путь к файлу: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    path = Console.ReadLine();
                    AddListWord(path);
                    break;
                default:
                    break;
            }
            StartProgram();
            Console.WriteLine("\nДля выхода нажмите любую клавишу!");
            Console.ReadKey();
        }

        public static void StartProgram()
        {
            ClinerDirectoryforFiles(pathCopy);
            ClinerDirectoryforFiles(pathChange);
            FindDrivers();
            FindFilesWhithForbiddenWords();
            Print();
            SaveReportFile();
        }

        public static void CapProgram()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t\"ПОИСК ЗАПРЕЩЕННЫХ СЛОВ\"\n" +
                                "\t\t ------------------------\n\n");
        }

        public static void ClinerDirectoryforFiles(string pathTest)
        {
            if (Directory.Exists(pathTest))
            {
                DirectoryInfo dir1 = new DirectoryInfo(pathTest);
                FileInfo[] files = dir1.GetFiles();
                if (files.Length > 0)
                {
                    foreach (var item in files)
                    {
                        FileInfo file = new FileInfo(item.FullName);
                        file.Delete();
                    }
                }
            }
        }

        public static void FindDrivers()
        {
            List<string> drives = new List<string>();
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            string[] exp = { ".txt", ".cs", ".h" };
            foreach (DriveInfo item in driveInfo)
            {
                if (item.DriveType == DriveType.Fixed || item.DriveType == DriveType.Removable)
                    drives.Add(item.RootDirectory.FullName);
            }
            foreach (var item in drives)
            {
                if (Directory.Exists(item))
                {
                    DirectoryInfo info = new DirectoryInfo(item);
                    FindTxtFiles(info, exp);
                }
            }
        }

        public static void FindTxtFiles(DirectoryInfo dir, string[] exp)
        {
            string[] notPath = { "Windows", "ProgramData", "Program Files", "Program Files (x86)",
                "$RECYCLE.BIN", "System Volume Information", "Recovery", "Microsoft"};
            FileInfo[] files;
            int res = 0;
            try
            { files = dir.GetFiles(); }
            catch { return; }
            var filtered = files.Where(f => !f.Attributes.HasFlag(FileAttributes.System) && !f.Attributes.HasFlag(FileAttributes.Hidden) &&
                                            !f.Attributes.HasFlag(FileAttributes.Temporary) && !f.Attributes.HasFlag(FileAttributes.ReadOnly));
            foreach (FileInfo item in filtered)
            {
                foreach (var it in exp)
                {
                    if (item.Extension == it)
                        listTxtFiles.Add(item.FullName);
                }
            }
            DirectoryInfo[] directories = dir.GetDirectories();
            foreach (DirectoryInfo item in directories)
            {
                res = 0;
                for (int i = 0; i < notPath.Length; i++)
                {
                    if (item.Name.Contains(notPath[i]))
                        res++;
                }
                if (res == 0)
                    FindTxtFiles(item, exp);
            }
        }

        public static void AddListWord(string path)
        {
            string t = "";
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    t = sr.ReadToEnd().ToLower();
                }
            }
            string[] text = t.Split();
            foreach (var item in text)
            {
                listWords.Add(new ForbiddenWord() { Name = item });
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tСлова загружены и добавлены в список!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\tПодождите минутку, идет поиск и составление отчета!");
            Thread.Sleep(4000);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void FindFilesWhithForbiddenWords()
        {
            string txt = "";
            bool right = false;
            foreach (var item in listTxtFiles)
            {
                right = false;
                using (StreamReader sr = new StreamReader(item, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        txt = sr.ReadToEnd();
                    }
                }
                foreach (var it in listWords)
                {
                    string reg = "\\b" + $"{it.Name}" + "\\b";
                    if (Regex.IsMatch(txt, reg, RegexOptions.IgnoreCase))
                    {
                        if (CheckForbiddenFiles(item))
                        {
                            int cnt = CountWords(txt, reg);
                            listForbiddenFiles.Add(new ForbiddenFile()
                            {
                                Way = item,
                                Name = new FileInfo(item).Name,
                                Size = new FileInfo(item).Length.ToString(),
                                CountChange = cnt
                            });
                            CopyForbiddenFiles(item);
                            right = true;
                        }
                    }
                    if (right) break;
                }
            }
        }

        public static int CountWords(string txt, string reg)
        {
            int cnt = 0;
            foreach (var it in listWords)
            {
                MatchCollection matchs = Regex.Matches(txt, reg, RegexOptions.IgnoreCase);
                cnt += matchs.Count;
            }
            return cnt;
        }

        public static int CountOneWord(string txt, string reg)
        {
            int cnt = 0;
            MatchCollection matchs = Regex.Matches(txt, reg, RegexOptions.IgnoreCase);
            cnt = matchs.Count;
            return cnt;
        }

        public static bool CheckForbiddenFiles(string st)
        {
            foreach (var item in listForbiddenFiles)
            {
                if (item.Way == st)
                    return false;
            }
            return true;
        }

        public static void CopyForbiddenFiles(string item)
        {
            int i = 1;
            string newpath = "";
            string name = new FileInfo(item).Name;
            DirectoryInfo dir = Directory.CreateDirectory(pathCopy);
            FileInfo fileInfo;
            fileInfo = new FileInfo(item);
            if (fileInfo.Exists)
            {
                if (CheckFileDublicate(pathCopy, name))
                {
                    newpath = pathCopy + "\\" + name;
                    try
                    {
                        fileInfo.CopyTo(newpath, true);
                    }
                    catch { }
                }
                else
                {
                    newpath = pathCopy + "\\" + $"{i}" + name;
                    fileInfo.CopyTo(newpath, true);
                    i++;
                }
            }
            ChangeForbiddenWords(newpath);
        }

        public static bool CheckFileDublicate(string path, string name)
        {
            int count = 0;
            string[] files = Directory.GetFiles(path);
            if (files.Length != 0)
            {
                foreach (var item in files)
                {
                    string nameFile = new FileInfo(item).Name;
                    if (name != nameFile)
                        count = 0;
                    else
                        count++;
                }
                if (count == 0)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        public static void ChangeForbiddenWords(string path)
        {
            DirectoryInfo dir = Directory.CreateDirectory(pathChange);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                string txt = "";
                string change = "*******";
                using (StreamReader sr = new StreamReader(file.FullName, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        txt = sr.ReadToEnd(); ;
                    }
                }
                foreach (var it in listWords)
                {
                    string reg = "\\b" + $"{it.Name}" + "\\b";
                    it.Count += CountOneWord(txt, reg);
                    txt = Regex.Replace(txt, reg, change, RegexOptions.IgnoreCase);
                }
                string newFullName = pathChange + "\\" + file.Name;
                using (StreamWriter sw = new StreamWriter(newFullName, false, Encoding.Default))
                {
                    sw.WriteLine(txt);
                }
            }
        }

        public static void Print()
        {
            Console.Clear();
            CapProgram();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\tОтчет по найденным файлам\n" +
                              "\t-------------------------");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"  Количество файлов - {listForbiddenFiles.Count}\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var item in listForbiddenFiles)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }
            Top10();
        }

        public static void Top10()
        {
            int i = 1;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ТОП 10 рейтинга слов\n" +
                              "   --------------------");
            var sortedListInstance = new BindingList<ForbiddenWord>(listWords.OrderByDescending(x => x.Count).ToList());
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var item in sortedListInstance)
            {
                Console.WriteLine($"    {i}. {item.Name.PadRight(10)} = {item.Count.ToString().PadRight(4)}");
                i++;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static void SaveReportFile()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\tДля сохранения отчета введите имя файла: ");
            Console.ForegroundColor = ConsoleColor.White;
            string nameFile = Console.ReadLine() + ".txt";
            string pathFile = "D:\\" + nameFile;
            using (StreamWriter sw = new StreamWriter(pathFile, false, Encoding.Default))
            {
                sw.WriteLine("\tОтчет по найденным файлам\n" +
                             "\t-------------------------");
                sw.WriteLine($"  Количество файлов - {listForbiddenFiles.Count}\n");
                foreach (var item in listForbiddenFiles)
                {
                    sw.WriteLine(item);
                    sw.WriteLine();
                }
                sw.WriteLine("\n");
                int i = 1;
                sw.WriteLine("   ТОП 10 рейтинга слов\n" +
                             "   --------------------");
                var sortedListInstance = new BindingList<ForbiddenWord>(listWords.OrderByDescending(x => x.Count).ToList());
                foreach (var item in sortedListInstance)
                {
                    sw.WriteLine($"    {i}. {item.Name.PadRight(10)} = {item.Count.ToString().PadRight(4)}");
                    i++;
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\tОтчет сохранен в файл! Путь к файлу - {pathFile}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
