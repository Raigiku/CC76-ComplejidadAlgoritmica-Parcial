using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Complejidad.Models
{
    public class Generator
    {
        private List<Format> Formats { get; set; }

        public Generator()
        {
            Restart();
        }

        public void Restart()
        {
            Formats = new List<Format>();
        }

        public void GenerateFile()
        {
            Random random = new Random();
            int width, heigth, formatSize, formatWidth, formatHeight, formatCount;
            const int maxSize = 1001;
            string format = "A";

            width = random.Next(1, maxSize);
            heigth = random.Next(1, maxSize);
            formatSize = random.Next(1, maxSize);

            // Console.WriteLine($"{width}, {heigth}");
            // Console.WriteLine($"{formatSize}");

            for (int i = 0; i < formatSize; i++)
            {
                formatWidth = random.Next(1, width + 1);
                formatHeight = random.Next(1, heigth + 1);
                formatCount = random.Next(1, 10);

                Format newFormat = new Format()
                { Id = format, Width = formatWidth, Height = formatHeight, Count = formatCount };

                if (IsFormatUsed(newFormat))
                {
                    --i;
                    continue;
                }

                Formats.Add(newFormat);

                //Console.WriteLine($"{format} {formatWidth} {formatHeight} {formatCount}");

                format = GetNextString(format);
            }

            using (StreamWriter file = new StreamWriter("input_file.txt"))
            {
                file.WriteLine($"{width}, {heigth}");
                file.WriteLine($"{formatSize}");

                foreach (Format item in Formats)
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

        private bool IsFormatUsed(Format format)
        {
            for (int i = 0; i < Formats.Count; i++)
            {
                if (Formats[i] == format)
                {
                    return true;
                }
            }

            return false;
        }
    }
}