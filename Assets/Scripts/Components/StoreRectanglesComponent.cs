using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

            CreateRectangles();
        }

        private void CreateRectangles()
        {
            foreach (var rectangle in rectangles)
            {
                GameObject rectangleObject = Instantiate(rectanglePrefab, transform);
                rectangleObject.transform.localScale = rectangle.area.size;
                rectangleObject.GetComponent<LetterComponent>().ChangeLetterText(rectangle.letter);
            }
        }

        private void OnDisable()
        {
            createButton.onClick.RemoveAllListeners();
        }
    }
}