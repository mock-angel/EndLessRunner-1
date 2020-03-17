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
    public GameObject leftWall;
    public GameObject fullWall;
    
    [Range(0, 50)]
    public int frequencyWeight = 5;
    
    public List<GameObject> windowPrefabs;
    
    [HideInInspector]
    public GameObject Player;
    [HideInInspector]
    public GameObject Parent;
    [HideInInspector]
    public GameObject previousBuilding;
    
    [HideInInspector]
    public Vector2 rightMostPoint;
    [HideInInspector]
    public Vector2 leftMostPoint;
    [HideInInspector]
    public Vector2 prevRightMostPoint;
    
    public LayerMask groundLayer;
    
    public int ID = 0;
    
    [HideInInspector]
    public Vector2 tileSize;
//    [HideInInspector]
//    public int coinsToPlace = 0;
//    [HideInInspector]
//    public int coinsPlaced = 0;
    
    //[HideInInspector]
    //public List<GameObject> AllTilesList;
    
    //Private use.
    private int tileCount;
    private List<GameObject> AllTiles  = new List<GameObject>();
    private List<GameObject> RoofTiles  = new List<GameObject>();
    
//    void Start(){
//        AllTiles = new List<GameObject>();
//    }
    
    public GeneratorScript mainGenerator;
    
    public void CreateContent(){
        
        leftMostPoint = new Vector2();
        tileSize = new Vector2();
        
//        print("New building created");
        
        //Set tile sizes.
        tileSize.x = roofMiddle.GetComponent<Renderer>().bounds.size.x;
        tileSize.y = roofMiddle.GetComponent<Renderer>().bounds.size.y;
        
        Vector2 nextPosition = new Vector2();
        nextPosition.x = nextPosition.y = 0;// Make sure vector is set to 0;
        if (previousBuilding != null){
            Building bScript = previousBuilding.GetComponent<Building>();
            prevRightMostPoint = bScript.rightMostPoint;
            nextPosition = bScript.rightMostPoint;
            
            //Check jump stats.
            float jumpVelocity = Player.GetComponent<PlayerJump>().jumpVelocity;
            float runSpeed = Player.GetComponent<PlayerMovement>().runSpeed;
            float gScale = (Player.GetComponent<Rigidbody2D>().gravityScale)*(-9.81f);
            
            float t = -jumpVelocity/gScale; //v = 0
            float s = jumpVelocity * t + 0.5f * gScale * t*t;
            
            float maxHeightOfPlatform = (s - tileSize.y/2f) * 0.8f;
            
            float minDepthOfPlatform = s * 1.5f;
            
            float y = Random.Range(-minDepthOfPlatform, maxHeightOfPlatform);
            
            //Set y height.
//            nextPosition.y = bScript.rightMostPoint.y;
            nextPosition.y += y;
            
            //Calculate t, then x distance here.
//            0.5 * gScale * t*t + u*t  - s = 0;
            float a = 0.5f * gScale;
            float b = jumpVelocity;
            float c = -y;
            
            float t_y = PolynomialSolver.solve2(a, b, c);
//            s = ut+ 0.5 a t**2;
            float d_x = runSpeed * t_y;
            nextPosition.x += d_x - tileSize.x;
            
            //
            nextPosition.x -= tileSize.x; ////////////////////
            leftMostPoint = nextPosition;
            
            //Equations of motion.
            //s = -(jumpVelocity * jumpVelocity)/ (2f * gScale);
            //v - u = at
            //s = ut+ 0.5 a t**2;
            //v**2 -u**2 = 2*a*s;
            //s = 0.5(u + v) t
            //Calculate y component.
            
        }
        
        //Randomly decide tileCount.
        tileCount = Random.Range(minimumTilesCount, maximumTilesCount + 1);
        
        //Create tiles.
        nextPosition.x += tileSize.x/2f;//compatible even during start without previousBuilding.
        nextPosition.y -= tileSize.y/2f;
        
        
        float gap = 0.0f;
        
        GameObject newTile;
        
        //Create left tile.
        {
            newTile = Instantiate(roofCornerLeft, nextPosition, Quaternion.identity);
            newTile.layer = Mathf.RoundToInt(Mathf.Log(groundLayer.value, 2));
            nextPosition.x  += (roofCornerLeft.GetComponent<Renderer>().bounds.size.x + gap);
            newTile.transform.parent = gameObject.transform;
            
            AllTiles.Add(newTile);
            RoofTiles.Add(newTile);
        }
        
        //Create middle tiles.
        for (int i = 0; i < tileCount - 2; i++) {
            newTile = Instantiate(roofMiddle, nextPosition, Quaternion.identity);
            newTile.layer = Mathf.RoundToInt(Mathf.Log(groundLayer.value, 2));
            
            //Prepare next position.
            nextPosition.x  += (tileSize.x + gap);
            
            //Set parent to Generator.
            newTile.transform.parent = gameObject.transform;
            
            //Add to list.
            AllTiles.Add(newTile);
            RoofTiles.Add(newTile);
        }
        
        //Create right tile.
        {
            newTile = Instantiate(roofCornerLeft, nextPosition, Quaternion.identity);
            newTile.layer = Mathf.RoundToInt(Mathf.Log(groundLayer.value, 2));
            newTile.transform.parent = gameObject.transform;
            nextPosition.x  += (roofCornerLeft.GetComponent<Renderer>().bounds.size.x + gap);
            
            //Flip tile now.
            newTile.GetComponent<SpriteRenderer>().flipX = true;
            
            //Add to list.
            AllTiles.Add(newTile);
            RoofTiles.Add(newTile);
        }
        rightMostPoint.x = tileSize.x/2f + newTile.transform.position.x;
        rightMostPoint.y = tileSize.y/2f + newTile.transform.position.y;
        gameObject.transform.parent = Parent.transform;
//        print(gameObject.GetComponent<Renderer>().bounds.size.x);
        
        //Fill Tiles Below roof.
        FillBelowTiles(20);
        
        mainGenerator.probeReach(rightMostPoint);
    }
    
    void FillBelowTiles(int stepsDeep){
        Vector2 nextPosition = new Vector2();
//        nextPosition.x = nextPosition.y = 0;
        nextPosition = leftMostPoint;
        nextPosition.x += tileSize.x/2f;
        nextPosition.y -= tileSize.y*1.5f;
        
        float gap = 0.0f;
        
        int numberOfColumns = tileCount;
        
        GameObject newTile;
        
        for (int i = 0; i < stepsDeep; i++)
        {
            //Create Left Wall Tile.
            {
                newTile = Instantiate(leftWall, nextPosition, Quaternion.identity);
                newTile.layer = Mathf.RoundToInt(Mathf.Log(groundLayer.value, 2));
                //Prepare next position.
                nextPosition.x  += (tileSize.x + gap);
                
                //Set parent to Generator.
                newTile.transform.parent = gameObject.transform;
                
                //Add to list.
                AllTiles.Add(newTile);
            }
            
            for (int j = 0; j < (numberOfColumns - 2); j++) {
                newTile = Instantiate(fullWall, nextPosition, Quaternion.identity);
                newTile.layer = Mathf.RoundToInt(Mathf.Log(groundLayer.value, 2));
                //Prepare next position.
                nextPosition.x  += (tileSize.x + gap);
                
                //Set parent to Generator.
                newTile.transform.parent = gameObject.transform;
                
                //Add to list.
                AllTiles.Add(newTile);
            }
            
            {
                newTile = Instantiate(leftWall, nextPosition, Quaternion.identity);
                newTile.layer = Mathf.RoundToInt(Mathf.Log(groundLayer.value, 2));
                //Prepare next position.
                nextPosition.x  += (tileSize.x + gap);
                
                //Set parent to Generator.
                newTile.transform.parent = gameObject.transform;
                
                //Flip tile now.
                newTile.GetComponent<SpriteRenderer>().flipX = true;
                
                //Add to list.
                AllTiles.Add(newTile);
            }
            
            nextPosition.x = leftMostPoint.x + tileSize.x/2f;
            nextPosition.y -= tileSize.y;
        }
    }

}












