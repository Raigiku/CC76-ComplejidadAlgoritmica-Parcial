namespace Complejidad.Models
{
    public abstract class Algorithm
    {
        public string Name { get; set; }
        public string TimeElapsed { get; set; }
        public string MemoryUsed { get; set; }
        public abstract void Execute();
    }
}