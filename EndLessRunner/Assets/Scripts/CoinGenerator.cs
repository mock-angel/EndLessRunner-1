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
            //Get the previous building's Building Script component.
            Building buildingScript = prevBuilding.GetComponent<Building>();
            
            //Get all required variables from buildingScript.
            Vector2 prevRightMostSpawnPoint = buildingScript.prevRightMostPoint;
            Vector2 LeftMostSwawnPoint = buildingScript.leftMostPoint;
            Vector2 RightMostSpawnPoint = buildingScript.rightMostPoint;//prevBuilding.transform.position;
            
            /************************Jump CoinGenerator*************************/
            float jumpDistance = LeftMostSwawnPoint.x - prevRightMostSpawnPoint.x;
            
            float runSpeed = settlementScript.player.GetComponent<PlayerMovement>().runSpeed;
            float jumpVelocity = settlementScript.player.GetComponent<PlayerJump>().jumpVelocity;
            float gScale = (settlementScript.player.GetComponent<Rigidbody2D>().gravityScale)*(-9.81f);
            
            Vector2 vec = prevRightMostSpawnPoint;
            
            //Modify spawn point.
            prevRightMostSpawnPoint.x -= buildingScript.tileSize.x/2f;
            LeftMostSwawnPoint.x -= buildingScript.tileSize.x/2f;
            RightMostSpawnPoint.x -= buildingScript.tileSize.x/2f;
            
            //Create new empty gameobject inside Building.
            GameObject CoinsOBJ = new GameObject();//Instantiate(, new Vector2(0, 0), Quaternion.identity);
            CoinsOBJ.transform.parent = prevBuilding.transform;
            CoinsOBJ.name = "All Coins";
            
            float x = 1;
            float t = x/runSpeed;
            float y = 0;
            for(int i = 0; i < 20; i++){
                y = jumpVelocity*t + 0.5f*gScale*t*t;
                vec.x = prevRightMostSpawnPoint.x + x;
                vec.y = prevRightMostSpawnPoint.y + y;
                
                Instantiate(CoinsPrefab, vec, Quaternion.identity).transform.parent = CoinsOBJ.transform;
                
                x += 1f;
                t = x/runSpeed;
                
                if(x >= jumpDistance) break;
            }
            
            /***********************Line CoinGenerator************************/
            
            
            
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
