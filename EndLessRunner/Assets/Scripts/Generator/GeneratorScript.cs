using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    
    public GameObject player;
    public ThisGameData thisGameData;
    
    [System.Serializable]
    public class SettlementsMeta
    {
        public string name;
        public GameObject prefab;
        public ObjectPooler objectPooler;
    }
    
    //Settlement assets.
    public List<GameObject> settlements;
    
     //Settlement assets.
    public List<SettlementsMeta> settlementsMeta;
    
    //Private assets.
//    [SerializeField]
    public int distance = 0;
    
    [HideInInspector]
    public GameObject previousSettlement;
    
    //Used while choosing new settlement.
    private Weight settlementWeightCalculator;
    private SettlementsMeta chosenSettlementMeta;
    
    private List<SettlementsMeta> CapableSettlementsMetaList;
    private List<GameObject> AllStartedSettlementList;
//    List<GameObject> AllSettlementsList;
    
    public int distancePerSign = 200;
    private int nextSignAtDistance = 0;
    private float prevDistanceProbed = 0;
    
    public GameObject signPrefab;
    private List<GameObject> signsList;
    
    private float nextSpeedBuffAtDistance = 100f;
    public float IncreaseDistance = 200f;
    public float MaxSpeed = 20;
    public float SpeedIncreasePer = 1f;
    
    void Start(){
//        AllSettlementsList = new List<GameObject>();
        signsList = new List<GameObject>();
        
        settlementWeightCalculator = new Weight();
        CapableSettlementsMetaList = new List<SettlementsMeta>();
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
        
        
        //Check for speed buff.
        if(distance >= nextSpeedBuffAtDistance){
            player.GetComponent<PlayerMovement>().runSpeed += SpeedIncreasePer;
            nextSpeedBuffAtDistance += IncreaseDistance;
            if(MaxSpeed <= player.GetComponent<PlayerMovement>().runSpeed)
                player.GetComponent<PlayerMovement>().runSpeed = MaxSpeed;
        }
    }
    
    void ChooseNewSettlement(){
        int count = settlementsMeta.Count;
        
        //Clear Before start.
        CapableSettlementsMetaList.Clear();
        settlementWeightCalculator.Clear();
        
        //Choose settlement code.
        for (int i = 0; i < count; i++){
            //Check if distance condition for settlement is met. 
            if ( settlementsMeta[i].prefab.GetComponent<Settlement>().checkAvailability(this) ){
                settlementWeightCalculator.Add(settlementsMeta[i].prefab.GetComponent<Settlement>().frequencyWeight);
                CapableSettlementsMetaList.Add(settlementsMeta[i]);
            }
        }
        
        //chooses first building if list is empty.
        int index = settlementWeightCalculator.Pick();
        
//        //Clear for next use.
//        CapableSettlementsList.Clear();
//        settlementWeightCalculator.Clear();
        
        chosenSettlementMeta = CapableSettlementsMetaList[index];
    }

    void CreateNewSettlement(){
        ChooseNewSettlement();
        
        GameObject newSettlement = Instantiate(chosenSettlementMeta.prefab, gameObject.transform.position, Quaternion.identity);
        Settlement newSettlementScript = newSettlement.GetComponent<Settlement>();
        
        //Set Settlement Transform.
        newSettlement.transform.parent = gameObject.transform;
        
        //Now let settlement start.
        newSettlementScript.parent = this;
        newSettlementScript.previousSettlement = previousSettlement;
        newSettlementScript.player = player;
        newSettlementScript.objectPooler = chosenSettlementMeta.objectPooler;
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
