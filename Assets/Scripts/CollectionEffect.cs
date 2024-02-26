using UnityEngine;

namespace ShareefSoftware
{
    public class CoinCollector : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        private bool isCollecting = false;


        private void Update()
        {
            if (isCollecting)
            {
                // Set a height for the coin to disappear, instead of using coroutine
                Vector3 disappPos = new Vector3(target.transform.position.x, 1f, target.transform.position.z);
                target.transform.position = Vector3.MoveTowards(target.transform.position, disappPos, 2 * Time.deltaTime);

                // Check if the coin has reached the disappering point
                if (Vector3.Distance(target.transform.position, disappPos) < 0.1f)
                {
                    target.SetActive(false);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            isCollecting = true;

            // Add coin to invetory
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null)
                inventory.Collect();
        }
    }
}
