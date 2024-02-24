using UnityEngine;

namespace MengQiLei
{
    class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private float speed;

        private void Update()
        {
            transform.position = target.position;
            
        }
    }
}