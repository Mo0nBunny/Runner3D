using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour {

    [SerializeField] TextMeshProUGUI coinsCollected;

    void Start () {
        coinsCollected.text = "Coins:" + PlayerMovement.coinCounter;
    }
	
}
