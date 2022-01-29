using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float jumpSpeed = 0;

    private float _distToGround;
    private CapsuleCollider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;

    }

    private void Update()
    {
        Jump();
    }


    private void SwapCharacter()
    {

    }

    private bool IsGrounded()
    {
        bool rval = Physics.Raycast(transform.position, Vector3.down, _distToGround + 1f);
        return rval;
    }

    private void OnDrawGizmos()
    {
        //Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - _distToGround - 1f, transform.position.z));
    }

    private void Jump()
    {
        if(IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _rigidbody.AddForce(Vector3.up * jumpSpeed);
            }
        }
       
    }

    private void Slide()
    {

    }
}
