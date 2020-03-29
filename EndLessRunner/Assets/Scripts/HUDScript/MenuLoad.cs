using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoad : MonoBehaviour
{
    public Player player;
    
    public string preText = "Coins : ";
    
    private Text textObj;
    void Start()
    {
        textObj = gameObject.GetComponent<Text>();
        if(player != null){
            player.LoadPlayer();
            
            textObj.text = preText + player.Coins;
        } else
        {
            Debug.LogError("player not set");
        }
    }
    
    void Update(){
        textObj.text = preText + player.Coins;
    }
}
