using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

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
            return $"Имя файла - {Name}\nПуть к файлу - {Way}\nРазмер файла - {Size} байт\n" +
                $"Количество запрещенных файлов - {CountChange}";
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
            AddListWord();
            FindDrivers();
            FindFilesWhithForbiddenWords();
            CopyForbiddenFiles();
            Print();
            Console.WriteLine();
            Top10();

            Console.ReadKey();
        }

        public static void FindDrivers()
        {
            List<string> drives = new List<string>();
            //DriveInfo[] driveInfo = DriveInfo.GetDrives();
            string exp = ".txt";
            //foreach (DriveInfo item in driveInfo)
            //{
            //    if (item.DriveType == DriveType.Fixed || item.DriveType == DriveType.Removable)
            //        drives.Add(item.RootDirectory.FullName);
            //}
            drives.Add("D:\\");
            drives.Add("C:\\");
            foreach (var item in drives)
            {
                if (Directory.Exists(item))
                {
                    DirectoryInfo info = new DirectoryInfo(item);
                    FindTxtFiles(info, exp);
                }
            }
        }

        public static void FindTxtFiles(DirectoryInfo dir, string exp)
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
                if (item.Extension == exp)
                    listTxtFiles.Add(item.FullName);
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
            using (StreamReader sr = new StreamReader(@"D:\\ForbiddenWords.txt", Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    t = sr.ReadToEnd().ToLower()    ;
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
            string[] text;
            string txt = "";
            bool right = false;
            foreach (var item in listTxtFiles)
            {
                right = false;
                using (StreamReader sr = new StreamReader(item, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        txt = sr.ReadToEnd().ToLower();
                    }
                }
                text = txt.Split();
                foreach (var it in listWords)
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        string st = text[i].Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Replace(":", "")
                            .Replace(";", "").Replace("-", "").Replace("(", "").Replace(")", "");
                        if (st == it.Name)
                        {
                            if (CheckForbiddenFiles(item))
                            {
                                int cnt = CountFile(text);
                                listForbiddenFiles.Add(new ForbiddenFile()
                                {
                                    Way = item,
                                    Name = new FileInfo(item).Name,
                                    Size = new FileInfo(item).Length.ToString(),
                                    CountChange = cnt
                                });
                                right = true;
                            }
                        }
                        if (right) break;
                    }
                    if (right) break;
                }
            }
        }

        public static int CountFile(string[] text)
        {
            int cnt = 0;
            foreach (var it in listWords)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    string st = text[i].Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Replace(":", "")
                        .Replace(";", "").Replace("-", "").Replace("(", "").Replace(")", "");
                    if (st == it.Name)
                        cnt++;
                }
            }
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

        public static void CopyForbiddenFiles()
        {
            int i = 1;
            string newpath = "";
            DirectoryInfo dir = Directory.CreateDirectory(pathCopy);
            FileInfo fileInfo;
            foreach (var item in listForbiddenFiles)
            {
                fileInfo = new FileInfo(item.Way);
                if (fileInfo.Exists)
                {
                    if (CheckFileDublicate(pathCopy, item.Name))
                    {
                        newpath = pathCopy + "\\" + item.Name;
                        fileInfo.CopyTo(newpath, true);
                    }
                    else
                    {
                        newpath = pathCopy + "\\" + $"{i}" + item.Name;
                        fileInfo.CopyTo(newpath, true);
                        i++;
                    }
                }
            }
            ChangeForbiddenWords(pathCopy);
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
            string[] files = Directory.GetFiles(path);
            foreach (var item in files)
            {
                FileInfo file = new FileInfo(item);
                if (file.Exists)
                {
                    string txt = "";
                    string[] text;
                    string change = "*******";
                    using (StreamReader sr = new StreamReader(file.FullName, Encoding.Default))
                    {
                        while (!sr.EndOfStream)
                        {
                            txt = sr.ReadLine().ToLower();;
                        }
                    }
                    text = txt.Split();
                    foreach (var it in listWords)
                    {
                        for (int i = 0; i < text.Length; i++)
                        {
                            string st = text[i].Replace(",", "").Replace(".", "").Replace("!", "").Replace("?", "").Replace(":", "")
                            .Replace(";", "").Replace("-", "").Replace("(", "").Replace(")", "");
                            if (st == it.Name)
                            {
                                text[i] = text[i].Replace($"{st}", $"{change}");
                                it.Count++;

                            }
                        }
                    }
                    string newFullName = pathChange + "\\" + file.Name;
                    using (StreamWriter sw = new StreamWriter(newFullName, false, Encoding.Default))
                    {
                        for (int i = 0; i < text.Length; i++)
                        {
                            sw.Write(text[i] + " ");
                        }
                    }
                }
            }
        }

        public static void Print()
        {
            Console.WriteLine("Отчет по файлам\n");
            foreach (var item in listForbiddenFiles)
            {
                Console.WriteLine(item);
            }
        }

        public static void Top10()
        {
            Console.WriteLine("ТОП 10 рейтинга\n");
            var sortedListInstance = new BindingList<ForbiddenWord>(listWords.OrderByDescending(x => x.Count).ToList());
            foreach (var item in sortedListInstance)
            {
                Console.WriteLine($"{item.Name} = {item.Count}");
            }
        }
    }
}
