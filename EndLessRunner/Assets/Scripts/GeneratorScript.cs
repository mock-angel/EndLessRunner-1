using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Tilemaps;

public class GeneratorScript : MonoBehaviour
{
    //Building assets.
    public GameObject roofTile;
    public GameObject buildingTile;
    
    public GameObject player;
    
    bool newBuldingRequired = true;
    
    void Start(){
        //Create start buildings.
    }
    
    void FixedUpdate(){
        //Check if more building placement is necessary.
        
        if(newBuldingRequired) {
            CreateNewBuilding();
            newBuldingRequired = false;
        }
    }
    void createTiles(){
        GameObject newObj = GameObject.Instantiate(roofTile, gameObject.transform.position, Quaternion.identity);
        newObj.transform.parent = gameObject.transform;
//        Destroy(newObj, 0.5f);
//        Instantiate(roofTile, gameObject.transform.position, Quaternion.identity, gameObject);
    }
    
    
    void CreateNewBuilding(){
        //Select Building type first.
        
        GameObject newBuilding = GameObject.Instantiate(new GameObject(), gameObject.transform.position, Quaternion.identity);
        GameObject newTile = GameObject.Instantiate(roofTile, gameObject.transform.position, Quaternion.identity);
//        newTile.alignment = (int)SpriteAlignment.TopLeft;
    }
    
}
