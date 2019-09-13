using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Complejidad.Models;

namespace Complejidad.Components
{
    public class StoreRectanglesComponent : MonoBehaviour
    {
        [SerializeField]
        private List<Models.Rectangle> rectangles = new List<Models.Rectangle>();
        [SerializeField]
        private Button createButton;
        [SerializeField]
        private TMP_InputField widthInput;
        [SerializeField]
        private TMP_InputField heightInput;
        [SerializeField]
        private TMP_InputField letterInput;
        [SerializeField]
        private GameObject rectanglePrefab;
        [SerializeField]
        private Transform gridTransform;

        private void OnEnable()
        {
            createButton.onClick.AddListener(() => AddRectangle());
        }

        public void AddRectangle()
        {
            float width = 0f;
            float.TryParse(widthInput.text, out width);
            float height = 0f;
            float.TryParse(heightInput.text, out height);
            char? letter = string.IsNullOrEmpty(letterInput.text) ? (char?)null : letterInput.text[0];

            if (width > 0f && height > 0f && letter != null)
            {
                rectangles.Add(new Models.Rectangle
                {
                    letter = letter.Value,
                    area = new Rect(0, 0, width, height)
                });
            }
        }

        public void CreateRectangles()
        {
            Node node = new Node();
            node.Id = 0;
            node.X = 0;
            node.Y = 0;
            node.Width = (int)gridTransform.localScale.x;
            node.Height = (int)gridTransform.localScale.y;

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
                transform.position = -gridTransform.localScale / 2f;
                rectangle.area.x += rectangle.area.width / 2f;
                rectangle.area.y += rectangle.area.height / 2f;
                GameObject rectangleObject = Instantiate(rectanglePrefab, transform);
                rectangleObject.transform.localScale = rectangle.area.size;
                rectangleObject.transform.localPosition = rectangle.area.position;
                rectangleObject.GetComponent<LetterComponent>().ChangeLetterText(rectangle.letter);
                rectangleObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            }
        }

        private void OnDisable()
        {
            createButton.onClick.RemoveAllListeners();
        }
    }
}