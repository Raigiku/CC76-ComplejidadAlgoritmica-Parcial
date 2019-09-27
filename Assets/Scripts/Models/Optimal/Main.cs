using System;
using System.Diagnostics;

namespace Complejidad.Models.Optimal
{
    public class Main : Complejidad.Models.Algorithm
    {
        public override void Execute()
        {
            var input_lines = System.IO.File.ReadAllLines(@"Assets\input.txt");
            var dimensions = input_lines[0].Split(' ');
            var width = int.Parse(dimensions[0]);
            var height = int.Parse(dimensions[1]);
            int n = int.Parse(input_lines[1]);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var initialMemory = GC.GetTotalMemory(false);

            Matriz m = new Matriz(width, height);

            var finalMemory = GC.GetTotalMemory(false);
            stopWatch.Stop();
            TimeSpan timeElapsedSpan = stopWatch.Elapsed;

            TimeElapsed = timeElapsedSpan.TotalSeconds.ToString();
            MemoryUsed = ((finalMemory - initialMemory) / 1_024).ToString();

            m.printInput();
            m.printResp();
        }
    }
}
