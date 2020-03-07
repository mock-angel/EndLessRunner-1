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
    
    public float distanceBetweenCoins = 1f;
    
    
    void Start(){
        //settlementScript = gameObject.GetComponent<Settlement>();
    }
    
    public void GenerateCoins(){
        
        GameObject prevBuilding = settlementScript.previousBuilding;
        if(prevBuilding != null){
            Vector2 vec = prevBuilding.GetComponent<Building>().rightMostPoint;//prevBuilding.transform.position;
            vec.y += 1;
            Instantiate(CoinsPrefab, vec, Quaternion.identity);
            
            float a = 48.02f;
            float b = 98f;
            float c = 46f;
            float e = -1f;
            PolynomialSolver.solve4(1, -7, 5, 31, -30);
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
