using ShareefSoftware.Input;
using ShreefSoftware;
using UnityEngine;

namespace ShareefSoftware
{
    public class InputManager : MonoBehaviour
    {
        private RotationControl inputScheme;
        [SerializeField] private SceneRotator Rotator;

        private void Awake()
        {
            inputScheme = new RotationControl();
        }

        private void OnEnable()
        {
            var RotationHandler = new RotationHandler(inputScheme.SceneRotation.Rotation, this.Rotator);
        }
    }
}