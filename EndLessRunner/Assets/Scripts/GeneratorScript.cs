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
    
    public List<GameObject> buildings;
    
    //Private assets.
    bool newBuldingRequired = true;
    [SerializeField]
    float distanceTravelled = 0f;
    Building tempBuilding;
    
    void FixedUpdate(){
        //Check if more building placement is necessary.
        
        if(newBuldingRequired) {
            CreateNewBuilding();
            newBuldingRequired = false;
        }
    }
    
    void CreateNewBuilding(){
        //Select Building type first.
        
        GameObject newBuilding = GameObject.Instantiate(buildings[0], gameObject.transform.position, Quaternion.identity);
        tempBuilding = newBuilding.GetComponent<Building>();
        Vector2 position = newBuilding.transform.position;
        
        //SetBuilding Transform.
        newBuilding.transform.parent = gameObject.transform;
        
        //Now let building create its contents.
        tempBuilding.Parent = gameObject;
        tempBuilding.CreateContent();
        
        
        
    }
    
}
