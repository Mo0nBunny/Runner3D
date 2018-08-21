using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour {


    public GameObject platformPrefab;
    public GameObject firstPlatform;
    public GameObject coinPrefab;
    public GameObject obstaclePrefab;

    public int randomNum;
    float lenghtOfPlatform = 22.8f;
    float minHeight = 0.4f;
    float maxHeight = 1.5f;

    Vector3 lastPosition;
    Vector3 firstPosition;


    // Use this for initialization
    void Start () {
        firstPosition = firstPlatform.transform.position;
    }

   public void GetSpawn()
    {
        GameObject _object = Instantiate(platformPrefab) as GameObject;
        _object.transform.position = firstPosition + new Vector3(0f, 0f, lenghtOfPlatform);
        firstPosition = _object.transform.position;
        lastPosition = firstPosition + new Vector3(0f, 0f, lenghtOfPlatform);
       
        if (_object != null)
        {
            randomNum = Random.Range(0, 8);

            for (int i = 0; i < randomNum; i++)
            {
                GameObject coinObject = Instantiate(coinPrefab) as GameObject;
                coinObject.transform.position = new Vector3(Random.Range(-1f, 1f), Random.Range(minHeight, maxHeight), Random.Range(firstPosition.z, lastPosition.z));
            }

            for (int i = 0; i < 3; i++)
            {
                GameObject obstacleObject = Instantiate(obstaclePrefab) as GameObject;
                obstacleObject.transform.position = new Vector3(Random.Range(-1.2f, 1.2f), 0f, Random.Range(firstPosition.z, lastPosition.z));
            }
        }


    }
   
}
