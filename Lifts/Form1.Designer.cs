
namespace Lifts
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.TimerPriority = new System.Windows.Forms.Timer(this.components);
            this.OutFocus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.richTextBox1.Location = new System.Drawing.Point(1002, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(207, 691);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "";
            // 
            // TimerPriority
            // 
            this.TimerPriority.Tick += new System.EventHandler(this.TimerPriority_Tick);
            // 
            // OutFocus
            // 
            this.OutFocus.AutoSize = true;
            this.OutFocus.Location = new System.Drawing.Point(4, 4);
            this.OutFocus.Name = "OutFocus";
            this.OutFocus.Size = new System.Drawing.Size(0, 13);
            this.OutFocus.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1209, 691);
            this.Controls.Add(this.OutFocus);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Приложение \"Лифты\"";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Timer TimerPriority;
        private System.Windows.Forms.Label OutFocus;
    }
}

