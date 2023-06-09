using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;
    [SerializeField] private List<GameObject> objectsList;

    // Start is called before the first frame update
    void Start()
    {
        AddObjectToPool(poolSize);
        // SetListOff();
    }

    private void AddObjectToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject poolObject = Instantiate(prefab);
            poolObject.SetActive(false);
            objectsList.Add(poolObject);
            poolObject.transform.parent = transform;
        }
    }

    public void RequestObjects()
    {
        for (int i = 0; i < objectsList.Count; i++)
        {
            if (!objectsList[i].activeSelf)
            {
                objectsList[i].SetActive(true);
                return;
            }
        }
        AddObjectToPool(1);
        objectsList[objectsList.Count - 1].SetActive(true);
    }
    public void SetListOff()
    {
        if (objectsList != null)
        {
            for (int i = 0; i < objectsList.Count; i++)
            {
                objectsList[i].SetActive(false);
            }
        }
        else { return; }
    }

}

