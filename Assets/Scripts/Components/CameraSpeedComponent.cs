using UnityEngine;
using UnityEngine.UI;

namespace Complejidad.Components
{
    public class CameraSpeedComponent : MonoBehaviour
    {
        [SerializeField]
        private Slider speedSlider;

        public float Speed { get; set; }

        private void OnEnable() {
            Speed = speedSlider.value;
            speedSlider.onValueChanged.AddListener((value) => ChangeSpeedSlider(value));
        }

        public void ChangeSpeedSlider(float value)
        {
            Speed = value;
        }

        private void OnDisable() {
            speedSlider.onValueChanged.RemoveAllListeners();
        }
    }
}