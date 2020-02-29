using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Range(1, 10)]
    public int minimumTilesCount = 3;
    [Range(1, 20)]
    public int maximumTilesCount = 8;
    
    public GameObject roofCornerLeft;
    public GameObject roofMiddle;
    
    public List<GameObject> windowPrefabs;
    
    public GameObject Parent;
    //Private use.
    int tileCount;
    
    public void CreateContent(){
        //Randomly decide tileCount.
        tileCount = Random.Range(minimumTilesCount, maximumTilesCount + 1);
        
        //Create tiles.
        float nextPosition = roofCornerLeft.GetComponent<Renderer>().bounds.size.x/2f;
        float gap = 0.1f;
        
        Vector2 nextPositionVector = new Vector2(nextPosition, 0);
        
        //Create left tile.
        {
            nextPositionVector.x = nextPosition;
            GameObject newTile = Instantiate(roofCornerLeft, nextPositionVector, Quaternion.identity);
            nextPosition  += (roofCornerLeft.GetComponent<Renderer>().bounds.size.x + gap);
            newTile.transform.parent = gameObject.transform;
        }
        
        //Create middle tiles.
        for (int i = 0; i < tileCount - 2; i++) {
            nextPositionVector.x = nextPosition;
            GameObject newTile = Instantiate(roofMiddle, nextPositionVector, Quaternion.identity);
            
            //Prepare next position.
            nextPosition  += (roofMiddle.GetComponent<Renderer>().bounds.size.x + gap);
            
            //Set parent to Generator.
            newTile.transform.parent = gameObject.transform;
        }
        
        //Create right tile.
        {
            nextPositionVector.x = nextPosition;
            GameObject newTile = Instantiate(roofCornerLeft, nextPositionVector, Quaternion.identity);
            newTile.transform.parent = gameObject.transform;
            nextPosition  += (roofCornerLeft.GetComponent<Renderer>().bounds.size.x + gap);
            
            newTile.GetComponent<SpriteRenderer>().flipX = true;
//            newTile.transform.localScale = -newTile.transform.localScale.x;
            
        }
        
        gameObject.transform.parent = Parent.transform;
        
    }
}
