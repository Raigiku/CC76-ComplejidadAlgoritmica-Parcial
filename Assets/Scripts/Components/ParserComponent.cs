using UnityEngine;

namespace Complejidad.Components
{
    public class ParserComponent : MonoBehaviour
    {
        [SerializeField]
        private ResizeComponent gridResizeComponent = null;

        [SerializeField]
        private StoreRectanglesComponent storeRectanglesComponent = null;

        public void ReadFile()
        {
            string[] input_lines = System.IO.File.ReadAllLines(@"Assets\Input\input.txt");
            ParseGridSize(input_lines[0].Split(' '));
            ParseRectangles(input_lines);
        }

        private void ParseGridSize(string[] dimensions)
        {
            float width = float.Parse(dimensions[0]);
            float height = float.Parse(dimensions[1]);
            gridResizeComponent.ChangeWidth(width);
            gridResizeComponent.ChangeHeight(height);
        }

        private void ParseRectangles(string[] input_lines)
        {
            float total_rectangles = float.Parse(input_lines[1]);
            for (int i = 2; i < total_rectangles + 2; ++i)
            {
                string[] properties = input_lines[i].Split(' ');
                string id = properties[0];
                float width = float.Parse(properties[1]);
                float height = float.Parse(properties[2]);
                storeRectanglesComponent.Rectangles.Add(new Models.Rectangle
                {
                    id = id,
                    area = new Rect(0, 0, width, height)
                });
            }
        }
    }
}