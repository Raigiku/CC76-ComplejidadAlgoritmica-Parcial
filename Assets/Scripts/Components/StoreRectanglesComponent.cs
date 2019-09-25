using UnityEngine;

namespace Complejidad.Components
{
    public class StoreRectanglesComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject grid = null;

        [SerializeField]
        private GameObject rectanglePrefab = null;

        public void CreateRectangle(Models.Rectangle rectangle)
        {
            transform.position = new Vector2(-grid.transform.localScale.x / 2f, grid.transform.localScale.y / 2f);
            GameObject rectangleObject = Instantiate(rectanglePrefab, transform);
            rectangleObject.transform.localScale = rectangle.area.size;
            var offsetPosition = new Vector2(
                rectangle.area.position.x + rectangle.area.width / 2f,
                -(rectangle.area.position.y + rectangle.area.height / 2f)
            );
            rectangleObject.transform.localPosition = offsetPosition;
            rectangleObject.GetComponent<IdComponent>().ChangeIdText(rectangle.id);
            rectangleObject.GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV();
        }
    }
}