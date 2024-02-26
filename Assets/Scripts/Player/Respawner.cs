using UnityEngine;

namespace ShareefSoftware
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField] private GameObject targetPlayer;
        private Vector3 initialPos;

        void Start()
        {
            initialPos = targetPlayer.transform.position;
        }

        public void Respawn()
        {
            targetPlayer.transform.position = initialPos;
        }
    }
}