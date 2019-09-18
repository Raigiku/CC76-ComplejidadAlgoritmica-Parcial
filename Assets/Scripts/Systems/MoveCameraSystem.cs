using UnityEngine;
using Complejidad.Components;
using UnityEngine.EventSystems;

namespace Complejidad.Systems
{
    public class MoveCameraSystem : MonoBehaviour
    {
        private Camera cameraComponent;

        private CameraSpeedComponent cameraSpeedComponent;

        [SerializeField]
        private Transform gridTransform;

        private void Awake()
        {
            cameraComponent = GetComponent<Camera>();
            cameraSpeedComponent = GetComponent<CameraSpeedComponent>();
        }

        void Update()
        {
            bool noUIcontrolsInUse = EventSystem.current.currentSelectedGameObject == null;
            if (noUIcontrolsInUse)
            {
                if (Input.GetMouseButton(0))
                {
                    float speed = cameraComponent.orthographicSize / 4f;
                    transform.position += new Vector3(-Input.GetAxisRaw("Mouse X") * speed, -Input.GetAxisRaw("Mouse Y") * speed, 0f);
                }
                if (Input.mouseScrollDelta.y == -1f)
                {
                    cameraComponent.orthographicSize += cameraSpeedComponent.Speed;
                }
                else if (Input.mouseScrollDelta.y == 1f && cameraComponent.orthographicSize - cameraSpeedComponent.Speed > 0)
                {
                    cameraComponent.orthographicSize -= cameraSpeedComponent.Speed;
                }
            }
        }
    }
}