using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportBackScript : MonoBehaviour {
 void Update(){
  if (Application.platform == RuntimePlatform.Android)
  {
   if (Input.GetKey(KeyCode.Escape))
   {
    Application.Quit();
   }
  }
 }
}
