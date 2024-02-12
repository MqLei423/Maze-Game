using UnityEngine;
using UnityEngine.InputSystem;

namespace ShareefSoftware
{
    public class SceneRotator : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed; // Adjust the speed as needed
        private bool isRotating;
        [SerializeField] GameObject maze;
        private Vector3 centerOfRotaion;

        void Start()
        {
            isRotating = false;
        }

        public void ToggleRataion()
        {
            isRotating = !isRotating;
            Debug.Log("Rotation Toggled");
        }

        private void Update()
        {
            // Rotate the entire scene around the y-axis if isRotating is true
            if (isRotating)
            {
                maze.transform.Rotate(0, rotationSpeed, 0);
            }
        }
    }
}
