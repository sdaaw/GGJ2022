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

    [SerializeField]
    private KeyCode jumpKey;

    public CharacterType characterType;

    public Plate currentPlate;

    public float characterHealth = 3;
    private float _characterHealth;

    public enum CharacterType
    {
        Character1,
        Character2
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        _rotateLevel = FindObjectOfType<RotateLevel>();

    }

    private void Update()
    {
        CheckUnder();

        if (IsGrounded() && Input.GetKeyDown(jumpKey))
        {
            Jump();
        }

        //swap characters sides
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwapCharacter();
        }
      
    }

    public void SwapCharacter()
    {
        characterType = (characterType == CharacterType.Character1) ? CharacterType.Character2 : CharacterType.Character1;
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
        _rigidbody.AddForce(Vector3.up * Mathf.Clamp(jumpSpeed * _rotateLevel.speedIncrease, 1, 1500));
    }

    public void Slide()
    {

    }

    public void CheckUnder()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _distToGround + 1f);
        if(hit.collider != null)
        {
            Plate plate = hit.collider.GetComponent<Plate>();
            if(plate != null && plate != currentPlate)
            {
                Debug.Log(plate.plateType);
                currentPlate = plate;
            }   
        }
    }
}
