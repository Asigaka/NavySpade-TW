using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    public List<GameObject> FillContainer(GameObject prefab, 
        Transform container, int startActiveCount, int maxCount, bool isActive = false)
    {
        List<GameObject> newList = new List<GameObject>();

        for (int i = 0; i < maxCount; i++)
        {
            GameObject newPrefab = Instantiate(prefab, container);
            newList.Add(newPrefab);

            if (i < startActiveCount)
                newPrefab.SetActive(true);
            else
                newPrefab.SetActive(isActive);
        }

        return newList;
    }

    public List<GameObject> FillContainer(GameObject prefab, Transform container, 
        int maxCount, Vector3 position, bool isActive = false)
    {
        List<GameObject> newList = new List<GameObject>();

        for (int i = 0; i < maxCount; i++)
        {
            GameObject newPrefab = Instantiate(prefab, container);
            newPrefab.SetActive(isActive);
            newPrefab.transform.position = position;
            newList.Add(newPrefab);
        }

        return newList;
    }

    public GameObject GetNotActiveObjectFromList(List<GameObject> gameObjects)
    {
        foreach (GameObject checkedObj in gameObjects)
        {
            if (!checkedObj.activeSelf)
            {
                return checkedObj;
            }
        }

        return null;
    }
}
