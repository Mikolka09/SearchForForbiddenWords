using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Globalization;

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
        public static string pathCopy = "D:\\CopyTest";
        public static string pathChange = "D:\\ChangeTest";

        static void Main(string[] args)
        {
            ClinerDirectoryforFiles(pathCopy);
            ClinerDirectoryforFiles(pathChange);
            AddListWord();
            FindDrivers();
            FindFilesWhithForbiddenWords();
            Print();
            Console.WriteLine();
            Top10();

            Console.WriteLine("\nДля выхода нажмите любую клавишу!");
            Console.ReadKey();
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
            //DriveInfo[] driveInfo = DriveInfo.GetDrives();
            string[] exp = { ".txt", ".cs", ".h" };
            //foreach (DriveInfo item in driveInfo)
            //{
            //    if (item.DriveType == DriveType.Fixed || item.DriveType == DriveType.Removable)
            //        drives.Add(item.RootDirectory.FullName);
            //}
            drives.Add("D:\\");
            //drives.Add("C:\\");
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
                "$RECYCLE.BIN", "System Volume Information", "Recovery"};
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

        public static void AddListWord()
        {
            string t = "";
            using (StreamReader sr = new StreamReader(@"D:\\ForbiddenWordsTest.txt", Encoding.Default))
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
            Console.WriteLine("\tОтчет по найденным файлам\n" +
                              "\t-------------------------");
            Console.WriteLine($"  Количество файлов - {listForbiddenFiles.Count}\n");
            foreach (var item in listForbiddenFiles)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }
        }

        public static void Top10()
        {
            int i = 1;
            Console.WriteLine("   ТОП 10 рейтинга слов\n" +
                              "   --------------------");
            var sortedListInstance = new BindingList<ForbiddenWord>(listWords.OrderByDescending(x => x.Count).ToList());
            foreach (var item in sortedListInstance)
            {
                Console.WriteLine($"    {i}. {item.Name.PadRight(10)} = {item.Count.ToString().PadRight(4)}");
                i++;
            }
        }
    }
}
