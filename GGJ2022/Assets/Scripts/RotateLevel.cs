using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLevel : MonoBehaviour
{
    [SerializeField]
    private Transform _levelObject = null;
    [SerializeField]
    private float _speed = 0;

    public float speedIncreaseInspector = 1;
    public static float speedIncrease = 1;

    [SerializeField]
    private Character _characterOfLevel = null;

    public float RotateCooldown = 1;
    private float _rotateCooldown = 1;

    private Vector3 _originalGravityScale;

    [SerializeField]
    private KeyCode Left;
    [SerializeField]
    private KeyCode Right;

    private GameManager _gm;

    private void Awake()
    {
        //_characterOfLevel = FindObjectOfType<Character>();
        _rotateCooldown = 0;
        //Physics.gravity *= 3;
        //_originalGravityScale = Physics.gravity;

        _gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        //if(game is not paused)
        //speedIncrease = speedIncreaseInspector;

        if (_gm.IsGameOver())
            return;

        if (_rotateCooldown <= 0)
        {
            CheckRotate();
        }
        else
        {
            _rotateCooldown -= Time.deltaTime;
        }

        MoveMap(speedIncrease);
        //Physics.gravity = _originalGravityScale * speedIncrease;
    }


    IEnumerator SmoothRotate(Vector3 dir, float degrees)
    {
        if(_rotateCooldown <= 0)
        {
            _rotateCooldown = RotateCooldown;
            Vector3 rot;
            for (float i = 0; i <= degrees; i += 1f)
            {
                rot = i * dir / 30.5f; //magic
                _levelObject.Rotate(rot);
                yield return new WaitForSeconds(0.00001f);
            }
        }
    }

    private void CheckRotate()
    {
        if (Input.GetKeyDown(Right))
        {
            //.transform.position = new Vector3(_characterOfLevel.transform.position.x, 5, _characterOfLevel.transform.position.z);
            //_levelObject.Rotate(Vector3.forward, 60);
            StartCoroutine(SmoothRotate(Vector3.forward, 60));
            //_characterOfLevel.CheckUnder();


        }
        else if (Input.GetKeyDown(Left))
        {
            //_characterOfLevel.transform.position = new Vector3(_characterOfLevel.transform.position.x, 5, _characterOfLevel.transform.position.z);
            //_levelObject.Rotate(Vector3.back, 60);
            StartCoroutine(SmoothRotate(Vector3.back, 60));
            //_characterOfLevel.CheckUnder();

        }
    }


    private void MoveMap(float speedIncrease)
    {
        _levelObject.transform.Translate(Vector3.back * Time.deltaTime * _speed * speedIncrease);
    }
}
