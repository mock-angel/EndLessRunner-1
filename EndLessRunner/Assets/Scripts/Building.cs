using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Range(1, 50)]
    public int minimumTilesCount = 3;
    [Range(1, 50)]
    public int maximumTilesCount = 8;
    
    public GameObject roofCornerLeft;
    public GameObject roofMiddle;
    
    [Range(0, 50)]
    public int frequencyWeight = 5;
    
    public List<GameObject> windowPrefabs;
    
    public GameObject Player;
    public GameObject Parent;
    
    public GameObject previousBuilding;
    
    public Vector2 rightMostPoint;
    public Vector2 leftMostPoint;
    public Vector2 prevRightMostPoint;
    
    public LayerMask groundLayer;
    
    public int ID = 0;
    
    //[HideInInspector]
    //public List<GameObject> AllTilesList;
    
    //Private use.
    int tileCount;
    List<GameObject> AllTiles  = new List<GameObject>();
    
//    void Start(){
//        AllTiles = new List<GameObject>();
//    }
    
    public GeneratorScript mainGenerator;
    
    public void CreateContent(){
        
        leftMostPoint = new Vector2();
        
        print("New building created");
        
        Vector2 nextPosition = new Vector2();
        nextPosition.x = nextPosition.y = 0;// Make sure vector is set to 0;
        if (previousBuilding != null){
            Building bScript = previousBuilding.GetComponent<Building>();
            prevRightMostPoint = bScript.rightMostPoint;
            nextPosition.x = bScript.rightMostPoint.x;
            
            //Check jump stats.
            float jumpVelocity = Player.GetComponent<PlayerJump>().jumpVelocity;
            float runSpeed = Player.GetComponent<PlayerMovement>().runSpeed;
            float gScale = (Player.GetComponent<Rigidbody2D>().gravityScale)*(-9.81f);
            
            float t = -jumpVelocity/gScale; //v = 0
            float s = jumpVelocity * t + 0.5f * gScale * t*t;
            
            float maxHeightOfPlatform = (s - roofMiddle.GetComponent<Renderer>().bounds.size.y/2f) * 0.8f;
            
            float minDepthOfPlatform = s * 1.5f;
            
            float y = Random.Range(-minDepthOfPlatform, maxHeightOfPlatform);
            
            //Set y height.
            nextPosition.y = bScript.rightMostPoint.y;
            nextPosition.y += y;
            
            //Calculate t, then x distance here.
//            0.5 * gScale * t*t + u*t  - s = 0;
            float a = 0.5f * gScale;
            float b = jumpVelocity;
            float c = -y;
            
            float t_y = PolynomialSolver.solve2(a, b, c);
//            s = ut+ 0.5 a t**2;
            float d_x = runSpeed * t_y;
            nextPosition.x += d_x - roofMiddle.GetComponent<Renderer>().bounds.size.x;
            
            
            
            //Equations of motion.
            //s = -(jumpVelocity * jumpVelocity)/ (2f * gScale);
            //v - u = at
            //s = ut+ 0.5 a t**2;
            //v**2 -u**2 = 2*a*s;
            //s = 0.5(u + v) t
            //Calculate y component.
            
        }
        
        //
        leftMostPoint = nextPosition;
        
        //Randomly decide tileCount.
        tileCount = Random.Range(minimumTilesCount, maximumTilesCount + 1);
        
        //Create tiles.
        nextPosition.x += roofMiddle.GetComponent<Renderer>().bounds.size.x/2f;
        nextPosition.y -= roofMiddle.GetComponent<Renderer>().bounds.size.y/2f;
        float gap = 0.0f;
        
        GameObject newTile;
        
        //Create left tile.
        {
            newTile = Instantiate(roofCornerLeft, nextPosition, Quaternion.identity);
            newTile.layer = Mathf.RoundToInt(Mathf.Log(groundLayer.value, 2));
            nextPosition.x  += (roofCornerLeft.GetComponent<Renderer>().bounds.size.x + gap);
            newTile.transform.parent = gameObject.transform;
            
            AllTiles.Add(newTile);
        }
        
        //Create middle tiles.
        for (int i = 0; i < tileCount - 2; i++) {
            newTile = Instantiate(roofMiddle, nextPosition, Quaternion.identity);
            newTile.layer = Mathf.RoundToInt(Mathf.Log(groundLayer.value, 2));
            
            //Prepare next position.
            nextPosition.x  += (roofMiddle.GetComponent<Renderer>().bounds.size.x + gap);
            
            //Set parent to Generator.
            newTile.transform.parent = gameObject.transform;
            
            //Add to list.
            AllTiles.Add(newTile);
        }
        
        //Create right tile.
        {
            newTile = Instantiate(roofCornerLeft, nextPosition, Quaternion.identity);
            newTile.layer = Mathf.RoundToInt(Mathf.Log(groundLayer.value, 2));
            newTile.transform.parent = gameObject.transform;
            nextPosition.x  += (roofCornerLeft.GetComponent<Renderer>().bounds.size.x + gap);
            
            //Flip tile here.
            newTile.GetComponent<SpriteRenderer>().flipX = true;
            
            //Add to list.
            AllTiles.Add(newTile);
            
        }
        rightMostPoint.x = newTile.GetComponent<Renderer>().bounds.size.x/2f + newTile.transform.position.x;
        rightMostPoint.y = newTile.GetComponent<Renderer>().bounds.size.y/2f + newTile.transform.position.y;
        gameObject.transform.parent = Parent.transform;
//        print(gameObject.GetComponent<Renderer>().bounds.size.x);
        
        mainGenerator.probeReach(rightMostPoint);
    }

}
