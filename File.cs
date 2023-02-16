using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лфбораторная_работа__4
{
   public class File
   {
            private string path;
            public bool Error { get; set; }

            public File(string path)
            {
                this.path = path;
            }

            public String open()
            {
                string result = "";
                try
                {
                    StreamReader sr = new StreamReader(path);
                    result = sr.ReadToEnd();
                    sr.Close();
                }
                catch
                {
                    Error = true;
                }
                return result;
            }

            public void save(string text)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(path);
                    sw.Write(text);
                    sw.Close();
                }
                catch
                {
                    Error = true;
                }
            }

            public static List<String> poisk(string text)
            {
                if(text == "")
                {
                    return null;
                }
                string[] sogl = new string[] { "б", "в", "г", "д", "ж", "з", "й", "к", "л", "м", "н", "п", "р", "с", "т", "ф", "х", "ц", "ч", "ш", "щ" };
                text = text.Trim();
                text = text.ToLower();
                text = text.TrimStart(new char[] { ',', '.', '(', ')', '\\', '/' });
                string[] masText = text.Split(' ');
                List<String> words = new List<string>();
                for (int i = 0; i < masText.Length; i++)
                {
                    if (sogl.Contains(masText[i].Substring(masText[i].Length - 1)) && !words.Contains(masText[i]))
                    {
                        words.Add(masText[i]);
                    }
                }
                words.Sort(new MyCompare());
                if (words != null && words.Count > 0)
                {
                    int maxLength = words[0].Length;
                    for(int i = 0; i < words.Count; i++)
                    {
                        if (words[i].Length != maxLength)
                        {
                            words.Remove(words[i]);
                            i--;
                        }
                    }
                }
                return words;
            }

        StreamReader streamToPrint;
        Font printFont;
        public bool PrintResult(Font pF)
        {
            try
            {
                streamToPrint = new System.IO.StreamReader(path,
                    System.Text.Encoding.GetEncoding(1251));
                try
                {
                    printFont = pF;
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler
                       (this.pd_PrintPage);
                    pd.Print();
                    return true;
                }
                finally
                {
                    streamToPrint.Close();
                }
            }
            catch
            {
                return false;
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            string line = null;

            // Чтобы вычислить количество строк на странице
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);

            // Печатаем каждую строку файла
            while (count < linesPerPage && ((line = streamToPrint.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                leftMargin, yPos, new StringFormat());
                count++;
            }

            // если строки не закончились, распечатаем еще одну страницу
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }


    }


    public class MyCompare : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x.Length > y.Length)
                {
                    return -1;
                }
                else if (x.Length == y.Length)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }     


}
