using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    private List<GameObject> PoolList = new List<GameObject>();

    [SerializeField] private GameObject[] PoolObjects;
    [SerializeField] private int PoolSize;

    void Awake()
    {
        InitPool(); 
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitPool()
    {
        int objectCount = PoolObjects.Length;
        int currentObject = 0;

        for(int i=0; i<PoolSize; i++)
        {
            GameObject clone = Instantiate(PoolObjects[currentObject],transform);
            clone.transform.localPosition = Vector3.zero;
            PoolList.Add(clone);
            clone.SetActive(false);
            currentObject++;
            if(currentObject >= objectCount) currentObject = 0;
        }
    }

    public GameObject GetFromPool()
    {
        int selection = Random.Range(0,PoolList.Count);
        GameObject selectedObject = PoolList[selection];
        PoolList.Remove(selectedObject);
        selectedObject.SetActive(true);
        return selectedObject;
    }

    public void AddToPool(GameObject obj)
    {
        obj.SetActive(false);
        PoolList.Add(obj);
    }
}
