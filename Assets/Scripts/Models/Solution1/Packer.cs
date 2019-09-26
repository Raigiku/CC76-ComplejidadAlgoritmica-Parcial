using System;
using System.Collections.Generic;

namespace Complejidad.Models.Solution1
{
    public class Packer
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Node> Nodes { get; set; }

        public Packer(int width, int height)
        {
            Width = width;
            Height = height;
            Nodes = new List<Node>();
            AddNode();
        }

        private void AddNode()
        {
            Nodes.Add(new Node(Width, Height));
        }

        private void AddNode(Box box)
        {
            Node node = new Node(Width, Height);
            node.Insert(box);
            Nodes.Add(node);
        }

        public List<List<Box>> Insert(List<Box> boxes)
        {
            for (int i = 0; i < boxes.Count; i++)
            {
                InsertBox(boxes[i]);
            }

            return GetBoxLevels(boxes);
        }

        private void InsertBox(Box box)
        {
            for (int i = 0; i < Nodes.Count; ++i)
            {
                if (Nodes[i].Insert(box) != null)
                {
                    box.Level = i;
                    return;
                }
            }

            box.Level = Nodes.Count;
            AddNode(box);
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

        private float GetUnusedArea(Node node)
        {
            if (node.IsALeaf())
            {
                if (!node.HasBox())
                    return node.Width * node.Height;
                return 0f;
            }

            return GetUnusedArea(node.Left) + GetUnusedArea(node.Right);
        }

        public Tuple<float, float> GetUnusedPercentageAndArea()
        {
            float nodesCount = Nodes.Count;
            float totalArea = Width * Height * nodesCount;
            float unusedArea = 0f;

            for (int i = 0; i < nodesCount; i++)
            {
                unusedArea += GetUnusedArea(Nodes[i]);
            }

            return new Tuple<float, float>(unusedArea / totalArea, unusedArea);
        }
    }
}