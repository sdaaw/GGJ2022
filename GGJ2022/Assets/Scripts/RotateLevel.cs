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

    private Character _character;

    public float RotateCooldown = 1;
    private float _rotateCooldown = 1;

    private Vector3 _originalGravityScale;

    private void Awake()
    {
        _character = FindObjectOfType<Character>();
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
        if (Input.GetKeyDown(KeyCode.D))
        {
            _rotateCooldown = RotateCooldown;
            _character.transform.position = new Vector3(_character.transform.position.x, 5, _character.transform.position.z);
            _levelObject.Rotate(Vector3.forward, 60);
            
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _rotateCooldown = RotateCooldown;
            _character.transform.position = new Vector3(_character.transform.position.x, 5, _character.transform.position.z);
            _levelObject.Rotate(Vector3.back, 60);
           
        }
    }


    private void MoveMap(float speedIncrease)
    {
        _levelObject.transform.Translate(Vector3.back * Time.deltaTime * _speed * speedIncrease);
    }
}
