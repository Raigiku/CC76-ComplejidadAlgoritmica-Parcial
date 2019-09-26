namespace Complejidad.Models.Solution2
{
    public class Level
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsSealed { get; set; }


        public Level()
        {
            X = Y = 0;
        }

        public Level(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            IsSealed = false;
        }
    }
}