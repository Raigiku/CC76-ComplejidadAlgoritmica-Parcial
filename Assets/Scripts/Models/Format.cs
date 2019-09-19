namespace Complejidad.Models
{
    public class Format
    {
        public string Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Count { get; set; }

        public static bool operator ==(Format a, Format b)
        {
            if ((object)a == null || (object)b == null)
                return false;

            if (a.Width == b.Width && a.Height == b.Height)
                return true;
            return false;
        }

        public static bool operator !=(Format a, Format b)
        {
            if (a == null || b == null)
                return false;

            return !(a == b);
        }
    }
}