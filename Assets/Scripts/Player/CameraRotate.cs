using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private GameObject playerToMove;
    private InputAction input;
    private float speed = 10f;

    public void Initialize(InputAction input)
    {
        this.input = input;
        this.input.Enable();
    }

    private void FixedUpdate()
    {
        Vector2 mousePos = this.input.ReadValue<Vector2>();
        mousePos *= speed;
        Quaternion deltaRotation = Quaternion.Euler(-mousePos.y, mousePos.x, 0f);
        Quaternion target = playerToMove.transform.rotation * deltaRotation;
        playerToMove.transform.rotation = Quaternion.RotateTowards(playerToMove.transform.rotation, target, speed);
    }
}
