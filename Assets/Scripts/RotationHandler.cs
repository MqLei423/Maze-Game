using ShareefSoftware;
using UnityEngine.InputSystem;

namespace ShareefSoftware
{
    public class RotationHandler
    {
        private SceneRotator Rotator;

        public RotationHandler(InputAction switchAction, SceneRotator Rotator)
        {
            switchAction.performed += SwitchAction_performed;
            switchAction.Enable();
            this.Rotator = Rotator;
        }

        private void SwitchAction_performed(InputAction.CallbackContext obj)
        {
            this.Rotator.ToggleRataion();
        }
    }
}