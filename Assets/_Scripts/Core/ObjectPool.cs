using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Pool
{
    public static class ObjectPool
    {
        private static Dictionary<string, List<GameObject>> _objectDictonary = new();

        public static GameObject GetPool(GameObject obj, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (obj == null) return null;
            string name = obj.name;
            GameObject objectPool = null;

            if (_objectDictonary.ContainsKey(name) == false || _objectDictonary[name].Count == 0)
            {
                _objectDictonary[name] = new List<GameObject>();
                objectPool = GameObject.Instantiate(obj, position, rotation, parent);
                objectPool.name = obj.name;

                return objectPool;
            }

            objectPool = _objectDictonary[name].First();
            objectPool.SetActive(true);
            objectPool.transform.SetPositionAndRotation(position, rotation);
            objectPool.transform.SetParent(parent);
            _objectDictonary[name].Remove(objectPool);

            return objectPool;
        }

        public static void SetPool(GameObject obj)
        {
            if (obj.activeInHierarchy == false)
            {
                obj.SetActive(false);
                return;
            }

            if (_objectDictonary.ContainsKey(obj.name) == false)
            {
                Debug.LogWarning($"Об`єкт {obj.name} створюється через Instantiate, а видаляється через ObjectPool");
                GameObject.Destroy(obj);
                // obj.SetActive(false);
                // _objectDictonary[obj.name] = new()
                // {
                //     obj
                // };


                return;
            }

            obj.SetActive(false);
            _objectDictonary[obj.name].Add(obj);
        }

        public static void ClearPool()
        {
            _objectDictonary.Clear();
        }
    }
}

namespace Core.PoolT
{
    // using System;
    // using System.Collections.Generic;
    // using System.Linq;
    // using UnityEngine;

    // public static class ObjectPool<T> where T : MonoBehaviour
    // {
    //     private static Dictionary<string, List<T>> _objectDictonary = new();

    //     public static T GetPool(T prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    //     {
    //         string name = prefab.name;
    //         T objectPool = null;

    //         if (_objectDictonary.ContainsKey(name) == false || _objectDictonary[name].Count == 0)
    //         {
    //             _objectDictonary[name] = new List<T>();
    //             objectPool = GameObject.Instantiate(prefab, position, rotation, parent);
    //             objectPool.name = prefab.name;

    //             return objectPool;
    //         }

    //         Debug.Log(_objectDictonary[name].Count);
    //         objectPool = _objectDictonary[name].First();
    //         objectPool.gameObject.SetActive(true);
    //         objectPool.transform.SetPositionAndRotation(position, rotation);
    //         objectPool.transform.SetParent(parent);
    //         _objectDictonary[name].Remove(objectPool);

    //         return objectPool;
    //     }

    //     public static void SetPool(T obj)
    //     {
    //         if (!obj.gameObject.activeInHierarchy)
    //         {
    //             return;
    //         }

    //         if (_objectDictonary.ContainsKey(obj.name) == false)
    //         {
    //             // obj.gameObject.SetActive(false);
    //             // _objectDictonary[obj.name] = new() { obj };


    //             return;
    //         }

    //         obj.gameObject.SetActive(false);
    //         _objectDictonary[obj.name].Add(obj);
    //     }

    //     public static void ClearTPool()
    //     {
    //         foreach (var item in _objectDictonary.Values)
    //         {
    //             item.Clear();
    //         }
    //         _objectDictonary.Clear();

    //     }
    // }

}