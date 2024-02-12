using UnityEngine;
using UnityEngine.InputSystem;

namespace ShareefSoftware
{
    public class SceneRotator : MonoBehaviour
    {
        private float rotationSpeed = 5f; // Adjust the speed as needed
        private InputAction rotateAction;
        private bool isRotating;

        void Start()
        {
            isRotating = false;
        }

        public void ToggleRataion()
        {
            isRotating = !isRotating;
        }

        private void FixedUpdate()
        {
            // Rotate the entire scene around the y-axis if isRotating is true
            if (isRotating)
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
    }
}
