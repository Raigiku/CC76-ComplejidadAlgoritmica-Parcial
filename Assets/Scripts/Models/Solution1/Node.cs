namespace Complejidad.Models.Solution1
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Box Box { get; set; }

        public Node()
        {
            Box = null;
            Left = null;
            Right = null;
        }

        public Node(int width, int height)
        {
            Box = null;
            Left = null;
            Right = null;
            Width = width;
            Height = height;
        }

        public Node(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Box = null;
            Left = null;
            Right = null;
        }

        public Node Insert(Box box)
        {
            if (!IsALeaf())
            {
                Node newNode = Left.Insert(box);

                if (newNode != null)
                {
                    return newNode;
                }

                return Right.Insert(box);
            }

            if (Box != null)
            {
                return null;
            }

            FitType fitType = GetFitType(box);

            // La caja es muy grande
            if (fitType == FitType.Bigger)
            {
                return null;
            }

            // La caja es exacta
            if (fitType == FitType.Exact)
            {
                box.X = X;
                box.Y = Y;
                // box.Width = Width;
                // box.Height = Height;
                this.Box = box;
                return this;
            }

            // La caja es pequeña
            // Dividir el nodo
            //fitType == FitType.SMALLER

            int horizontalLine = Width - box.Width;
            int verticalLine = Height - box.Height;

            if (horizontalLine >= verticalLine)
            {
                Left = new Node(X, Y, box.Width, Height);
                /*Left.X = X;
                Left.Y = Y;
                Left.Width = box.Width;
                Left.Height = Height;*/

                Right = new Node(X + box.Width, Y, horizontalLine, Height);
                /*Right.X = X + box.Width;
                Right.Y = Y;
                Right.Width = horizontalLine;
                Right.Height = Height;*/
            }
            else
            {
                Left = new Node(X, Y, Width, box.Height);
                /*Left.X = X;
                Left.Y = Y;
                Left.Width = Width;
                Left.Height = box.Height;*/

                Right = new Node(X, Y + box.Height, Width, verticalLine);
                /*Right.X = X;
                Right.Y = Y + box.Height;
                Right.Width = Width;
                Right.Height = verticalLine;*/
            }


            return Left.Insert(box);
        }


        // No se considera las rotaciones de las figuras
        public FitType GetFitType(Box box)
        {
            if (box.Width > Width || box.Height > Height)
            {
                return FitType.Bigger;
            }

            if (box.Width == Width && box.Height == Height)
            {
                return FitType.Exact;
            }

            return FitType.Smaller;
        }

        public bool IsALeaf()
        {
            if (Left == null && Right == null)
            {
                return true;
            }

            return false;
        }

        public bool HasBox()
        {
            if (Box != null)
                return true;
            return false;
        }

        public override string ToString()
        {
            return $"{X}, {Y}, {Width}, {Height}";
        }
    }
}