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


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            _levelObject.Rotate(Vector3.forward, 45);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            _levelObject.Rotate(Vector3.back, 45);
        }

        //if(game is not paused)
        MoveMap(speedIncrease);
    }


    private void MoveMap(float speedIncrease)
    {
        _levelObject.transform.Translate(Vector3.back * Time.deltaTime * _speed * speedIncrease);
    }
}
