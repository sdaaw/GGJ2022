using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrower : MonoBehaviour
{
    public List<GameObject> objectList = new List<GameObject>();
    public List<GameObject> spawnPositionList = new List<GameObject>();

    public float objectSpawnInterval;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1 * Time.deltaTime;
        if(timer > objectSpawnInterval)
        {
            ThrowObject(objectList[Random.Range(0, objectList.Count)]);
            timer = 0;
        }
    }


    public void ThrowObject(GameObject g)
    {
        GameObject a = Instantiate(g, spawnPositionList[Random.Range(0, spawnPositionList.Count)].transform.position, Quaternion.identity);
        FlyingObjectBehaviour fob = a.GetComponent<FlyingObjectBehaviour>();
        float randScale = Random.Range(0.5f, 1.5f);
        a.transform.localScale = new Vector3(randScale, randScale, randScale);
        fob.movementSpeed = Random.Range(50, 150);
        fob.rotationSpeed = fob.movementSpeed;
    }
}
