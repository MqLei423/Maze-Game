using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private GameObject playerToMove;
    private InputAction input;
    [SerializeField]private float sensitivity = 20f;

    public void Initialize(InputAction input)
    {
        this.input = input;
        this.input.Enable();
        // This is to improve playing experience
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        // Calculate and rotate based on mouse input
        Vector2 mousePos = this.input.ReadValue<Vector2>();
        float x = mousePos.x * sensitivity * Time.deltaTime;
        float y = mousePos.y * sensitivity * Time.deltaTime;

        float verticleRotation = -y;
        verticleRotation = Mathf.Clamp(verticleRotation, -90f, 90f);

        Quaternion deltaRotation = Quaternion.Euler(sensitivity * verticleRotation, sensitivity * x, 0f);
        Quaternion target = playerToMove.transform.rotation * deltaRotation;
        playerToMove.transform.rotation = Quaternion.RotateTowards(playerToMove.transform.rotation, target, float.MaxValue);

        // Try to set off the z rotation resulted in by above rotation
        if (playerToMove.transform.rotation.z != 0)
        {
            Vector3 eulerRotation = playerToMove.transform.rotation.eulerAngles;
            playerToMove.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
        }

    }
}
