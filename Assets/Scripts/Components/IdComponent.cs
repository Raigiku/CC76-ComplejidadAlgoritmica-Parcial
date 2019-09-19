using UnityEngine;
using TMPro;

namespace Complejidad.Components
{
    public class IdComponent : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text idText = null;

        public void ChangeIdText(string id)
        {
            idText.text = id;
        }
    }
}