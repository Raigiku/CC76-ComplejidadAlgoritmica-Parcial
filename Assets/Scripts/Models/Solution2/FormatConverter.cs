using System.Collections.Generic;

namespace Complejidad.Models.Solution2
{
    public class FormatConverter
    {
        public FormatConverter()
        {
        }

        public static List<Box> FormatToBox(List<Format> formats)
        {
            List<Box> boxes = new List<Box>();
            int idCounter = 1;

            for (int i = 0; i < formats.Count; i++)
            {
                for (int j = 0; j < formats[i].Quantity; j++)
                {
                    boxes.Add(new Box()
                    {
                        Id = idCounter++,
                        Name = formats[i].Id + $"{j + 1}",
                        Width = formats[i].Width,
                        Height = formats[i].Height
                    });
                }
            }

            return boxes;
        }
    }
}