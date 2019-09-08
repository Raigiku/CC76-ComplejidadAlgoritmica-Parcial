using UnityEngine;
using TMPro;

namespace Complejidad.Components
{
    public class LetterComponent : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text letterText;

        public void ChangeLetterText(char letter)
        {
            letterText.text = letter.ToString();
        }
    }
}