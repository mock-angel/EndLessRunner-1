using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using TMPro;

public class ShowFPS : MonoBehaviour {
    public TextMeshProUGUI fpsText;
    private float deltaTime;
    public float secTime = 0;
    public float nextSecTime = 0;
    
    void Update () {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
         
         secTime+=Time.deltaTime;
         
        if(secTime<nextSecTime){
            return;
        }
        nextSecTime = Mathf.Ceil (secTime);
        
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil (fps).ToString ();
    }
}

