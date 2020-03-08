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
    public GameObject building;
    public Building buildingScript;
    public float distanceBetweenCoins = 1f;
    
    //All of these are to be set from within GenerateCoins() method
    private GameObject prevBuilding;
    
    private Vector2 prevRightMostSpawnPoint;
    private Vector2 LeftMostSwawnPoint;
    private Vector2 RightMostSpawnPoint;//prevBuilding.transform.position;
    
    public void GenerateCoins(){
        
//        prevBuilding = settlementScript.previousBuilding;
        
        if(buildingScript != null){
            
            //Get all required variables from buildingScript.
            prevRightMostSpawnPoint = buildingScript.prevRightMostPoint;
            LeftMostSwawnPoint = buildingScript.leftMostPoint;
            RightMostSpawnPoint = buildingScript.rightMostPoint;//prevBuilding.transform.position;
            
            //Modify spawn point.
            prevRightMostSpawnPoint.x -= buildingScript.tileSize.x/2f;
            LeftMostSwawnPoint.x -= buildingScript.tileSize.x/2f;
            RightMostSpawnPoint.x -= buildingScript.tileSize.x/2f;
            GenerateCornerJumpCoins(20);
        }
    }
    
    int GenerateCornerJumpCoins(int numberOfCoins){
        /************************Jump CoinGenerator*************************/
        float jumpDistance = LeftMostSwawnPoint.x - prevRightMostSpawnPoint.x;
        
        float runSpeed = settlementScript.player.GetComponent<PlayerMovement>().runSpeed;
        float jumpVelocity = settlementScript.player.GetComponent<PlayerJump>().jumpVelocity;
        float gScale = (settlementScript.player.GetComponent<Rigidbody2D>().gravityScale)*(-9.81f);
        
        Vector2 vec = prevRightMostSpawnPoint;
        
        //Create new empty gameobject inside Building.
        GameObject CoinsOBJ = new GameObject();//Instantiate(, new Vector2(0, 0), Quaternion.identity);
        CoinsOBJ.transform.parent = building.transform;
        CoinsOBJ.name = "All Coins";
        
        float x = 1;
        float t = x/runSpeed;
        float y = 0;
        int i = 0;
        for(; i < numberOfCoins; i++){
            y = jumpVelocity*t + 0.5f*gScale*t*t;
            vec.x = prevRightMostSpawnPoint.x + x;
            vec.y = prevRightMostSpawnPoint.y + y;
            
            Instantiate(CoinsPrefab, vec, Quaternion.identity).transform.parent = CoinsOBJ.transform;
            
            //Update next x component.
            x += 1f;
            t = x/runSpeed;
            
            //Validation.
            if(x >= jumpDistance) break;
        }
        return i;
        /***********************Line CoinGenerator************************/
    }
    
    int GenerateLineJumpCoins(int numberOfCoins){
        
        return 0;
    }
}
