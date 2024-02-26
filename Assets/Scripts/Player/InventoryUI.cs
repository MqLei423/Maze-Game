using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ShareefSoftware {

    public class InventoryUI : MonoBehaviour
    {
        private TextMeshProUGUI text;
        [SerializeField] PlayerInventory inventory;

        // Start is called before the first frame update
        void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            UpdateCoinAmt(inventory);
        }

        // Update is called once per frame
        void UpdateCoinAmt(PlayerInventory playerInventory)
        {
            text.text = "Coins Collected: " + playerInventory.coinAmt.ToString();
        }
    }
}
