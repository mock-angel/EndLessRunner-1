using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Tilemaps;

public class GeneratorScript : MonoBehaviour
{
    //Building assets.
    public GameObject player;
    
    public List<GameObject> buildings;
    
    //Private assets.
    bool newBuldingRequired = true;
    [SerializeField]
    float distanceTravelled = 0f;
    Building tempBuildingScript;
    GameObject tempSelectedBuilding;
    float totalFrequencyWeight = 0;
    
    void FixedUpdate(){
        //Check if more building placement is necessary.
        
        if(newBuldingRequired) {
            CreateNewBuilding();
            newBuldingRequired = false;
        }
    }
    
    void CalculateWeight(){
        int count = buildings.Count;
        int totalFrequencyWeight = 0;
        
        List<int> frequencyWeightList = new List<int>();
        
        int currentFrequency;
        
        for (int i = 0; i < count; i++){
            currentFrequency = buildings[i].GetComponent<Building>().frequencyWeight;
            totalFrequencyWeight += currentFrequency;
            frequencyWeightList.Add(currentFrequency);
        }
        
        int selectedWeight = Random.Range(1, totalFrequencyWeight + 1);
        
        totalFrequencyWeight = 0;
        for (int i = 0; i < count; i++){
            totalFrequencyWeight += frequencyWeightList[i];
            if (totalFrequencyWeight >= selectedWeight){
                //Now building matches selected weight.
                //select building.
                tempSelectedBuilding = buildings[i];
                return;
            }
            
            //If none selected.
            if (i == count -1){
                print("Now buildings were selected.");
            }
        }
    }
    
    void CreateNewBuilding(){
        
        //Select buildings based on their weight.
        CalculateWeight();
        
        //Select Building type first.
        GameObject newBuilding = GameObject.Instantiate(tempSelectedBuilding, gameObject.transform.position, Quaternion.identity);
        tempBuildingScript = newBuilding.GetComponent<Building>();
        Vector2 position = newBuilding.transform.position;
        
        //SetBuilding Transform.
        newBuilding.transform.parent = gameObject.transform;
        
        //Now let building create its contents.
        tempBuildingScript.Parent = gameObject;
        tempBuildingScript.CreateContent();
        
        
        
    }
    
}
