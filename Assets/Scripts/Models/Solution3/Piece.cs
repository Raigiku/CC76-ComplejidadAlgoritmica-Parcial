namespace Complejidad.Models.Solution3
{
    class Piece
    {
        public Piece(string Id, int H, int W, int Quant)
        {
            this.Id = Id;
            if (H > W)
            {
                this.H = W;
                this.W = H;
                Normal = "N";
            }
            else
            {
                this.H = H;
                this.W = W;
                Normal = "G";
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
