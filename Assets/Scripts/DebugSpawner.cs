using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpawner : MonoBehaviour
{
    public GameObject cameraObject;
    public GameObject enemyPrefab;
    public GameObject bolasPrefab;
    public GameObject smokePrefab;
    public GameObject boostPrefab;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("i"))
        {
            int itemPicked = Random.Range(0, 3);
            switch (itemPicked)
            {
                case 0:
                    {
                        Instantiate(bolasPrefab, new Vector3(Random.Range(-6.5f, 6.5f), (Random.Range(-3.5f, 3.5f) + cameraObject.transform.position.y), 0), new Quaternion());
                        break;
                    }
                case 1:
                    {
                        Instantiate(smokePrefab, new Vector3(Random.Range(-6.5f, 6.5f), (Random.Range(-3.5f, 3.5f) + cameraObject.transform.position.y), 0), new Quaternion());
                        break;
                    }
                case 2:
                    {
                        Instantiate(boostPrefab, new Vector3(Random.Range(-6.5f, 6.5f), (Random.Range(-3.5f, 3.5f) + cameraObject.transform.position.y), 0), new Quaternion());
                        break;
                    }
            }
        }
        if(Input.GetKeyDown("o"))
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-6.5f, 6.5f), (Random.Range(-3.5f, 3.5f) + cameraObject.transform.position.y), 0), new Quaternion());
        }
    }
}
