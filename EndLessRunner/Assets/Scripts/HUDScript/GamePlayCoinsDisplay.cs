using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayCoinsDisplay : MonoBehaviour
{
    public int coins;
    
    public ThisGameData thisGameData;
    
    private Text text;
    
    void Start(){
        text = gameObject.GetComponent<Text>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        coins = thisGameData.coinsCollected;
        text.text = "Coins Collected \n" + coins;
    }
}
