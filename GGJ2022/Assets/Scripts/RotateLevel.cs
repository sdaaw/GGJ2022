using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLevel : MonoBehaviour
{
    [SerializeField]
    private Transform _levelObject = null;
    [SerializeField]
    private float _speed = 0;

    public float speedIncrease = 1;

    [SerializeField]
    private Character _characterOfLevel = null;

    public float RotateCooldown = 1;
    private float _rotateCooldown = 1;

    private Vector3 _originalGravityScale;

    [SerializeField]
    private KeyCode Left;
    [SerializeField]
    private KeyCode Right;

    private void Awake()
    {
        //_characterOfLevel = FindObjectOfType<Character>();
        _rotateCooldown = 0;
        Physics.gravity *= 3;
        _originalGravityScale = Physics.gravity;
    }

    private void Update()
    {
        //if(game is not paused)

        if (_rotateCooldown <= 0)
        {
            CheckRotate();
        }
        else
        {
            _rotateCooldown -= Time.deltaTime * speedIncrease;
        }

        MoveMap(speedIncrease);
        Physics.gravity = _originalGravityScale * speedIncrease;
    }

    private void CheckRotate()
    {
        if (Input.GetKeyDown(Right))
        {
            _rotateCooldown = RotateCooldown;
            //.transform.position = new Vector3(_characterOfLevel.transform.position.x, 5, _characterOfLevel.transform.position.z);
            _levelObject.Rotate(Vector3.forward, 60);
            //_characterOfLevel.CheckUnder();


        }
        else if (Input.GetKeyDown(Left))
        {
            _rotateCooldown = RotateCooldown;
            //_characterOfLevel.transform.position = new Vector3(_characterOfLevel.transform.position.x, 5, _characterOfLevel.transform.position.z);
            _levelObject.Rotate(Vector3.back, 60);
            //_characterOfLevel.CheckUnder();

        }
    }


    private void MoveMap(float speedIncrease)
    {
        _levelObject.transform.Translate(Vector3.back * Time.deltaTime * _speed * speedIncrease);
    }
}
