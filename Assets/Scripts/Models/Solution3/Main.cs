namespace Complejidad.Models.Solution3
{
    public class Main : Complejidad.Models.Algorithm
    {
        public override void Execute()
        {
            Algorithm a = new Algorithm();
            var input_lines = System.IO.File.ReadAllLines(@"Assets\input.txt");
            var dimensions = input_lines[0].Split(' ');
            a.W = int.Parse(dimensions[0]);
            a.H = int.Parse(dimensions[1]);
            int n = int.Parse(input_lines[1]);
            for (var i = 2; i < n + 2; ++i)
            {
                var properties = input_lines[i].Split(' ');
                var id = properties[0];
                var width = int.Parse(properties[1]);
                var height = int.Parse(properties[2]);
                var count = int.Parse(properties[3]);
                Piece p = new Piece(id, height, width, count);
                a.Pieces.Add(p);
            }
            a.a();
        }
    }
}