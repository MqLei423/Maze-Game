using UnityEngine;
using TMPro;

namespace ShareefSoftware
{

    public class WinningTextDisplay : MonoBehaviour
    {
        private TextMeshProUGUI text;
        [SerializeField] GameWinning trigger;
        

        // Start is called before the first frame update
        void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            UpdateText(trigger);
        }

        // Update is called once per frame
        void UpdateText(GameWinning playerInventory)
        {
            text.text = trigger.text;
        }
    }
}
