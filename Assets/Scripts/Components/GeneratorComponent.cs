using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Complejidad.Components
{
    public class GeneratorComponent : MonoBehaviour
    {
        private List<Models.Format> formats = new List<Models.Format>();

        public void GenerateFile()
        {
            const int maxSize = 1001;
            string format = "A";

            int width = Random.Range(1, maxSize);
            int height = Random.Range(1, maxSize);
            int formatSize = Random.Range(1, maxSize);

            for (int i = 0; i < formatSize; i++)
            {
                int formatWidth = Random.Range(1, width + 1);
                int formatHeight = Random.Range(1, height + 1);
                int formatCount = Random.Range(1, 10);

                Models.Format newFormat = new Models.Format()
                { Id = format, Width = formatWidth, Height = formatHeight, Count = formatCount };

                if (IsFormatUsed(newFormat))
                {
                    --i;
                    continue;
                }

                formats.Add(newFormat);
                format = GetNextString(format);
            }

            using (StreamWriter file = new StreamWriter(@"Assets\input.txt"))
            {
                file.WriteLine($"{width} {height}");
                file.WriteLine($"{formatSize}");

                foreach (Models.Format item in formats)
                {
                    file.WriteLine($"{item.Id} {item.Width} {item.Height} {item.Count}");
                }
            }
        }

        public string GetNextString(string current)
        {
            StringBuilder nextString = GetNextStringBuilder(new StringBuilder(current), current.Length - 1);
            return nextString.ToString();
        }

        private StringBuilder GetNextStringBuilder(StringBuilder format, int index)
        {
            // A -> Z
            // AA -> AZ
            // AAA -> ZZZ
            if (format[index] == 'Z')
            {
                if (index == 0)
                {
                    return new StringBuilder(new string('A', format.Length + 1));
                }
                else
                {
                    format[index] = 'A';
                    return GetNextStringBuilder(format, index - 1);
                }
            }
            else
            {
                format[index] = (char)(format[index] + 1);
                return format;
            }
        }

        private bool IsFormatUsed(Models.Format format)
        {
            for (int i = 0; i < formats.Count; i++)
            {
                if (formats[i] == format)
                {
                    return true;
                }
            }
            return false;
        }
    }
}