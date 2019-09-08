using UnityEngine;

namespace Complejidad.Systems
{
    public class MoveCameraSystem : MonoBehaviour
    {
        private Camera camera;

        private void Awake()
        {
            camera = GetComponent<Camera>();
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                float speed = camera.orthographicSize / 4f;
                transform.position += new Vector3(-Input.GetAxisRaw("Mouse X") * speed, -Input.GetAxisRaw("Mouse Y") * speed, 0f);
            }
            if (Input.mouseScrollDelta.y == -1f)
            {
                camera.orthographicSize += 1f;
            }
            else if (Input.mouseScrollDelta.y == 1f)
            {
                camera.orthographicSize -= 1f;
            }
        }
    }
}