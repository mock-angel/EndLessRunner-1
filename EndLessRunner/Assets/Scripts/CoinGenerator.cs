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
    public Building buildingScript;
    public float distanceBetweenCoins = 1f;
    
    
    void Start(){
        //settlementScript = gameObject.GetComponent<Settlement>();
    }
    
    public void GenerateCoins(){
        
        GameObject prevBuilding = settlementScript.previousBuilding;
        if(prevBuilding != null){
            Building buildingScript = prevBuilding.GetComponent<Building>();
            Vector2 RightMostPoint = buildingScript.rightMostPoint;//prevBuilding.transform.position;
            Vector2 vec = RightMostPoint;
            vec.y += 1;
            
            Vector2 LeftMostPoint = buildingScript.leftMostPoint;
            Vector2 prevRightMostPoint = buildingScript.prevRightMostPoint;
            
            Instantiate(CoinsPrefab, vec, Quaternion.identity);
            
            
            
//            float a = 48.02f;
//            float b = -98f;
//            float c = 46f;
//            float e = -1f;
//            PolynomialSolver.solve4(a, b, c, 0, e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
//int P = 8ac - 3b*b
//int R = b*b*b - 4*a*b*c
//
