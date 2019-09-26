using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

namespace Complejidad.Models.Solution1
{
    public class Printer
    {
        public static void PrintMatrixes(int width, int height, List<List<Box>> boxes, Tuple<float, float> unusedArea)
        {
            WriteLine($"Planchas: {boxes.Count} plancha(s) utilizada(s) ");
            WriteLine($"Desperdicio: {unusedArea.Item1 * 100f}%, Area: {unusedArea.Item2} metros cuadrados");
            WriteLine($"Cortes: algoritmo packing");

            for (int i = 0; i < boxes.Count; i++)
            {
                WriteLine($"Plancha {i + 1}");

                foreach (Box box in boxes[i])
                {
                    WriteLine($"{box.Name} {box.X} {box.Y} N");
                }
            }
        }

        public static void PrintFile(int width, int height, List<List<Box>> boxes, Tuple<float, float> unusedArea)
        {

            using (StreamWriter file = new StreamWriter(@"Assets\output.txt"))
            {
                file.WriteLine($"Planchas: {boxes.Count} plancha(s) utilizada(s) ");
                file.WriteLine($"Desperdicio: {unusedArea.Item1 * 100f}%, Area: {unusedArea.Item2} metros cuadrados");
                file.WriteLine($"Cortes: algoritmo packing");

                for (int i = 0; i < boxes.Count; i++)
                {
                    file.WriteLine($"Plancha {i + 1}");

                    foreach (Box box in boxes[i])
                    {
                        file.WriteLine($"{box.Name} {box.X} {box.Y} N");
                    }
                }
            }
        }

    }
}