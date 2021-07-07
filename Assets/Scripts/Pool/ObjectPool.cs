using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    List<PoolItem> prefabsToPool = new List<PoolItem>();
    Dictionary<GameObject, PoolItem> poolPrefabsDictionary = new Dictionary<GameObject, PoolItem>();
    Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();


    private void Awake()
    {
        foreach (var item in prefabsToPool)
        {
            poolPrefabsDictionary.Add(item.prefabGO, item);

            var queue = new Queue<GameObject>();
            poolDictionary.Add(item.prefabGO, queue);
            GrowQueue(item.prefabGO);
        }
    }


    void GrowQueue(GameObject poolKey)
    {
        poolPrefabsDictionary.TryGetValue(poolKey, out PoolItem poolItem);

        var keyqueue = poolDictionary[poolKey];
        for (int i = 0; i < poolItem.clonesNeeded; i++)
        {
            var go = GameObject.Instantiate(poolItem.prefabGO, transform);
            go.SetActive(false);
            keyqueue.Enqueue(go);
        }
    }

    public GameObject GetObjectFromPool(GameObject prefabGO, Transform newParent)
    {
        if (poolDictionary[prefabGO].Count <= 0)
        {
            print("grow");
            GrowQueue(prefabGO);

        }

        var pooledObj = poolDictionary[prefabGO].Dequeue();
        pooledObj.transform.SetParent(newParent);
        pooledObj.SetActive(true);

        return pooledObj;
    }


    public void RecycleObject(GameObject prefabGO, GameObject pooledObj)
    {
        pooledObj.SetActive(false);
        pooledObj.transform.SetParent(transform);
        poolDictionary[prefabGO].Enqueue(pooledObj);
    }

}

[System.Serializable]
public class PoolItem
{
    public GameObject prefabGO;
    public int clonesNeeded;
}
