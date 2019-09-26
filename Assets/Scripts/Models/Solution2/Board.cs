using System.Collections.Generic;

namespace Complejidad.Models.Solution2
{
    public class Board
    {
        private int Width { get; set; }
        private int Height { get; set; }

        public List<Level> Levels { get; set; }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;

            // Se agrega el primer nivel
            Levels = new List<Level>();
            Levels.Add(new Level(0, 0, width, height));
        }

        public bool Insert(Box box)
        {
            for (int i = 0; i < Levels.Count; i++)
            {
                if (InsertInLevel(box, Levels[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private bool InsertInLevel(Box box, Level level)
        {
            // Si es asignable
            if (box.Height <= level.Height && box.Width <= level.Width)
            {
                box.X = level.X;
                box.Y = level.Y;
                level.X += box.Width;
                level.Width -= box.Width;

                if (box.Height == level.Height)
                {
                    level.IsSealed = true;
                }
                else if (!level.IsSealed)
                {
                    // Sellar y agregar otro nivel
                    level.Height = box.Height;

                    level.IsSealed = true;

                    Level newLevel = new Level();
                    newLevel.X = 0;
                    newLevel.Y = level.Y + level.Height;
                    newLevel.Width = Width;
                    newLevel.Height = Height - level.Y - level.Height;
                    Levels.Add(newLevel);
                }


                return true;
            }

            return false;
        }
    }
}