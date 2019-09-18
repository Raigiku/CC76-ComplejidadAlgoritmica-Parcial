using UnityEngine;

namespace Complejidad.Components
{
    public class ResizeComponent : MonoBehaviour
    {
        public void ChangeHeight(float y)
        {
            transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
        }

        public void ChangeWidth(float x)
        {
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        }
    }
}