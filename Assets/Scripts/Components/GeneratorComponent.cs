using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Complejidad.Components
{
    public class GeneratorComponent : MonoBehaviour
    {
        private List<Models.Format> formats;

        public void GenerateFile()
        {
            formats = new List<Models.Format>();
            System.Random random = new System.Random();
            int width = random.Next(1, 1001);
            int height = random.Next(1, 1001);
            int formatsNumber = random.Next(1, 101);
            int unitsNumber = random.Next(1, 101);
            int formatWidth, formatHeight, formatCount;
            string format = "A";
            for (int i = 0; i < formatsNumber; i++)
            {
                formatWidth = random.Next(1, width + 1);
                formatHeight = random.Next(1, height + 1);
                formatCount = random.Next(1, unitsNumber);

                Models.Format newFormat = new Models.Format()
                { Id = format, Width = formatWidth, Height = formatHeight, Quantity = formatCount };

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
                file.WriteLine($"{formatsNumber}");

                foreach (var item in formats)
                {
                    file.WriteLine($"{item.Id} {item.Width} {item.Height} {item.Quantity}");
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