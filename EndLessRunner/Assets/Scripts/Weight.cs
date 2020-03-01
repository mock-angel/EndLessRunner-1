using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight
{
    private List<int> weightList;
    
    //Run time usage.
    private int tempweight = 0;
    private int totalWeight = 0;
    private int selectedWeight = 0;
    
    public Weight(){
        weightList = new List<int>();
    }
    
    public void Add(int weight){
        weightList.Add(weight);
        totalWeight += weight;
    }
    
    public int Pick(){
        int count = weightList.Count;
        
        //Randomly Select weight.
        selectedWeight = Random.Range(1, totalWeight + 1);
        
        //This is where we select the index based on their weight;
        totalWeight = 0;
        for (int i = 0; i < count; i++){
            totalWeight += weightList[i];
            
            if (totalWeight >= selectedWeight) return i;
            
            //If none selected.
            if (i == count -1){
                Debug.LogError("No index seleted were selected.");
            }
        }
        
        return 0;
        
    }
    
    public void Clear(){
        weightList.Clear();
        tempweight = totalWeight = selectedWeight = 0;
    }
}
