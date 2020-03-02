using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayDistanceDisplay : MonoBehaviour
{
    public int distance;
    
    public ThisGameData thisGameData;
    
    private Text text;
    
    void Start(){
        text = gameObject.GetComponent<Text>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        distance = thisGameData.distance;
        text.text = "Distance \n" + distance;
    }
}
