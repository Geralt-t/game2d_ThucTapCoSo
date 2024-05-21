using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImagePool : MonoBehaviour
{
    [SerializeField]
    private GameObject afterImagePrefab;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    public static AfterImagePool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject instanceToAdd = Instantiate(afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (availableObjects.Count == 0)
        {
            GrowPool();
        }

        GameObject instance = availableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
