using System;
using System.Collections.Generic;
using System.Linq;

namespace Complejidad.Models.Solution3
{
    class GFG : IComparer<Piece>
    {
        public int Compare(Piece a, Piece b)
        {
            if (a.H * a.W < b.H * b.W)
            {
                return 1;
            }
            if (a.H * a.W > b.H * b.W)
            {
                return -1;
            }
            if (a.W < b.W)
            {
                return 1;
            }
            return -1;


        }
    }

    class Algorithm
    {
        public List<Piece> Pieces { get; set; }
        public int H { get; set; }
        public int W { get; set; }
        public int Table { get; set; }
        public ulong Area { get; set; }
        public float Waste { get; set; }
        List<int> hojas;
        List<Tuple<string, int, int, string>> resp;
        List<List<Tuple<string, int, int, string>>> respT;

        public Algorithm()
        {
            Pieces = new List<Piece>();
            hojas = new List<int>();
            respT = new List<List<Tuple<string, int, int, string>>>();
            resp = new List<Tuple<string, int, int, string>>();
            //Console.WriteLine("Hola");
        }

        void imp()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Assets\output.txt"))
            {
                file.WriteLine("Planchas: " + Table.ToString() + " plancha(s) utilizada(s)");
                file.WriteLine("Desperdicio: " + Waste * 100 + "%, Area: " + Area + " metros cuadrados ");
                file.WriteLine("no hay cortes");
                for (int j = 0; j < respT.Count(); ++j)
                {
                    file.WriteLine("Plancha " + (j + 1));
                    for (int i = 0; i < respT[j].Count(); ++i)
                    {
                        file.WriteLine(respT[j][i].Item1 + " " + respT[j][i].Item2.ToString() + " " + respT[j][i].Item3.ToString() + " " + respT[j][i].Item4);
                    }
                }
            }
        }

        public int a()
        {
            Table = 1;

            Pieces.Sort(new GFG());
            while (!BBF())
            {


                if (resp.Count() == 0)
                    break;

                respT.Add(resp.ToList());

                resp.Clear();

                if (finished())
                    break;


                Table += 1;
            }
            Area = areaDespercidiada();
            Waste = Area;
            Waste /= (H * W);
            Waste /= Table;

            imp();
            return Table;
        }


        bool BBF()
        {
            hojas = new List<int>();
            for (int i = 0; i < W; ++i)
                hojas.Add(0);


            int esp = 0;

            int refenceLine = H;
            int init = 0;
            bool inf = true;

            Tuple<int, int> gap = Tuple.Create(0, 0);

            while (!finished() && esp < H * W)
            {

                gap = FindLowestGap(init);
                if (gap.Item1 == 0 && gap.Item2 == 0) break;
                int index = FindBestFittingItem(gap.Item2, refenceLine - hojas[gap.Item1]);
                if (index >= 0)
                {
                    if (inf)
                    {
                        refenceLine = H;
                        //refenceLine = Pieces[index].H + hojas[gap.Item1];
                        inf = false;
                    }

                    resp.Add(Tuple.Create(Pieces[index].Id + (Pieces[index].Quant - Pieces[index].Available).ToString(), gap.Item1, H - (hojas[gap.Item1] + Pieces[index].H), Pieces[index].Normal));

                    FillSheets(Pieces[index].H, gap.Item1, Pieces[index].W);
                    esp += Pieces[index].H * Pieces[index].W;

                    init = 0;
                }
                else
                {
                    if (index == -1)
                    {
                        int menor = Math.Min((gap.Item1 - 1 >= 0) ? (hojas[gap.Item1 - 1] - hojas[gap.Item1]) : H, (gap.Item2 + gap.Item1 < W) ? (hojas[gap.Item2 + gap.Item1] - hojas[gap.Item2 + gap.Item1 - 1]) : H);

                        FillSheets(menor, gap.Item1, gap.Item2);

                        esp += menor * gap.Item2;
                    }
                    else
                    {
                        if (!inf)
                        {
                            refenceLine = H;
                            inf = true;
                        }

                    }
                }

            }

            return false;

        }

        Tuple<int, int> FindLowestGap(int init)
        {
            int index = -1, w = 0, min = W;
            bool inte = false;
            for (int i = init; i < W; ++i)
            {
                if (hojas[i] < min)
                {
                    min = hojas[i];
                    index = i;
                    w = 1;
                    inte = false;
                }
                else
                {
                    if (hojas[i] == min && min != W && !inte)
                        w++;
                    else
                    {
                        inte = true;
                    }
                }

            }
            if (index == -1) index = init;
            return Tuple.Create(index, w);
        }


        int FindBestFittingItem(int wmax, int hmax)
        {
            int index = -1;

            for (int i = 0; i < Pieces.Count(); ++i)
            {
                if (Pieces[i].W <= wmax && Pieces[i].Available > 0)
                {
                    if (Pieces[i].H <= hmax)
                    {
                        Pieces[i].Available--;
                        return i;
                    }

                }
                else
                {
                    if (Pieces[i].H <= wmax && Pieces[i].Available > 0)
                    {
                        if (Pieces[i].W <= hmax)
                        {
                            Pieces[i].Available--;
                            int temp = Pieces[i].H;
                            Pieces[i].H = Pieces[i].W;
                            Pieces[i].W = temp;
                            if (Pieces[i].Normal == "G")
                                Pieces[i].Normal = "N";
                            else
                                Pieces[i].Normal = "G";
                            return i;
                        }

                    }
                }
            }

            return index;
        }

        void FillSheets(int inc, int init, int dist)
        {
            for (int i = init; i < init + dist; ++i)
            {
                hojas[i] += inc;
            }
        }

        bool finished()
        {

            for (int i = 0; i < this.Pieces.Count(); ++i)
            {
                if (this.Pieces[i].Available > 0)
                    return false;
            }
            return true;


        }

        ulong areaDespercidiada()
        {
            ulong A = (ulong)(W * H);
            A *= (ulong)(Table);
            for (int i = 0; i < Pieces.Count(); ++i)
            {
                A -= (ulong)(Pieces[i].Quant * Pieces[i].H * Pieces[i].W);
            }

            return A;
        }

    }
}
