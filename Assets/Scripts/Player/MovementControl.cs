using UnityEngine;
using UnityEngine.InputSystem;

namespace ShareefSoftware
{
    public class MovementControl : MonoBehaviour
    {
        [SerializeField] private GameObject playerToMove;
        private float speed = 0.1f;
        private float sprintSpd = 0.2f;
        private InputAction moveAction;
        private InputAction rotation;

        public void Initialize(InputAction moveAction, InputAction rotation)
        {
            this.moveAction = moveAction;
            this.moveAction.Enable();
            this.rotation = rotation;
            this.rotation.Enable();
        }

        private void FixedUpdate()
        {
            Vector2 movementInput = this.moveAction.ReadValue<Vector2>();
            Vector3 playerPos = this.playerToMove.transform.position;

            // Get the player's current rotation
            Quaternion playerRotation = this.playerToMove.transform.rotation;

            // Transform the input movement from local to world space
            Vector3 movement = playerRotation * new Vector3(movementInput.x, 0f, movementInput.y);

            // Calculate the new position
            Vector3 newPosition = playerPos + movement * speed;
            newPosition.y = playerPos.y;

            // Move the player to the new position
            this.playerToMove.transform.position = Vector3.MoveTowards(playerPos, newPosition, speed);
        }
    }
}
