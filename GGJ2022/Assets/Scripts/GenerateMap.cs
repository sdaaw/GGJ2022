using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _prefabs = new List<GameObject>();

    public Queue<GameObject> currentMapObjects = new Queue<GameObject>();
    public Transform mapHolder;

    public float NewTileTimer = 3;
    private float _newTileTimer = 3;

    public float RemoveTileTimer = 6;
    private float _removeTileTimer = 6;

    public float startAmount = 6;

    public float spawnOffset = 15;
    private float _spawnOffset;


    private void Start()
    {
        _newTileTimer = 0;
        _spawnOffset = spawnOffset;

        InitMap();
    }

    private void Update()
    {
        if(_newTileTimer <= 0)
        {
            _newTileTimer = NewTileTimer;
            AddNewRandomTile();
        }
        else
        {
            _newTileTimer -= Time.deltaTime * RotateLevel.speedIncrease;
        }

        if(_removeTileTimer <= 0)
        {
            _removeTileTimer = RemoveTileTimer;
            RemoveOldTile();
        }
        else
        {
            _removeTileTimer -= Time.deltaTime * RotateLevel.speedIncrease;
        }
    }

    private void InitMap()
    {
        for (int i = 0; i < startAmount; i++)
            AddNewRandomTile();
    }

    private void AddNewRandomTile()
    {
        //pick random/next tile
        //add tile to back of queu

        GameObject mapObject = Instantiate(_prefabs[Random.Range(0, _prefabs.Count - 1)], mapHolder);
        Vector3 ogScale;
        ogScale = mapObject.transform.localScale;
        mapObject.transform.localScale = Vector3.zero;
        StartCoroutine(AnimateTile(mapObject, ogScale));
        mapObject.transform.position += new Vector3(0,0, _spawnOffset);
        _spawnOffset += spawnOffset;
        currentMapObjects.Enqueue(mapObject);
    }


    IEnumerator AnimateTile(GameObject g, Vector3 ogScale)
    {
        for(float i = 0; i < 1f; i += 0.01f)
        {
            g.transform.localScale = new Vector3(ogScale.x * i, ogScale.y * i, ogScale.z * i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void RemoveOldTile()
    {
        GameObject oldMapObject = currentMapObjects.Dequeue();
        Destroy(oldMapObject);
    }

}
