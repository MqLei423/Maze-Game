using UnityEngine;
using UnityEngine.InputSystem;

namespace ShareefSoftware
{
    public class MovementControl : MonoBehaviour
    {
        [SerializeField] private GameObject playerToMove;
        private float speed = 0.1f;
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
            Vector2 horizontalMovement = this.moveAction.ReadValue<Vector2>();
            Vector3 playerPos = this.playerToMove.transform.position;

            // Calculate the new position
            Vector3 newPosition = new Vector3(playerPos.x + horizontalMovement.x, playerPos.y, playerPos.z + horizontalMovement.y);

            // Move the player to the new position
            this.playerToMove.transform.position = Vector3.MoveTowards(playerPos, newPosition, speed);


        }
    }
}
