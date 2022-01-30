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

    [SerializeField]
    private GameObject _plateHolderPrefab;

    [SerializeField]
    private List<Plate> _platePrefabs = new List<Plate>();

    [SerializeField]
    private List<Vector3> _platePositions = new List<Vector3>();


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
            GenerateMapObject();
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
            GenerateMapObject();
    }

    private void AddNewRandomTile()
    {
        //pick random/next tile
        //add tile to back of queu

        GameObject mapObject = Instantiate(_prefabs[Random.Range(0, _prefabs.Count)], mapHolder);
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

    private void GenerateMapObject()
    {
        GameObject cylinderHolder = Instantiate(_plateHolderPrefab, mapHolder);

        //generate 6 sides
        for(int i = 0; i < 6; i++)
        {
            Plate plate = Instantiate(_platePrefabs[Random.Range(0, _platePrefabs.Count)], cylinderHolder.transform);
            plate.transform.Rotate(Vector3.forward, i * 60);
            plate.transform.localPosition = _platePositions[i];

            int r = Random.Range(0, 100);
            if(r<25)
            {
                plate.plateType = Plate.PlateType.Empty;
                plate.GetComponentInChildren<MeshRenderer>().enabled = false;
            }
        }

        Vector3 ogScale;
        ogScale = cylinderHolder.transform.localScale;
        cylinderHolder.transform.localScale = Vector3.zero;
        StartCoroutine(AnimateTile(cylinderHolder, ogScale));
        cylinderHolder.transform.position += new Vector3(0, 0, _spawnOffset);
        _spawnOffset += spawnOffset;

        currentMapObjects.Enqueue(cylinderHolder);
    }

}
