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

    public partial class Form1 : Form
    {
        public BindingList<ForbiddenWord> listWords = new BindingList<ForbiddenWord>();
        public BindingList<string> listTxtFiles = new BindingList<string>();
        public BindingList<ForbiddenFile> listForbiddenFiles = new BindingList<ForbiddenFile>();
        public string pathCopy = "D:\\CopyTest";
        public string pathChange = "D:\\ChangeTest";
        public Thread threadBar;
        public object obj = new object();
        public System.Windows.Forms.Timer timer;
        public Thread thread;

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
                        textBoxEnterWord.Text = sr.ReadToEnd();
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thread = new Thread(Start);

            threadBar = new Thread(ProgressBarProcess);
            threadBar.IsBackground = true;
            thread.IsBackground = false;

            buttonStart.Enabled = false;
            buttonAddWord.Enabled = false;

        }

        public void ProgressBarProcess(object obj)
        {
            try
            {
                ProgressBar bar = (ProgressBar)obj;
                
                while (thread != null)
                {
                    
                    if (bar.InvokeRequired)
                        bar.Invoke(new Action(() => bar.PerformStep()));
                    
                }
            }
            finally { }

        }

        public void Start()
        {

            FindDrivers();
            FindFilesWhithForbiddenWords();

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;

            threadBar.Start(progressBarProcess);
            thread.Start();
            thread.Join();
            Print();
            MessageBox.Show("End");
        }

        private void buttonAddWord_Click(object sender, EventArgs e)
        {

            string[] text = textBoxEnterWord.Text.Split();
            foreach (var item in text)
            {
                listWords.Add(new ForbiddenWord() { Name = item });
            }
            if (listWords.Count != 0)
            {
                MessageBox.Show("Запрещенные слова добавлены в список!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxEnterWord.Text = "";
                buttonStart.Enabled = true;
            }
        }

        public void FindDrivers()
        {
            List<string> drives = new List<string>();
            // DriveInfo[] driveInfo = DriveInfo.GetDrives();
            string exp = ".txt";
            //foreach (DriveInfo item in driveInfo)
            //{
            //    if (item.DriveType == DriveType.Fixed || item.DriveType == DriveType.Removable)
            //        drives.Add(item.RootDirectory.FullName);
            //}
            drives.Add(@"C:\\");
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

        public void FindFilesWhithForbiddenWords()
        {
            lock (obj)
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
                            txt = sr.ReadToEnd();
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
                CopyForbiddenFiles();
            }
        }

        public int CountFile(string[] text)
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
            lock (obj)
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

        public void ChangeForbiddenWords(string path)
        {
            lock (obj)
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
                                txt = sr.ReadToEnd();
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
        }

        public void Print()
        {
            Console.WriteLine("Отчет по файлам\n");
            foreach (var item in listForbiddenFiles)
            {
                listBox1.Items.Add(item + "\n");
            }
        }

        private void textBoxEnterWord_TextChanged(object sender, EventArgs e)
        {
            buttonAddWord.Enabled = true;
            if (textBoxEnterWord.Text == "")
                buttonAddWord.Enabled = false;
        }
    }

    public class ForbiddenFile
    {
        public string Way;
        public string Name;
        public string Size;
        public int CountChange;

        public override string ToString()
        {
            return $"Имя файла - {Name} \nПуть к файлу - {Way} \nРазмер файла - {Size} байт \n" +
                 $"Количество запрещенных файлов - {CountChange}";
        }
    }

    public class ForbiddenWord
    {
        public string Name;
        public int Count = 0;
    }

}
