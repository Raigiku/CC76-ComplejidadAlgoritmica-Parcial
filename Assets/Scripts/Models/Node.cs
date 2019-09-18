namespace Complejidad.Models
{
    public enum FitType
    {
        BIGGER,
        EXACT,
        SMALLER
    }

    public class Node
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Rectangle Rectangle { get; set; }

        public Node()
        {
            Rectangle = null;
            Left = null;
            Right = null;
        }

        public Node Insert(Rectangle rectangle)
        {
            // Si no somos hojas
            if (!IsALeaf())
            {
                Node newNode = Left.Insert(rectangle);

                if (newNode != null)
                {
                    return newNode;
                }

                return Right.Insert(rectangle);
            }
            else
            {
                // Si tenemos una caja
                if (Rectangle != null)
                {
                    return null;
                }

                FitType fitType = GetFitType(rectangle);

                // La caja es muy grande
                if (fitType == FitType.BIGGER)
                {
                    //Console.WriteLine($"Es mas pequeño: {box.Id}");
                    return null;
                }

                // La caja es exacta
                else if (fitType == FitType.EXACT)
                {
                    //Console.WriteLine($"Es exacto {box.Id}, {X}, {Y}");
                    rectangle.area.x = X;
                    rectangle.area.y = Y;
                    // box.Width = Width;
                    // box.Height = Height;
                    this.Rectangle = rectangle;
                    return this;
                }

                // La caja es pequeña
                // Dividir el nodo
                //fitType == FitType.SMALLER
                else
                {
                    //Console.WriteLine($"Es más grande: {box.Id}");
                    Left = new Node();
                    Right = new Node();

                    int horizontalLine = Width - (int)rectangle.area.width;
                    int verticalLine = Height - (int)rectangle.area.height;

                    if (horizontalLine > verticalLine)
                    {
                        Left.X = X;
                        Left.Y = Y;
                        Left.Width = (int)rectangle.area.width;
                        Left.Height = Height;
                        

                        Right.X = X + (int)rectangle.area.width;
                        Right.Y = Y;
                        Right.Width = horizontalLine;
                        Right.Height = Height;
                        
                    }
                    else
                    {
                        Left.X = X;
                        Left.Y = Y;
                        Left.Width = Width;
                        Left.Height = (int)rectangle.area.height;

                        Right.X = X;
                        Right.Y = Y + (int)rectangle.area.height;
                        Right.Width = Width;
                        Right.Height = verticalLine;
                    }
                    // Console.WriteLine(Left);
                    // Console.WriteLine(Right);
                    // Console.WriteLine("========================");
                }
                return Left.Insert(rectangle);
            }
        }

        // No se considera las rotaciones de las figuras
        public FitType GetFitType(Rectangle rectangle)
        {
            if (rectangle.area.width > Width || rectangle.area.height > Height)
            {
                return FitType.BIGGER;
            }
            if (rectangle.area.width == Width && rectangle.area.height == Height)
            {
                return FitType.EXACT;
            }
            return FitType.SMALLER;
        }

        public bool IsALeaf()
        {
            if (Left == null && Right == null)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{X}, {Y}, {Width}, {Height}";
        }
    }
}
