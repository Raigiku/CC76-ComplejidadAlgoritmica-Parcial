using System;
using System.Collections.Generic;

namespace Complejidad.Models.Solution2
{
    public class Packer
    {
        private int Width { get; set; }
        private int Height { get; set; }
        public List<Solution2.Board> Boards { get; set; }

        public Packer(int width, int height)
        {
            Width = width;
            Height = height;

            // Agregar el primer board
            Boards = new List<Solution2.Board>();
            Boards.Add(new Solution2.Board(width, height));
        }

        public List<List<Box>> InsertBoxes(List<Box> boxes)
        {
            foreach (Box box in boxes)
            {
                Insert(box);
            }

            return GetBoxLevels(boxes);
        }

        public void Insert(Box box)
        {
            if (!Fits(box))
            {
                box.Rotate();
            }

            for (int i = 0; i < Boards.Count; i++)
            {
                if (Boards[i].Insert(box))
                {
                    box.Level = i;
                    return;
                }
            }

            // Crear otro tablero
            Solution2.Board newBoard = new Solution2.Board(Width, Height);
            newBoard.Insert(box);
            box.Level = Boards.Count;
            Boards.Add(newBoard);
        }

        private bool Fits(Box box)
        {
            if (box.Width <= Width && box.Height <= Height)
            {
                return true;
            }

            return false;
        }

        private List<List<Box>> GetBoxLevels(List<Box> boxes)
        {
            int maxLevel = GetMaxLevel(boxes) + 1;

            List<List<Box>> levelBoxes = new List<List<Box>>();

            for (int i = 0; i < maxLevel; i++)
            {
                levelBoxes.Add(new List<Box>());
            }

            for (int i = 0; i < boxes.Count; i++)
            {
                levelBoxes[boxes[i].Level].Add(boxes[i]);
            }

            return levelBoxes;
        }

        private int GetMaxLevel(List<Box> boxes)
        {
            int max = 0;

            foreach (Box box in boxes)
            {
                max = Math.Max(max, box.Level);
            }

            return max;
        }

        public Tuple<float, float> GetUnusedPercentageAndArea(List<Box> boxes)
        {
            float boardsSize = Boards.Count;
            float totalArea = Width * Height * boardsSize;
            float usedArea = 0f;

            foreach (Box box in boxes)
            {
                usedArea += box.Width * box.Height;
            }

            return new Tuple<float, float>((totalArea - usedArea) / totalArea, totalArea - usedArea);
        }
    }
}