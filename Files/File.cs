using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Files
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

        public List<String> poisk(string text)
        {
            string[] sogl = new string[] { "б", "в", "г", "д", "ж", "з", "й", "к", "л", "м", "н", "п", "р", "с", "т", "ф", "х", "ц", "ч", "ш", "щ" };
            text = text.Trim();
            text = text.ToLower();
            text = text.TrimStart(new char[] { ',', '.', '(', ')', '\\', '/' });
            string[] masText = text.Split(' ');
            List<String> words = new List<string>();
            for (int i = 0; i < masText.Length; i++)
            {
                if (sogl.Contains(masText[i].Substring(masText.Length - 1)))
                {
                    words.Add(masText[i]);
                }
            }

            words.Sort(new MyCompare());
            return words;
        }
    }

    public class MyCompare : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            if (x.Length > y.Length)
            {
                return 1;
            }
            else if (x.Length == y.Length)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}