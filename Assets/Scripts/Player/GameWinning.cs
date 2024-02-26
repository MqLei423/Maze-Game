using UnityEngine;

namespace ShareefSoftware
{
    public class GameWinning : MonoBehaviour
    {
        public string text { get; private set; } = "";
        [SerializeField] GameOverReset sceneReset;

        private void OnTriggerEnter(Collider other)
        {
            text = "GAME OVER";
            Invoke("Reset", 3f);
        }

        private void Reset()
        {
            sceneReset.ResetScene();
        }
    }
}
