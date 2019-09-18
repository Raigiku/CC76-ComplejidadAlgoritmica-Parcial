using System.Collections.Generic;
using UnityEngine;
using System;

namespace Complejidad.Components
{
    public class StoreRectanglesComponent : MonoBehaviour
    {
        [SerializeField]
        private CameraSpeedComponent cameraSpeedComponent;

        [SerializeField]
        private TextAsset textFile;

        [SerializeField]
        private GameObject grid;

        [SerializeField]
        private List<Models.Rectangle> rectangles = new List<Models.Rectangle>();

        [SerializeField]
        private GameObject rectanglePrefab;

        public void ReadFile()
        {
            string[] input_lines = textFile.text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // Change grid dimensions
            string[] dimensions = input_lines[0].Split(' ');
            float width = float.Parse(dimensions[0]);
            float height = float.Parse(dimensions[1]);
            ResizeComponent gridResizeComponent = grid.GetComponent<ResizeComponent>();
            gridResizeComponent.ChangeWidth(width);
            gridResizeComponent.ChangeHeight(height);

            // Add rectangles to List
            float total_rectangles = float.Parse(input_lines[1]);
            for (int i = 2; i < total_rectangles + 2; ++i)
            {
                string[] properties = input_lines[i].Split(' ');
                AddRectangle(properties[0], float.Parse(properties[1]), float.Parse(properties[2]));
            }

            // Initialize rectangles into world
            CreateRectangles();
        }

        public void ClearRectangles()
        {
            rectangles.Clear();
            GameObject[] rectangleObjects = GameObject.FindGameObjectsWithTag("Rectangle");
            for (int i = 0; i < rectangleObjects.Length; ++i)
            {
                Destroy(rectangleObjects[i]);
            }
        }

        private void AddRectangle(string id, float width, float height)
        {
            rectangles.Add(new Models.Rectangle
            {
                id = id,
                area = new Rect(0, 0, width, height)
            });
        }

        private void CreateRectangles()
        {
            Models.Node node = new Models.Node();
            node.Id = 0;
            node.X = 0;
            node.Y = 0;
            node.Width = (int)grid.transform.localScale.x;
            node.Height = (int)grid.transform.localScale.y;

            foreach (var rectangle in rectangles)
            {
                print($"x:{rectangle.area.x}");
                print($"y:{rectangle.area.y}");
                rectangle.area.x -= rectangle.area.width / 2f;
                rectangle.area.y -= rectangle.area.height / 2f;
                node.Insert(rectangle);
            }

            foreach (var rectangle in rectangles)
            {
                transform.position = -grid.transform.localScale / 2f;
                rectangle.area.x += rectangle.area.width / 2f;
                rectangle.area.y += rectangle.area.height / 2f;
                GameObject rectangleObject = Instantiate(rectanglePrefab, transform);
                rectangleObject.transform.localScale = rectangle.area.size;
                rectangleObject.transform.localPosition = rectangle.area.position;
                rectangleObject.GetComponent<IdComponent>().ChangeIdText(rectangle.id);
                rectangleObject.GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV();
            }
        }
    }
}