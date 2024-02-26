using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShareefSoftware
{
    public class PlayerInventory : MonoBehaviour
    {
        public int coinAmt { get; private set; }

        public void Collect()
        {
            coinAmt++;
        }
    }
 }
