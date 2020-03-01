using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    
    public GameObject player;
    
    //Settlement assets.
    public List<GameObject> settlements;
    
    //Private assets.
    [SerializeField]
    float distance = 0f;
    
    [HideInInspector]
    public GameObject previousSettlement;
    
    //Used while choosing new settlement.
    private Weight settlementWeightCalculator;
    private GameObject chosenSettlement;
    
    private List<GameObject> AllStartedSettlementList;
//    List<GameObject> AllSettlementsList;

    void Start(){
//        AllSettlementsList = new List<GameObject>();
        
        settlementWeightCalculator = new Weight();
        AllStartedSettlementList = new List<GameObject>();
        CreateNewSettlement();
    }
    
    void FixedUpdate(){
        //Check if settlement needs to be destroyed.
        //code here.
        
        if(AllStartedSettlementList[0].GetComponent<Settlement>().allBuildingsDeleted){
            GameObject firstSettlement = AllStartedSettlementList[0];
            AllStartedSettlementList.RemoveAt(0);
            Destroy(firstSettlement);
        }
        
        //Create new settlements.
        if(previousSettlement.GetComponent<Settlement>().Finished())
            CreateNewSettlement();
    }
    
    void ChooseNewSettlement(){
        int count = settlements.Count;
        
        //Choose settlement code.
        for (int i = 0; i < count; i++){
            //Check if distance condition for settlement is met. 
            if ( distance >= settlements[i].GetComponent<Settlement>().reachDistance )
                settlementWeightCalculator.Add(settlements[i].GetComponent<Settlement>().frequencyWeight);
        }
        
        int index = settlementWeightCalculator.Pick();
        
        //Clear calculator for next use.
        settlementWeightCalculator.Clear();
        
        chosenSettlement = settlements[index];
    }

    void CreateNewSettlement(){
        ChooseNewSettlement();
        
        GameObject newSettlement = Instantiate(chosenSettlement, gameObject.transform.position, Quaternion.identity);
        Settlement newSettlementScript = newSettlement.GetComponent<Settlement>();
        
        //Set Settlement Transform.
        newSettlement.transform.parent = gameObject.transform;
        
        //Now let settlement start.
        newSettlementScript.parent = gameObject;
        newSettlementScript.previousSettlement = previousSettlement;
        newSettlementScript.player = player;
        newSettlementScript.StartSettlement();
        
        previousSettlement = newSettlement;
        AllStartedSettlementList.Add(newSettlement);
    }
}
