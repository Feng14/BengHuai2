using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// 内存池
public class Pool {
    public enum PoolType { Bullet, Monster };
    public static Pool Instance = new Pool();

    private GameObject poolGO;
    private Hashtable hashTable;

    private Pool()
    {
        poolGO = new GameObject("Pool");
        hashTable = new Hashtable();
    }

    // 创建一种对象的池
    public void createObjPool(GameObject prefab, PoolType type, int size = 10)
    {
        PoolObj newPoolObj = new PoolObj();
        newPoolObj.prefab = prefab;

        GameObject container = new GameObject(prefab.name + "Container");
        container.transform.parent = poolGO.transform;
        newPoolObj.contaienr = container;

        for (int i = 0; i < size; i++)
            addToPool(newPoolObj);

        newPoolObj.index = 0;
        hashTable.Add(type, newPoolObj);
    }

    // 从池中获取一个能用的对象
    public GameObject getObjFromPool(PoolType type)
    {
        if (!hashTable.ContainsKey(type))
            throw new Exception("Have not create " + type.ToString() + " Pool");

        PoolObj poolObj = hashTable[type] as PoolObj;
        GameObject t;
        for (int i = 0; i < poolObj.objList.Count; i++)
        {
            t = poolObj.objList[(poolObj.index + i) % poolObj.objList.Count];
            // Active 为False 即为可以拿出来使用的对象，否则寻找下一个
            if (!t.activeSelf)
            {
                poolObj.index = (poolObj.index + i) % poolObj.objList.Count;
                t.SetActive(true);
                return t;
            }
        }
        // 所有的都正在使用……只能添加新的了
        t = addToPool(poolObj);
        t.SetActive(true);
        return t;
    }

    // 往某个池中添加新对象
    private GameObject addToPool(PoolObj poolObj)
    {
        GameObject t = GameObject.Instantiate(poolObj.prefab) as GameObject;
        t.SetActive(false);
        poolObj.objList.Add(t);
        t.transform.parent = poolObj.contaienr.transform;
        return t;
    }

    private class PoolObj
    {
        public List<GameObject> objList = new List<GameObject>();
        public int index;
        public GameObject prefab;
        public GameObject contaienr;
    }
}
