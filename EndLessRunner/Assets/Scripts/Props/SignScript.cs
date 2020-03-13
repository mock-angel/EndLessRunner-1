using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignScript : MonoBehaviour
{
    public Text text;
    private int Distance = 0;
    private Vector2 signPos;
    public RectTransform rect;
    
    void Start(){
        text = gameObject.GetComponent<Text>();
        signPos = gameObject.transform.position;
//        rect = text.GetComponent<RectTransform>();
    }
    
    public void SetMeters(int distance){
        Distance = distance;
        text.text = "Reached\n" + distance+" m";
    }
    
    void Update(){
//        rect.anchoredPosition = (Camera.main.WorldToScreenPoint(signPos));
    }
}
