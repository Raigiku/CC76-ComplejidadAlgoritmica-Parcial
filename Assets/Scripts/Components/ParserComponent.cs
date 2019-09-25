using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Complejidad.Components
{
    public class ParserComponent : MonoBehaviour
    {
        private Vector2 gridScale;

        private Dictionary<string, Models.Format> formats;

        [SerializeField]
        private GameObject boardPrefab = null;

        private StoreBoardsComponent storeBoardsComponent;

        private void Awake()
        {
            storeBoardsComponent = GetComponent<StoreBoardsComponent>();
        }

        public void ReadFiles()
        {
            ClearEntities();
            ReadInput();
            ReadOutput();
        }

        private void ClearEntities()
        {
            formats = new Dictionary<string, Models.Format>();
            storeBoardsComponent.Boards.ForEach(
                boardGameObject => Destroy(boardGameObject)
            );
            storeBoardsComponent.Boards.Clear();
        }

        private void ReadInput()
        {
            var input_lines = System.IO.File.ReadAllLines(@"Assets\input.txt");
            ParseInputGrid(input_lines[0].Split(' '));
            ParseInputFormats(input_lines);
        }

        private void ReadOutput()
        {
            string[] output_lines = System.IO.File.ReadAllLines(@"Assets\output.txt");
            ParseOutputRectangles(output_lines);
        }

        private void ParseInputGrid(string[] dimensions)
        {
            var width = float.Parse(dimensions[0]);
            var height = float.Parse(dimensions[1]);
            gridScale = new Vector2(width, height);
        }

        private void ParseInputFormats(string[] input_lines)
        {
            var total_formats = int.Parse(input_lines[1]);
            for (var i = 2; i < total_formats + 2; ++i)
            {
                var properties = input_lines[i].Split(' ');
                var id = properties[0];
                var width = int.Parse(properties[1]);
                var height = int.Parse(properties[2]);
                var count = int.Parse(properties[3]);
                formats[id] = new Models.Format
                {
                    Id = id,
                    Width = width,
                    Height = height,
                    Count = count
                };
            }
        }

        private void ParseOutputRectangles(string[] output_lines)
        {
            GameObject board = null;
            for (int i = 3; i < output_lines.Length; ++i)
            {
                string[] words = output_lines[i].Split(' ');
                if (words.Length == 2)
                {
                    // Format: Plancha 1
                    board = Instantiate(boardPrefab, transform);
                    var boardGrid = board.transform.GetChild(0);
                    boardGrid.localScale = gridScale;
                    board.SetActive(false);
                    storeBoardsComponent.Boards.Add(board);
                }
                else
                {
                    // Format: A1 150 154 G
                    var id = words[0];
                    var formatId = Regex.Replace(id, @"[\d]", string.Empty);
                    Models.Format format = formats[formatId];
                    float width = formats[formatId].Width;
                    float height = formats[formatId].Height;
                    var x = float.Parse(words[1]);
                    var y = float.Parse(words[2]);
                    var rotated = words[3] == "N" ? false : true;
                    if (rotated)
                    {
                        (width, height) = (height, width);
                    }
                    var area = new Rect
                    {
                        x = x,
                        y = y,
                        width = width,
                        height = height
                    };
                    // Rectangles Transform GameObject
                    var rectanglesTransform = board.transform.GetChild(1);
                    var storeRectanglesComponent = rectanglesTransform.GetComponent<StoreRectanglesComponent>();
                    var rectangle = new Models.Rectangle()
                    {
                        id = id,
                        area = area
                    };
                    storeRectanglesComponent.CreateRectangle(rectangle);
                }
            }
            if (storeBoardsComponent.Boards.Count > 1)
                storeBoardsComponent.Boards[0].SetActive(true);
        }
    }
}