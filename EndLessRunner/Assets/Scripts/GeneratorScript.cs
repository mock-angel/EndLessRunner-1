using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    
    public GameObject player;
    public ThisGameData thisGameData;
    
    
    //Settlement assets.
    public List<GameObject> settlements;
    
    //Private assets.
//    [SerializeField]
    public int distance = 0;
    
    [HideInInspector]
    public GameObject previousSettlement;
    
    //Used while choosing new settlement.
    private Weight settlementWeightCalculator;
    private GameObject chosenSettlement;
    
    private List<GameObject> CapableSettlementsList;
    private List<GameObject> AllStartedSettlementList;
//    List<GameObject> AllSettlementsList;
    
    public int distancePerSign = 200;
    private int nextSignAtDistance = 0;
    private float prevDistanceProbed = 0;
    
    public GameObject signPrefab;
    private List<GameObject> signsList;
    
    void Start(){
//        AllSettlementsList = new List<GameObject>();
        signsList = new List<GameObject>();
        
        settlementWeightCalculator = new Weight();
        CapableSettlementsList = new List<GameObject>();
        AllStartedSettlementList = new List<GameObject>();
        CreateNewSettlement();
        
        nextSignAtDistance = distancePerSign;
    }
    
    void FixedUpdate(){
        
        //Calculate player distance;
        distance = (int)player.transform.position.x;
        thisGameData.distance = distance;
        
        
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
        
        //Clear Before start.
        CapableSettlementsList.Clear();
        settlementWeightCalculator.Clear();
        
        //Choose settlement code.
        for (int i = 0; i < count; i++){
            //Check if distance condition for settlement is met. 
            if ( settlements[i].GetComponent<Settlement>().checkAvailability(this) ){
                settlementWeightCalculator.Add(settlements[i].GetComponent<Settlement>().frequencyWeight);
                CapableSettlementsList.Add(settlements[i]);
            }
        }
        
        //chooses first building if list is empty.
        int index = settlementWeightCalculator.Pick();
        
//        //Clear for next use.
//        CapableSettlementsList.Clear();
//        settlementWeightCalculator.Clear();
        
        chosenSettlement = CapableSettlementsList[index];
    }

    void CreateNewSettlement(){
        ChooseNewSettlement();
        
        GameObject newSettlement = Instantiate(chosenSettlement, gameObject.transform.position, Quaternion.identity);
        Settlement newSettlementScript = newSettlement.GetComponent<Settlement>();
        
        //Set Settlement Transform.
        newSettlement.transform.parent = gameObject.transform;
        
        //Now let settlement start.
        newSettlementScript.parent = this;
        newSettlementScript.previousSettlement = previousSettlement;
        newSettlementScript.player = player;
        newSettlementScript.StartSettlement();
        
        previousSettlement = newSettlement;
        AllStartedSettlementList.Add(newSettlement);
    }
    
    //This code here is for sign.
    public void probeReach(Vector2 vec){
        
        //Add sign creation here after sign requirement check.
        if(vec.x > nextSignAtDistance){
            vec.x = nextSignAtDistance;
            vec.y += 1;
            
            GameObject newSign = Instantiate(signPrefab, vec, Quaternion.identity);
            signsList.Add(newSign);
            newSign.GetComponent<SignScript>().SetMeters((int)vec.x);
            
            nextSignAtDistance += distancePerSign;
        }
//        prevDistanceProbed = vec.x;
    }
}
