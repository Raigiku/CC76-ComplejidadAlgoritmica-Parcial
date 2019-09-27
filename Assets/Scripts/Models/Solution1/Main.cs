using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Complejidad.Models.Solution1
{
    public class Main : Algorithm
    {
        public override void Execute()
        {
            int width, height, formatsNumber, formatWidth, formatHeight, formatCount;
            string line;
            string[] data;
            string formatName;

            Packer packer;
            List<Format> formats = new List<Format>();


            using (StreamReader file = new StreamReader(@"Assets\input.txt"))
            {
                // Width and Height
                data = file.ReadLine().Split(' ');
                width = Convert.ToInt32(data[0]);
                height = Convert.ToInt32(data[1]);

                packer = new Packer(width, height);

                // formats number
                line = file.ReadLine();
                formatsNumber = Convert.ToInt32(line);

                // name, width, height, counter
                for (int i = 0; i < formatsNumber; i++)
                {
                    data = file.ReadLine().Split(' ');
                    formatName = data[0];
                    formatWidth = Convert.ToInt32(data[1]);
                    formatHeight = Convert.ToInt32(data[2]);
                    formatCount = Convert.ToInt32(data[3]);

                    formats.Add(new Format()
                    {
                        Id = formatName,
                        Width = formatWidth,
                        Height = formatHeight,
                        Quantity = formatCount
                    });
                }
            }


            List<Box> boxes = FormatConverter.FormatToBox(formats);

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var initialMemory = GC.GetTotalMemory(false);

            List<List<Box>> levelBoxes = packer.Insert(boxes);

            var finalMemory = GC.GetTotalMemory(false);
            stopWatch.Stop();
            TimeSpan timeElapsedSpan = stopWatch.Elapsed;
            
            TimeElapsed = timeElapsedSpan.TotalSeconds.ToString();
            MemoryUsed = ((finalMemory - initialMemory) / 1_024).ToString();

            Tuple<float, float> unusedArea = packer.GetUnusedPercentageAndArea();
            Printer.PrintFile(width, height, levelBoxes, unusedArea);
        }
    }
}