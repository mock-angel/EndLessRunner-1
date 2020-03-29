using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]    
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public class PoolQueue<T> : Queue
{
    public Pool poolParent;
}

public class ObjectPooler : MonoBehaviour
{
    
    
//    public ObjectPooler Instance;
    
    public List<Pool> pools;
    public Dictionary<string, PoolQueue<GameObject>> poolDictionary;
    
    void Start(){
        poolDictionary = new Dictionary<string, PoolQueue<GameObject>>();
        
        for(int i = 0; i < pools.Count; i++)
        {
            Pool pool = pools[i];
            PoolQueue<GameObject> objectPool = new PoolQueue<GameObject>();
            
            for(int j = 0;j<pool.size; j++)
            {
                GameObject obj = Instantiate(pool.prefab, gameObject.transform);
                obj.SetActive(false);
                
                MonoBehaviourPooledObject PooledObjectScript = obj.GetComponent<MonoBehaviourPooledObject>(); 
                if(objectPool == null) print("Start WEnt wrong");
                if(PooledObjectScript != null) {
                    PooledObjectScript.SetQueue(objectPool);
                    if(PooledObjectScript.queueBelongsTo == null ) print("inside if and it went wrong");
                }
                else print("This particular didnt derive from Pooled Object");
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.tag, objectPool);
            objectPool.poolParent = pool;
        }
        
    }
    
    GameObject CreateObject(Pool pool){
//        if(pool == null) print("It was null"); 
        GameObject obj = Instantiate(pool.prefab, gameObject.transform);
        obj.SetActive(false);
        
        MonoBehaviourPooledObject PooledObjectScript = obj.GetComponent<MonoBehaviourPooledObject>(); 
        
        if(PooledObjectScript != null) PooledObjectScript.SetQueue(poolDictionary[pool.tag]);
        else {
            Destroy(obj);
//            Debug.LogWarning("Pooled Objects must contain or derive from MonoBehaviourPooledObject");
            return null;
        }
        PooledObjectScript.WithdrawToPool();
        return obj;
    }
    
    GameObject CreateObjectBlank(Pool pool){
        GameObject obj = Instantiate(pool.prefab, gameObject.transform);
        obj.SetActive(false);
        
        MonoBehaviourPooledObject PooledObjectScript = obj.GetComponent<MonoBehaviourPooledObject>(); 
        
        if(PooledObjectScript != null) PooledObjectScript.SetQueue(poolDictionary[pool.tag]);
        else {
//            Destroy(obj);
//            Debug.LogWarning("Pooled Objects must contain or derive from MonoBehaviourPooledObject");
            return null;
        }
        return obj;
    
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag)){
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        
        PoolQueue<GameObject> poolQueue = poolDictionary[tag];
        GameObject objectToSpawn;
        if(poolQueue.Count>0)
            objectToSpawn = (GameObject) poolQueue.Dequeue();
        else //return null;
            objectToSpawn = CreateObjectBlank(poolQueue.poolParent);
         
        IPooledObject ObjectInterface = objectToSpawn.GetComponent<IPooledObject>(); //Get rid of GetComponent call.
        if(ObjectInterface != null) ObjectInterface.OnObjectSpawn();
        
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        
        return objectToSpawn;
    }
    
//    public void WithdrawToPool(string tag, GameObject obj){
//        poolDictionary[tag].Enqueue(obj);
//    }
}
