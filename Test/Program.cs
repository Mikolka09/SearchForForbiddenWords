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
            return "";
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

        static void Main(string[] args)
        {
            AddListWord();
            FindDrivers();
            FindFilesWhithForbiddenWords();
            CopyForbiddenFiles();

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
            drives.Add(@"D:\\TEST");
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
            FileInfo[] files;
            try
            { files = dir.GetFiles(); }
            catch { return; }

            foreach (FileInfo item in files)
            {
                if (item.Extension == exp)
                    listTxtFiles.Add(item.FullName);
            }

            DirectoryInfo[] directories = dir.GetDirectories();
            foreach (DirectoryInfo item in directories)
            {
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
                    t = sr.ReadLine();
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
                using (StreamReader sr = new StreamReader(item, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        txt = sr.ReadLine();
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
                                listForbiddenFiles.Add(new ForbiddenFile()
                                { Way = item, Name = new FileInfo(item).Name, Size = new FileInfo(item).Length.ToString() });
                            right = true;
                        }
                        if (right) break;
                    }
                    if (right) break;
                }
            }
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
            string path = "D:\\CopyTest";
            string newpath = "";
            DirectoryInfo dir = Directory.CreateDirectory(path);
            FileInfo fileInfo;
            foreach (var item in listForbiddenFiles)
            {
                fileInfo = new FileInfo(item.Way);
                if (fileInfo.Exists)
                {
                    if (CheckFileDublicate(path, item.Name))
                    {
                        newpath = path + "\\" + item.Name;
                        fileInfo.CopyTo(newpath, true);
                    }
                    else
                    {
                        newpath = path + "\\" + $"{i}" + item.Name;
                        fileInfo.CopyTo(newpath, true);
                        i++;
                    }
                }

            }
            ChangeForbiddenWords(path);
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
            string newPath = "D:\\ChangeTest";
            DirectoryInfo dir = Directory.CreateDirectory(newPath);
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
                        txt = sr.ReadLine();
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
                    string newFullName = path + "\\" + file.Name;
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
    }
}
