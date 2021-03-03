
namespace SearchForForbiddenWords
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxEnterWord = new System.Windows.Forms.TextBox();
            this.buttonAddWord = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.openFileDialogWords = new System.Windows.Forms.OpenFileDialog();
            this.buttonStart = new System.Windows.Forms.Button();
            this.progressBarProcess = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonPresStop = new System.Windows.Forms.Button();
            this.buttonProcess = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.listViewReport = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonExit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewTop = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.labelCountFile = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSaveReport = new System.Windows.Forms.Button();
            this.saveFileDialogReport = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxEnterWord
            // 
            this.textBoxEnterWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxEnterWord.Location = new System.Drawing.Point(12, 31);
            this.textBoxEnterWord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxEnterWord.Multiline = true;
            this.textBoxEnterWord.Name = "textBoxEnterWord";
            this.textBoxEnterWord.Size = new System.Drawing.Size(233, 88);
            this.textBoxEnterWord.TabIndex = 0;
            this.textBoxEnterWord.TextChanged += new System.EventHandler(this.textBoxEnterWord_TextChanged);
            // 
            // buttonAddWord
            // 
            this.buttonAddWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonAddWord.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddWord.Location = new System.Drawing.Point(151, 127);
            this.buttonAddWord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonAddWord.Name = "buttonAddWord";
            this.buttonAddWord.Size = new System.Drawing.Size(94, 33);
            this.buttonAddWord.TabIndex = 1;
            this.buttonAddWord.Text = "Добавить";
            this.buttonAddWord.UseVisualStyleBackColor = false;
            this.buttonAddWord.Click += new System.EventHandler(this.buttonAddWord_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(23, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "ВВОД ЗАПРЕЩЕННЫХ СЛОВ";
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonOpenFile.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOpenFile.Location = new System.Drawing.Point(12, 128);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(106, 33);
            this.buttonOpenFile.TabIndex = 3;
            this.buttonOpenFile.Text = "Из фийла";
            this.buttonOpenFile.UseVisualStyleBackColor = false;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // openFileDialogWords
            // 
            this.openFileDialogWords.FileName = "openFileDialogWords";
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonStart.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStart.Location = new System.Drawing.Point(584, 509);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(83, 33);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "СТАРТ";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // progressBarProcess
            // 
            this.progressBarProcess.Location = new System.Drawing.Point(12, 462);
            this.progressBarProcess.Name = "progressBarProcess";
            this.progressBarProcess.Size = new System.Drawing.Size(776, 31);
            this.progressBarProcess.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(279, 441);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Процесс работы программы";
            // 
            // buttonPresStop
            // 
            this.buttonPresStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.buttonPresStop.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPresStop.Location = new System.Drawing.Point(69, 42);
            this.buttonPresStop.Name = "buttonPresStop";
            this.buttonPresStop.Size = new System.Drawing.Size(140, 29);
            this.buttonPresStop.TabIndex = 8;
            this.buttonPresStop.Text = "Приостановить";
            this.buttonPresStop.UseVisualStyleBackColor = false;
            this.buttonPresStop.Click += new System.EventHandler(this.buttonPresStop_Click);
            // 
            // buttonProcess
            // 
            this.buttonProcess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonProcess.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonProcess.Location = new System.Drawing.Point(18, 95);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(113, 29);
            this.buttonProcess.TabIndex = 9;
            this.buttonProcess.Text = "Продолжить";
            this.buttonProcess.UseVisualStyleBackColor = false;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonStop.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStop.Location = new System.Drawing.Point(149, 95);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(108, 28);
            this.buttonStop.TabIndex = 10;
            this.buttonStop.Text = "Остановить";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // listViewReport
            // 
            this.listViewReport.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listViewReport.Location = new System.Drawing.Point(9, 191);
            this.listViewReport.Name = "listViewReport";
            this.listViewReport.ShowItemToolTips = true;
            this.listViewReport.Size = new System.Drawing.Size(776, 230);
            this.listViewReport.TabIndex = 11;
            this.listViewReport.UseCompatibleStateImageBehavior = false;
            this.listViewReport.View = System.Windows.Forms.View.Details;
            this.listViewReport.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewReport_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Имя файла";
            this.columnHeader1.Width = 122;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Путь к файлу";
            this.columnHeader2.Width = 427;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Размер файла";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 111;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Кол-во слов";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 96;
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonExit.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExit.Location = new System.Drawing.Point(695, 509);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(90, 33);
            this.buttonExit.TabIndex = 12;
            this.buttonExit.Text = "ВЫХОД";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonPresStop);
            this.groupBox1.Controls.Add(this.buttonProcess);
            this.groupBox1.Controls.Add(this.buttonStop);
            this.groupBox1.Location = new System.Drawing.Point(262, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 148);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Работа с потоком";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(604, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 18);
            this.label3.TabIndex = 14;
            this.label3.Text = "ТОП 10 СЛОВ";
            // 
            // listViewTop
            // 
            this.listViewTop.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listViewTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listViewTop.Location = new System.Drawing.Point(541, 31);
            this.listViewTop.Name = "listViewTop";
            this.listViewTop.ShowItemToolTips = true;
            this.listViewTop.Size = new System.Drawing.Size(247, 130);
            this.listViewTop.TabIndex = 15;
            this.listViewTop.UseCompatibleStateImageBehavior = false;
            this.listViewTop.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "№п/п";
            this.columnHeader5.Width = 52;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Слово";
            this.columnHeader6.Width = 115;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Кол-во";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(585, 424);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "Кол-во файлов   -";
            // 
            // labelCountFile
            // 
            this.labelCountFile.AutoSize = true;
            this.labelCountFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCountFile.Location = new System.Drawing.Point(742, 424);
            this.labelCountFile.Name = "labelCountFile";
            this.labelCountFile.Size = new System.Drawing.Size(17, 18);
            this.labelCountFile.TabIndex = 17;
            this.labelCountFile.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(195, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(425, 18);
            this.label5.TabIndex = 18;
            this.label5.Text = "ОТЧЕТ ПО ФАЙЛАМ С ЗАПРЕЩЕННЫМИ СЛОВАМИ";
            // 
            // buttonSaveReport
            // 
            this.buttonSaveReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonSaveReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSaveReport.Location = new System.Drawing.Point(12, 509);
            this.buttonSaveReport.Name = "buttonSaveReport";
            this.buttonSaveReport.Size = new System.Drawing.Size(175, 33);
            this.buttonSaveReport.TabIndex = 19;
            this.buttonSaveReport.Text = "Сохранить в файл";
            this.buttonSaveReport.UseVisualStyleBackColor = false;
            this.buttonSaveReport.Click += new System.EventHandler(this.buttonSaveReport_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 552);
            this.Controls.Add(this.buttonSaveReport);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelCountFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listViewTop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.listViewReport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBarProcess);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAddWord);
            this.Controls.Add(this.textBoxEnterWord);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "ПОИСК ЗАПРЕЩЕННЫХ СЛОВ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxEnterWord;
        private System.Windows.Forms.Button buttonAddWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.OpenFileDialog openFileDialogWords;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ProgressBar progressBarProcess;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonPresStop;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ListView listViewReport;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listViewTop;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelCountFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSaveReport;
        private System.Windows.Forms.SaveFileDialog saveFileDialogReport;
    }
}

