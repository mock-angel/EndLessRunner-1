using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]    
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    
//    public ObjectPooler Instance;
    
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    
    void Start(){
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        
        for(int i = 0; i < pools.Count; i++)
        {
            Pool pool = pools[i];
            Queue<GameObject> objectPool = new Queue<GameObject>();
            
            for(int j = 0;j<pool.size; j++)
            {
                GameObject obj = Instantiate(pool.prefab, gameObject.transform);
                obj.SetActive(false);
                
                MonoBehaviourPooledObject PooledObjectScript = obj.GetComponent<MonoBehaviourPooledObject>(); 
                if(PooledObjectScript != null) PooledObjectScript.SetQueue(poolDictionary[tag]);
                
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.tag, objectPool);
        }
        
    }
    
    void CreateObject(Pool pool){
        GameObject obj = Instantiate(pool.prefab, gameObject.transform);
        obj.SetActive(false);
        
        MonoBehaviourPooledObject PooledObjectScript = obj.GetComponent<MonoBehaviourPooledObject>(); 
        if(PooledObjectScript != null) PooledObjectScript.SetQueue(poolDictionary[tag]);
        
        PooledObjectScript.WithdrawToPool();//Do PooledObjectScript.WithdrawToPool instead??
    }
    
//    void Awake(){
//        Instance = this;
//    }
    
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(poolDictionary.ContainsKey(tag)){
        
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
        }
        
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        
        IPooledObject ObjectInterface = objectToSpawn.GetComponent<IPooledObject>(); //Get rid of GetComponent call.
        if(ObjectInterface != null) ObjectInterface.OnObjectSpawn();
        
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        
        return objectToSpawn;
    }
    
    public void WithdrawToPool(string tag, GameObject obj){
        poolDictionary[tag].Enqueue(obj);
    }
}
