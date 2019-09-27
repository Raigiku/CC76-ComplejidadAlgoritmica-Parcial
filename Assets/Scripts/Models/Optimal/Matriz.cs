using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Complejidad.Models.Optimal
{
    class Matriz
    {
        public int Width { get; set; }
        public int Height { get; set; }
        int Tables;
        Random random;
        StringBuilder Id;
        Dictionary<Tuple<int, int>, Tuple<int, string>> Pieces;
        List<List<Tuple<string, int, int, string>>> Resp;

        public Matriz(int width, int height)
        {
            random = new Random();
            Pieces = new Dictionary<Tuple<int, int>, Tuple<int, string>>();
            Resp = new List<List<Tuple<string, int, int, string>>>();
            Id = new StringBuilder("A");
            Width = width;
            Height = height;
            generateMatriz();
        }


        public void generateMatriz()
        {
            Tables = random.Next(1, 11);
            for (int i = 0; i < Tables; ++i)
            {
                Resp.Add(new List<Tuple<string, int, int, string>>());
                generatePieces(0, 0, Height, Width, i);
            }
        }

        void generatePieces(int x, int y, int height, int widht, int r)
        {
            if (widht == 0 || height == 0)
                return;

            int h = random.Next(1, height);
            int w = random.Next(1, widht);

            creationPiece(x, y, h, w, r);

            generatePieces(x + w, y, height, widht - w, r);
            generatePieces(x, y + h, height - h, w, r);

        }

        void creationPiece(int x, int y, int height, int width, int r)
        {
            Tuple<int, string> resp;
            if (Pieces.TryGetValue(Tuple.Create(width, height), out resp))
            {
                Pieces[Tuple.Create(width, height)] = Tuple.Create(resp.Item1 + 1, resp.Item2);
                Resp[r].Add(Tuple.Create((resp.Item2).ToString() + (resp.Item1 + 1).ToString(), x, this.Height - (y + height), "N"));
            }
            else
            {
                Pieces[Tuple.Create(width, height)] = Tuple.Create(1, Id.ToString());
                Resp[r].Add(Tuple.Create((Id).ToString() + (1).ToString(), x, this.Height - (y + height), "N"));
                Id = GenerateString(Id);
            }
        }

        StringBuilder GenerateString(StringBuilder code)
        {

            if (code[code.Length - 1] == 'Z')
            {
                code[code.Length - 1] = 'A';
                bool add = true;
                for (int i = code.Length - 2; i >= 0; ++i)
                {
                    if (code[i] != 'Z')
                    {
                        add = false;
                        code[i] = (char)(code[i] + 1);
                        break;
                    }
                    else
                    {
                        code[i] = 'A';
                    }
                }
                if (add)
                    code.Append('A');
            }
            else
            {
                code[code.Length - 1] = (char)(code[code.Length - 1] + 1);
            }
            return code;
        }

        public void printResp()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Assets\output.txt"))
            {
                file.WriteLine("Planchas: " + Resp.Count().ToString() + " planchas utilizadas");
                file.WriteLine("Desperdicio: 0%, Area 0 metros cuadrados");
                file.WriteLine("Cortes: algoritmo de empaquetamiento");
                for (int i = 0; i < Resp.Count(); ++i)
                {
                    file.WriteLine("Plancha " + (i + 1).ToString());
                    for (int j = 0; j < Resp[i].Count(); ++j)
                    {
                        file.WriteLine(Resp[i][j].Item1 + " " + Resp[i][j].Item2 + " " + Resp[i][j].Item3 + " " + Resp[i][j].Item4);
                    }
                }
            }
        }

        public void printInput()
        {
            using (StreamWriter file = new StreamWriter(@"Assets\input.txt"))
            {
                file.WriteLine(Width.ToString() + " " + Height.ToString());
                file.WriteLine(Pieces.Count().ToString());

                foreach (KeyValuePair<Tuple<int, int>, Tuple<int, string>> entry in Pieces)
                {
                    file.WriteLine(entry.Value.Item2.ToString() + " " + entry.Key.Item1 + " " + entry.Key.Item2 + " " + entry.Value.Item1);
                }
            }
        }
    }
}
