using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лфбораторная_работа__4
{
    public partial class Form1 : Form
    {
        public string fn;
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            toolStripStatusLabel3.Text = "Состояние: Открытие файла";
            openFileDialog1.FileName = String.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            // вызов диалогового окна открытия файла
            {
                fn = openFileDialog1.FileName;
                this.Text = fn;         // выведем имя файла в заголовок формы
                File txfl = new File(fn);    // создание экземпляра класса
                richTextBox1.Text = txfl.open(); //вызов метода класса TextFile
                if (txfl.Error)
                {
                    MessageBox.Show("Ошибка доступа к файлу!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "Состояние: Сохранение файла";
            File txfl = new File(fn);
            txfl.save(richTextBox1.Text);
            if (txfl.Error)
            {
                MessageBox.Show("Ошибка доступа к файлу!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string savefn = "";
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            toolStripStatusLabel3.Text = "Состояние: Сохранение файла";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savefn = saveFileDialog1.FileName;
                File txfl = new File(savefn);
                txfl.save(richTextBox1.Text);
                if (txfl.Error)
                {
                    MessageBox.Show("Ошибка доступа к файлу!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        public void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            File file = new File(fn);
            file.PrintResult(richTextBox1.Font);
        }

        private void поискСловToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> words = File.poisk(richTextBox1.Text);
            if(words != null && words.Count > 0)
            {
                string addString = "";
                foreach (string word in words)
                {
                    addString += word + " ";
                }
                MessageBox.Show("Найденные слова: " + addString);
            }
            else
            {
                MessageBox.Show("Слова не найдены");
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Число знаков:" + richTextBox1.Text.Trim().Length;
            int sum = 1;
            for (int i = 0; i < richTextBox1.Text.Length; i++)
            {
                if (richTextBox1.Text[i] == '\n')
                {
                    sum++;
                }
            }
            toolStripStatusLabel2.Text = "Число сторк:" + sum;
            toolStripStatusLabel3.Text = "Состояние: ввод данных";
        }

        private void скрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
        }

        private void показатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowDialog();
            Font font = fontDialog.Font;
            richTextBox1.SelectionFont = font;
        }

        private void цветТекстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
            Color color = colorDialog.Color;
            richTextBox1.SelectionColor = color;
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Работу выполнил студент гр. 319\\1 Приходько Иван");
        }
    }
}
