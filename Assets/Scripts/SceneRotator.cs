using UnityEngine;
using UnityEngine.InputSystem;

namespace ShareefSoftware
{
    public class SceneRotator : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed; // Adjust the speed as needed
        private bool isRotating;
        [SerializeField] GameObject rotatedTarget;
        private Vector3 centerOfRotaion;

        void Start()
        {
            isRotating = false;
        }

        public void ToggleRataion()
        {
            isRotating = !isRotating;
            centerOfRotaion = CalculateCenterPoint();
        }

        private Vector3 CalculateCenterPoint()
        {
            var rendererComponents = rotatedTarget.GetComponentsInChildren<Renderer>();
            Bounds bounds = new Bounds(rotatedTarget.transform.position, Vector3.zero);

            foreach (Renderer child in rendererComponents)
            {
                bounds.Encapsulate(child.bounds);
            }

            return bounds.center;
        }

        private void Update()
        {
            // Rotate the entire scene around the y-axis if isRotating is true
            if (isRotating)
            {
                rotatedTarget.transform.RotateAround(centerOfRotaion, Vector3.up, rotationSpeed);
            }
        }
    }
}
