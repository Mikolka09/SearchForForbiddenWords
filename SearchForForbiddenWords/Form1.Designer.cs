
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
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialogWords = new System.Windows.Forms.OpenFileDialog();
            this.buttonStart = new System.Windows.Forms.Button();
            this.progressBarProcess = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textBoxEnterWord
            // 
            this.textBoxEnterWord.Location = new System.Drawing.Point(12, 36);
            this.textBoxEnterWord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxEnterWord.Multiline = true;
            this.textBoxEnterWord.Name = "textBoxEnterWord";
            this.textBoxEnterWord.Size = new System.Drawing.Size(318, 57);
            this.textBoxEnterWord.TabIndex = 0;
            this.textBoxEnterWord.TextChanged += new System.EventHandler(this.textBoxEnterWord_TextChanged);
            // 
            // buttonAddWord
            // 
            this.buttonAddWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddWord.Location = new System.Drawing.Point(224, 101);
            this.buttonAddWord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonAddWord.Name = "buttonAddWord";
            this.buttonAddWord.Size = new System.Drawing.Size(106, 37);
            this.buttonAddWord.TabIndex = 1;
            this.buttonAddWord.Text = "Добавить";
            this.buttonAddWord.UseVisualStyleBackColor = true;
            this.buttonAddWord.Click += new System.EventHandler(this.buttonAddWord_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(29, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "ДОБАВЛЕНИЕ ЗАПРЕЩЕННЫХ СЛОВ";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(12, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(191, 37);
            this.button1.TabIndex = 3;
            this.button1.Text = "Загрузить из фийла";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialogWords
            // 
            this.openFileDialogWords.FileName = "openFileDialogWords";
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStart.Location = new System.Drawing.Point(772, 15);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(88, 31);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "СТАРТ";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // progressBarProcess
            // 
            this.progressBarProcess.Location = new System.Drawing.Point(45, 410);
            this.progressBarProcess.Name = "progressBarProcess";
            this.progressBarProcess.Size = new System.Drawing.Size(815, 35);
            this.progressBarProcess.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(330, 380);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Процесс работы программы";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Location = new System.Drawing.Point(12, 157);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(848, 220);
            this.listBox1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 506);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBarProcess);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAddWord);
            this.Controls.Add(this.textBoxEnterWord);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "ПОИСК ЗАПРЕЩЕННЫХ СЛОВ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxEnterWord;
        private System.Windows.Forms.Button buttonAddWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialogWords;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ProgressBar progressBarProcess;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
    }
}

