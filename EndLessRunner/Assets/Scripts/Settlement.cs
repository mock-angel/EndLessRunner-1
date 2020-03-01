using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour
{
    public int frequencyWeight = 1;
    public int reachDistance = 0;
    public int numberOfBuildings = 5;
    public List<GameObject> buildings;
    
    [HideInInspector]
    public GameObject parent;
    [HideInInspector]
    public GameObject player;
    
    [HideInInspector]
    public GameObject previousSettlement;
    
    public int minBuildingCount = 6;
    public int maxBuildingCount = 10;
    
    //TODO: rename all Building variables of game object to BuildingObj.
    [HideInInspector]
    public GameObject previousBuilding; // TODO: rename to prevBuilding
    private bool finalBuildingCreated;
    
    private bool finishedLayingBuildings = false;
    [HideInInspector]
    public bool allBuildingsDeleted = false;
    
    private Weight buildingWeightCalculator;
    private GameObject toCreateBuilding;
    
    private List<GameObject> AllCreatedBuildingsList;
    
    private int buildingCount = 0;
    private int buildingCountLimit;
    
    public void StartSettlement(){
        AllCreatedBuildingsList = new List<GameObject>();
        
        buildingWeightCalculator = new Weight();
        
        buildingCountLimit = Random.Range(minBuildingCount, maxBuildingCount);
        
        if(previousSettlement != null)
            previousBuilding = previousSettlement.GetComponent<Settlement>().previousBuilding;
        CreateNewBuilding();
    }
    
    void FixedUpdate()
    {
        //Check if more building placement is necessary and remove unnecessary ones.
        
        if(allBuildingsDeleted) return;
        
        Vector2 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 upperRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        //Remove Buildings that went outside of view.
        if(allBuildingsDeleted) return;
        if(AllCreatedBuildingsList[0].GetComponent<Building>().rightMostPoint.x < lowerLeft.x){
            GameObject firstBuilding = AllCreatedBuildingsList[0];
            AllCreatedBuildingsList.RemoveAt(0);
            Destroy(firstBuilding);
            if(AllCreatedBuildingsList.Count == 0) allBuildingsDeleted = true;
        }
        
        // Create new buildings.
        if(!finishedLayingBuildings && previousBuilding.GetComponent<Building>().rightMostPoint.x < upperRight.x)
             CreateNewBuilding();
            
    }
    
    public bool Finished(){
        if(finishedLayingBuildings == true)
            return true;
        else return false;
    }
    
    void ChooseNewBuilding(){
        int count = buildings.Count;
        
        //choose building.
        for (int i = 0; i < count; i++){
            //Check if distance condition for building is met. 
//            if ( distance >= buildings.GetComponent<Building>().reachDistance ) //bypass this check for now.
                buildingWeightCalculator.Add(buildings[i].GetComponent<Building>().frequencyWeight);
        }
        
        int index = buildingWeightCalculator.Pick();
        
        //Clear calculator for next use.
        buildingWeightCalculator.Clear();
        
        toCreateBuilding = buildings[index];
    }
    
    void CreateNewBuilding(){
        ChooseNewBuilding();
        
        GameObject newBuilding = GameObject.Instantiate(toCreateBuilding, gameObject.transform.position, Quaternion.identity);
        Building newBuildingScript = newBuilding.GetComponent<Building>();
        
        Vector2 position = newBuilding.transform.position;
        
        //Set Building Transform.
        newBuilding.transform.parent = gameObject.transform;
        
        //Now let building create its contents.
        newBuildingScript.Parent = gameObject;
        newBuildingScript.previousBuilding = previousBuilding;
        newBuildingScript.Player = player; //TODO: Change from Player to player.
        newBuildingScript.CreateContent();
        
        previousBuilding = newBuilding;
        
        //Finally add it to building list, to handle deletion.
        AllCreatedBuildingsList.Add(newBuilding);
        
        buildingCount++;
        
        if(buildingCount >= buildingCountLimit)
            finishedLayingBuildings = true;
    }
}
