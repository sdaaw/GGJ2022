using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float jumpSpeed = 0;

    private float _distToGround;
    private CapsuleCollider _collider;

    private RotateLevel _rotateLevel;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        _rotateLevel = FindObjectOfType<RotateLevel>();

    }

    private void Update()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
      
    }


    public void SwapCharacter()
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

    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * Mathf.Clamp(jumpSpeed * _rotateLevel.speedIncrease, 1, 3500));
    }

    public void Slide()
    {

    }
}
