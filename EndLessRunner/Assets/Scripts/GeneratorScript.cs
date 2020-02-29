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
    [SerializeField]
    float distanceTravelled = 0f;
    Building tempBuildingScript;
    GameObject tempSelectedBuilding;
    Building tempPreviousBuildingScript;
    
    [SerializeField]
    int totalFrequencyWeight = 0;
    [SerializeField]
    int selectedWeight = 0;
    
    GameObject previousBuilding;
    List<GameObject> AllBuildingsList;
    
    void Start(){
        AllBuildingsList = new List<GameObject>();
        
        CreateNewBuilding();
    }
    
    void FixedUpdate(){
        //Check if more building placement is necessary.
        
        Vector2 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 upperRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        //Remove Buildings that went outside of view.
        if(AllBuildingsList[0].GetComponent<Building>().rightMostPoint.x < lowerLeft.x){
            GameObject firstBuilding = AllBuildingsList[0];
            AllBuildingsList.RemoveAt(0);
            Destroy(firstBuilding);
        }
        
        // Create new buildings.
        tempPreviousBuildingScript = previousBuilding.GetComponent<Building>();
        if(tempPreviousBuildingScript.rightMostPoint.x < upperRight.x)
             CreateNewBuilding();
        
    }
    
    void CalculateWeight(){
        int count = buildings.Count;
        
        List<int> frequencyWeightList = new List<int>();
        
        int tempFrequency;
        
        //Calculate total weight, and store all weights in list.
        for (int i = 0; i < count; i++){
            tempFrequency = buildings[i].GetComponent<Building>().frequencyWeight;
            totalFrequencyWeight += tempFrequency;
            frequencyWeightList.Add(tempFrequency);
        }
        
        //Randomly Select weight.
        selectedWeight = Random.Range(1, totalFrequencyWeight + 1);
        
        //This is where we select the building based on their weight;
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
        tempBuildingScript.previousBuilding = previousBuilding;
        tempBuildingScript.CreateContent();
        
        previousBuilding = newBuilding;
        
        //Finally add it to building list, to handle deletion.
        AllBuildingsList.Add(newBuilding);
    }
    
}
