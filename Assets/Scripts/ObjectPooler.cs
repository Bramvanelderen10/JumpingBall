using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObjectPooler : MonoBehaviour {

    [Serializable]
    public struct PooledObjectPrefab
    {
        public string name;
        public int poolSize;
        public GameObject prefab;
    }

    public struct PooledObject
    {
        public string name;
        public int poolSize;
        public List<PoolableObject> objectPool;

        public PooledObject(string name, int poolSize, List<PoolableObject> objectPool) : this()
        {
            this.name = name;
            this.poolSize = poolSize;
            this.objectPool = objectPool;
        }
    }

    public static ObjectPooler Instance;
    public List<PooledObjectPrefab> _pooledObjects;

    private List<PooledObject> _objectPools = new List<PooledObject>();

    void Awake()
    {
        Instance = this;
    }

	void Start () {
        if (_pooledObjects == null)
            _pooledObjects = new List<PooledObjectPrefab>();

        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            List<PoolableObject> list = new List<PoolableObject>();
            GameObject go = new GameObject();
            go.name = _pooledObjects[i].name + "Pool";

            for (int o = 0; o < _pooledObjects[i].poolSize; o++)
            {
                PoolableObject obj = Instantiate(_pooledObjects[i].prefab).GetComponent<PoolableObject>();
                obj.gameObject.SetActive(false);
                obj.gameObject.transform.parent = go.transform;
                list.Add(obj);
            }
            _objectPools.Add(new PooledObject(_pooledObjects[i].name, _pooledObjects[i].poolSize, list));            
        }
	}    

    public GameObject GetPooledObject(string type)
    {
        for (int i = 0; i < _objectPools.Count; i++)
        {
            if (_objectPools[i].name.Equals(type))
            {
                List<PoolableObject> pool = _objectPools[i].objectPool;
                for (int o = 0; o < pool.Count; o++)
                {
                    if (!pool[o].gameObject.activeInHierarchy)
                        return pool[o].gameObject;
                }                
            }
        }

        return null;
    }
}
