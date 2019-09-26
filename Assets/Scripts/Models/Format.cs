namespace Complejidad.Models
{
    public class Format
    {
        public string Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Quantity { get; set; }
        
        public override string ToString()
        {
            return $"Format {{ Id: {Id}, Width: {Width}, Height: {Height}, Quantity: {Quantity} }}";
        }
    }
}