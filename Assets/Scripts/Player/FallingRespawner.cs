using System.Collections;
using UnityEngine;

namespace ShareefSoftware
{
    public class FallingRespawner : MonoBehaviour
    {
        [SerializeField] private GameObject targetPlayer;
        [SerializeField] private float RespawnHeight;
        private Vector3 initialPos;

        void Start()
        {
            initialPos = targetPlayer.transform.position;
            StartCoroutine(RespawnAfterFall());
        }

        private IEnumerator RespawnAfterFall()
        {
            // Have one meter tolerance so respawn won't be triggered by accident
            while (targetPlayer.transform.position.y > -1)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1f); // Wait for 1 second after falling below threshold
            Respawn();

            StartCoroutine(RespawnAfterFall());
        }

        public void Respawn()
        {
            targetPlayer.transform.position = new Vector3(initialPos.x, initialPos.y + RespawnHeight, initialPos.z);
        }
    }
}