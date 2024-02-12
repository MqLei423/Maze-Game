using ShareefSoftware.Input;
using ShareefSoftware;
using UnityEngine;

namespace ShareefSoftware
{
    public class InputManager : MonoBehaviour
    {
        private RotationControl inputScheme;
        [SerializeField] private SceneRotator Rotator;

        private RotationHandler rotationHandler; // Store a reference to RotationHandler

        private void Awake()
        {
            inputScheme = new RotationControl();
        }

        private void OnEnable()
        {
            // Assign Rotator before creating RotationHandler
            rotationHandler = new RotationHandler(inputScheme.SceneRotation.Rotation, this.Rotator);
        }
    }
}
