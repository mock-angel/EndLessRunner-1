using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourPooledObject: MonoBehaviour, IPooledObject
{
    Queue<GameObject> queueBelongsTo;
    
    public void SetQueue(Queue<GameObject> queue){
        queueBelongsTo = queue;
    }
    
    public void WithdrawToPool(){
        gameObject.SetActive(false);
        queueBelongsTo.Enqueue(gameObject);
    }
    
    public void OnObjectSpawn(){
        
    }
}
