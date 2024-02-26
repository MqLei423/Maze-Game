using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField]private GameObject target;
    private float minHeight;
    private float maxHeight;
    private bool goingUp;

    private void Start()
    {
        minHeight = target.transform.position.y;
        maxHeight = minHeight + 0.2f;
        goingUp = true;
    }

    void Update()
    {
        // Spin the object around the target at 20 degrees/second.
        transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);

        // Make the object float up and down
        if (target.transform.position.y >= maxHeight)
            goingUp = false;
        else if (target.transform.position.y <= minHeight)
            goingUp = true;

        Vector3 newPos;
        if (goingUp) newPos = new Vector3(target.transform.position.x, target.transform.position.y + 0.1f, target.transform.position.z);
        else newPos = new Vector3(target.transform.position.x, target.transform.position.y - 0.1f, target.transform.position.z);

        target.transform.position = Vector3.MoveTowards(target.transform.position, newPos, 0.1f * Time.deltaTime);
    }
}