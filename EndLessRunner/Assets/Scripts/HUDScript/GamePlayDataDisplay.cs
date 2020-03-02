using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayDataDisplay : MonoBehaviour
{
    public int distance;
    public int coins;
    public ThisGameData thisGameData;
    
    public Text distanceText;
    public Text coinsText;
    
    void FixedUpdate()
    {
        distance = thisGameData.distance;
        distanceText.text = "Distance \n" + distance + " m";
        coins = thisGameData.coinsCollected;
        coinsText.text = "Coins Collected \n" + coins;
    }
}
