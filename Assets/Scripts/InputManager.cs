using ShareefSoftware.Input;
using ShareefSoftware;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace ShareefSoftware
{
    public class InputManager : MonoBehaviour
    {
        private Controls inputScheme;
        [SerializeField] private SceneRotator Rotator;
        [SerializeField] private MovementControl movementController;
        [SerializeField] private CameraRotate cameraRotate;

        private RotationHandler rotationHandler; // Store a reference to RotationHandler

        private void Awake()
        {
            inputScheme = new Controls();
            movementController.Initialize(inputScheme.Maze.Move, inputScheme.Maze.CameraRotation, inputScheme.Maze.Sprint);
            cameraRotate.Initialize(inputScheme.Maze.CameraRotation);
        }

        private void OnEnable()
        {
            // Assign Rotator before creating RotationHandler
            rotationHandler = new RotationHandler(inputScheme.Maze.Rotation, this.Rotator);
        }
    }
}
