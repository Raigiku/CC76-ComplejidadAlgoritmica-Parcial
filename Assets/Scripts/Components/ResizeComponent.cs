using UnityEngine;
using TMPro;

namespace Complejidad.Components
{
    public class ResizeComponent : MonoBehaviour
    {
        public void ChangeHeight(TMP_InputField inputField)
        {
            float y = 0f;
            float.TryParse(inputField.text, out y);
            transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
        }

        public void ChangeWidth(TMP_InputField inputField)
        {
            float x = 0f;
            float.TryParse(inputField.text, out x);
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        }
    }
}