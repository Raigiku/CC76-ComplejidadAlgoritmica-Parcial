namespace Complejidad.Models.Solution2
{
    public class Box
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Level { get; set; }
        public bool IsRotated { get; set; }

        public Box()
        {
            Name = "";
            Id = Level = X = Y = Width = Height = 0;
        }

        public void Rotate()
        {
            int temp = Width;
            Width = Height;
            Height = temp;
            IsRotated = true;
        }

        public override string ToString()
        {
            return $"Box {{ Id: {Id}, Level: {Level}, Name: {Name}, X: {X}, Y: {Y}, Width: {Width}, Height: {Height} }}";
        }
    }
}