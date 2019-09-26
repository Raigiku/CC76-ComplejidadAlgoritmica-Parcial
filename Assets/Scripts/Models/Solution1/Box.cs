namespace Complejidad.Models.Solution1
{
    public class Box
    {
        public static int IdCounter;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Box()
        {
            IdCounter++;
            Id = IdCounter;
        }

        public override string ToString()
        {
            return $"Box {{ Id: {Id}, Name: {Name}, Level: {Level}, Width: {Width}, Height: {Height} }}";
        }
    }
}