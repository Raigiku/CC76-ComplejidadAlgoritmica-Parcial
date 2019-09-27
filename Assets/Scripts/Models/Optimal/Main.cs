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
            Matriz m = new Matriz(width, height);
            m.printInput();
            m.printResp();
        }
    }
}
