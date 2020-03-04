using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyAttributes;

public class CoinGenerator : MonoBehaviour
{
    public GameObject CoinsPrefab;
    
    private CoinGenerator coinGenerator;
    
    public bool spawnCoins = true;
    [ConditionalField("spawnCoins")]
    public int minCoinsSpawnCount = 0;
    [ConditionalField("spawnCoins")]
    public int maxCoinsSpawnCount = 10; 
    
    public Settlement settlementScript;
    
    void Start(){
        //settlementScript = gameObject.GetComponent<Settlement>();
    }
    
    public void GenerateCoins(){
        
        GameObject prevBuilding = settlementScript.previousBuilding;
        if(prevBuilding != null){
            Vector2 vec = prevBuilding.GetComponent<Building>().rightMostPoint;//prevBuilding.transform.position;
            vec.y += 1;
            Instantiate(CoinsPrefab, vec, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
