using System.Collections.Generic;
using UnityEngine;

namespace Complejidad.Components
{
    public class StoreRectanglesComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject grid = null;

        [SerializeField]
        private GameObject rectanglePrefab = null;

        public List<Models.Rectangle> Rectangles { get; set; }

        private void Awake()
        {
            Rectangles = new List<Models.Rectangle>();
        }

        public void ClearRectangles()
        {
            Rectangles.Clear();
            GameObject[] rectangleObjects = GameObject.FindGameObjectsWithTag("Rectangle");
            foreach (GameObject rectangle in rectangleObjects)
            {
                Destroy(rectangle);
            }
        }

        public void CreateRectangles()
        {
            Models.Node node = new Models.Node();
            node.Id = 0;
            node.X = 0;
            node.Y = 0;
            node.Width = (int)grid.transform.localScale.x;
            node.Height = (int)grid.transform.localScale.y;

            foreach (var rectangle in Rectangles)
            {
                rectangle.area.x -= rectangle.area.width / 2f;
                rectangle.area.y -= rectangle.area.height / 2f;
                node.Insert(rectangle);
            }

            foreach (var rectangle in Rectangles)
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