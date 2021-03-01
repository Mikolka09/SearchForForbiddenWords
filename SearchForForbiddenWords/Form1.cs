﻿using System;
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
        public object obj = new object();
        public Thread thread;
        public Thread thread1;
        public Thread thread2;

        public Form1()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {

            buttonStart.Enabled = false;
            buttonAddWord.Enabled = false;
            buttonPresStop.Enabled = false;
            buttonProcess.Enabled = false;
            buttonStop.Enabled = false;
        }


        void ProgressProsess(object obj)
        {
            ProgressBar bar = (ProgressBar)obj;
            int i = 0;
            while (bar.Value < bar.Maximum)
            {
                i++;
                if (bar.InvokeRequired)
                    bar.Invoke(new Action(() => bar.Increment(i)));
            }

        }

        public void Start()
        {

            buttonStart.Enabled = false;
            buttonPresStop.Enabled = true;
            buttonProcess.Enabled = true;
            buttonStop.Enabled = true;
            thread = new Thread(FindDrivers);
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            thread1 = new Thread(FindFilesWhithForbiddenWords);
            thread1.IsBackground = true;
            thread1.Start();
            thread1.Join();
            progressBarProcess.Step = 3;
            thread2 = new Thread(ProgressProsess);
            thread2.IsBackground = true;
            thread2.Start(progressBarProcess);

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
            drives.Add("D:\\");
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
            CopyForbiddenFiles();

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

        public void Print()
        {
            foreach (var item in listForbiddenFiles)
            {
                ListViewItem it = new ListViewItem(item.Name);
                it.SubItems.Add(item.Way);
                it.SubItems.Add(item.Size);
                it.SubItems.Add(item.CountChange.ToString());
                listViewReport.Items.Add(it);
            }
            Top10();
        }

        public void Top10()
        {
            var sortedListInstance = new BindingList<ForbiddenWord>(listWords.OrderByDescending(x => x.Count).ToList());
            foreach (var item in sortedListInstance)
            {
                ListViewItem it = new ListViewItem(item.Name);
                it.SubItems.Add(item.Count.ToString());
                listViewTop.Items.Add(it);
            }
        }


        private void buttonOpenFile_Click(object sender, EventArgs e)
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

        private void buttonAddWord_Click(object sender, EventArgs e)
        {

            string[] text = textBoxEnterWord.Text.Split();
            foreach (var item in text)
            {
                listWords.Add(new ForbiddenWord() { Name = item.ToLower() });
            }
            if (listWords.Count != 0)
            {
                MessageBox.Show("Запрещенные слова добавлены в список!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxEnterWord.Text = "";
                buttonStart.Enabled = true;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Start();
            Thread.Sleep(2000);
            Print();
        }

        private void textBoxEnterWord_TextChanged(object sender, EventArgs e)
        {
            buttonAddWord.Enabled = true;
            if (textBoxEnterWord.Text == "")
                buttonAddWord.Enabled = false;
        }

        private void buttonPresStop_Click(object sender, EventArgs e)
        {
            if (thread.IsAlive)
                thread.Suspend();
            if (thread1.IsAlive)
                thread1.Suspend();
            if (thread2.IsAlive)
                thread2.Suspend();
            buttonStop.Enabled = false;
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            if (thread.IsAlive)
                thread.Resume();
            if (thread1.IsAlive)
                thread1.Resume();
            if (thread2.IsAlive)
                thread2.Resume();
            buttonStop.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (thread.IsAlive)
                thread.Abort();
            if (thread1.IsAlive)
                thread1.Abort();
            if (thread2.IsAlive)
                thread2.Abort();
            buttonPresStop.Enabled = false;
            buttonProcess.Enabled = false;
            buttonStop.Enabled = false;
            buttonStart.Enabled = true;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
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