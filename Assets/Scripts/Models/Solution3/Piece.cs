namespace Complejidad.Models.Solution3
{
    class Piece
    {
        public Piece(string Id, int W, int H, int Quant)
        {
            //Console.WriteLine("hola");
            this.Id = Id;
            if (H > W)
            {
                this.H = W;
                this.W = H;
                Normal = "G";
            }
            else
            {
                this.H = H;
                this.W = W;
                Normal = "N";
            }

            this.Quant = Quant;
            this.Available = Quant;
        }


        public string Id { get; set; }
        public int H { get; set; }
        public int W { get; set; }
        public int Quant { get; set; }
        public int Available { get; set; }
        public string Normal { get; set; }
    }
}
