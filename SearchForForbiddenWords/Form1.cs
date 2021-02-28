using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;


namespace SearchForForbiddenWords
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

    public partial class Form1 : Form
    {
        public BindingList<ForbiddenWord> listWords = new BindingList<ForbiddenWord>();
        public BindingList<string> listTxtFiles = new BindingList<string>();
        public BindingList<ForbiddenFile> listForbiddenFiles = new BindingList<ForbiddenFile>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialogWords.Filter = "Все файлы (*.*)|*.*|Текстовые файлы (*.txt)|*.txt";
            if (openFileDialogWords.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(openFileDialogWords.FileName, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        textBoxEnterWord.Text = sr.ReadLine();
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //FindDrivers();
        }

        private void buttonAddWord_Click(object sender, EventArgs e)
        {
            string[] text = textBoxEnterWord.Text.Split();
            foreach (var item in text)
            {
                listWords.Add(new ForbiddenWord() { Name = item });
            }
            if (listWords.Count != 0)
                MessageBox.Show("Запрещенные слова добавлены в список!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void FindDrivers()
        {
            List<string> drives = new List<string>();
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            string exp = ".txt";
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

        public void FindTxtFiles(DirectoryInfo dir, string exp)
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

        public void FindFilesWhithForbiddenWords()
        {
            string[] text;
            string txt = "";
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
                        if (text[i] == it.Name)
                        {
                            if (CheckForbiddenFiles(item))
                                listForbiddenFiles.Add(new ForbiddenFile()
                                { Way = item, Name = new FileInfo(item).Name, Size = new FileInfo(item).Length.ToString() });
                            break;
                        }
                    }
                }
            }
            CopyForbiddenFiles();

        }

        public bool CheckForbiddenFiles(string st)
        {
            foreach (var item in listForbiddenFiles)
            {
                if (item.Way == st)
                    return false;
            }
            return true;
        }

        public void CopyForbiddenFiles()
        {
            int i = 1;
            string path = "D:\\CopyForbiddenFiles";
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
        }

        public bool CheckFileDublicate(string path, string name)
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

    }
}
