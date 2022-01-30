using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float health = 3;

    public Text healthText;

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

    private bool _isJumping;
    private Vector3 _startPos;
    private Vector3 _highPos;

    public float jumpTimer = 1.5f;
    private float _jumpTimer;

    [SerializeField]
    private float _jumpHeight = 5;

    private bool isDead;

    public enum CharacterType
    {
        Character1,
        Character2
    }

    [SerializeField]
    private GameObject _character1;
    [SerializeField]
    private GameObject _character2;

    private Animator _c1Anim;
    private Animator _c2Anim;

    private Animator _c1ImageAnim;
    private Animator _c2ImageAnim;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        _rotateLevel = FindObjectOfType<RotateLevel>();

        _startPos = transform.position;
        _highPos = _startPos + new Vector3(0, _jumpHeight, 0);

        _c1Anim = _character1.GetComponentInChildren<Animator>();
        _c2Anim = _character2.GetComponentInChildren<Animator>();

        _c1ImageAnim = transform.GetChild(0).GetComponentInChildren<Animator>();
        _c2ImageAnim = transform.GetChild(1).GetComponentInChildren<Animator>();

        if (characterType == CharacterType.Character1)
        {
            _c1ImageAnim.gameObject.SetActive(true);
            _c2ImageAnim.gameObject.SetActive(false);
        }
        else
        {
            _c1ImageAnim.gameObject.SetActive(false);
            _c2ImageAnim.gameObject.SetActive(true);
        }

    }

    public void PlayDamageTakeAnimation()
    {
        if (characterType == CharacterType.Character1)
            _c1ImageAnim.SetTrigger("TakeDmg");
        else
            _c2ImageAnim.SetTrigger("TakeDmg");
    }

    private void Update()
    {
        if(isDead)
        {
            transform.Rotate(Random.insideUnitCircle.normalized, 1f);
        }
        CheckUnder();

        if (IsGrounded() && Input.GetKeyDown(jumpKey))
        {
            if (_isJumping)
                return;

            Jump();
        }

        //swap characters sides
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwapCharacter();
        }

        if(_isJumping)
        {
            _jumpTimer += Time.deltaTime;

            if(_jumpTimer < (jumpTimer / 2))
                transform.position = Vector3.Lerp(_startPos, _highPos, _jumpTimer / jumpTimer);

            if (_jumpTimer > (jumpTimer / 2))
                transform.position = Vector3.Lerp(_highPos, _startPos, _jumpTimer / jumpTimer);

            if(_jumpTimer > jumpTimer)
            {
                transform.position = _startPos;
                _isJumping = false;
                _jumpTimer = 0;
            }
        }

        if(health <= 0)
        {
            Dead();
        }
      
    }

    public void UpdateHealthText()
    {
        healthText.text = health.ToString() + "/3";
    }

    public void Dead()
    {
        isDead = true;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.mass = 10f;
        _rigidbody.constraints = RigidbodyConstraints.None;

        FindObjectOfType<GameManager>().GameOver();
    }

    public void SwapCharacter()
    {
        SoundManager.PlayASource("Teleport");
        characterType = (characterType == CharacterType.Character1) ? CharacterType.Character2 : CharacterType.Character1;
        if(characterType == CharacterType.Character1)
        {
            _character1.SetActive(true);
            _character2.SetActive(false);
            _c1ImageAnim.gameObject.SetActive(true);
            _c2ImageAnim.gameObject.SetActive(false);
        }
        else if(characterType == CharacterType.Character2)
        {
            _character1.SetActive(false);
            _character2.SetActive(true);
            _c1ImageAnim.gameObject.SetActive(false);
            _c2ImageAnim.gameObject.SetActive(true);
        }
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
        //_rigidbody.AddForce(Vector3.up * Mathf.Clamp(jumpSpeed * _rotateLevel.speedIncrease, 1, 1500));
        _isJumping = true;
        SoundManager.PlayASource("Jump");

        if (characterType == CharacterType.Character1)
            _c1Anim.SetTrigger("Jump");
        else
            _c2Anim.SetTrigger("Jump");
    }

    public void CheckUnder()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _distToGround + 1f);
        if(hit.collider != null)
        {
            Plate plate = hit.collider.GetComponent<Plate>();
            if(plate != null && plate != currentPlate)
            {
                //Debug.Log(plate.plateType);
                currentPlate = plate;

                if(plate.plateType == Plate.PlateType.Blue && characterType == CharacterType.Character1)
                {
                    GameManager gm = FindObjectOfType<GameManager>();
                    gm.AddScore(1000);
                    gm.gameDifficultyScaler += .25f;
                    if (gm.gameDifficultyScaler > 15)
                        gm.gameDifficultyScaler = 15;
                    SoundManager.PlayASource("GoodScore");
                }
                else if(plate.plateType == Plate.PlateType.Blue && characterType == CharacterType.Character2)
                {
                    GameManager gm = FindObjectOfType<GameManager>();
                    gm.gameDifficultyScaler -= .5f;
                    if (gm.gameDifficultyScaler < 1)
                        gm.gameDifficultyScaler = 1;
                    SoundManager.PlayASource("BadScore");
                }
                else if(plate.plateType == Plate.PlateType.Red && characterType == CharacterType.Character2)
                {
                    GameManager gm = FindObjectOfType<GameManager>();
                    gm.AddScore(1000);
                    gm.gameDifficultyScaler += .25f;
                    if (gm.gameDifficultyScaler > 15)
                        gm.gameDifficultyScaler = 15;
                    SoundManager.PlayASource("GoodScore");
                }
                else if (plate.plateType == Plate.PlateType.Red && characterType == CharacterType.Character1)
                {
                    GameManager gm = FindObjectOfType<GameManager>();
                    gm.gameDifficultyScaler -= .5f;
                    if (gm.gameDifficultyScaler < 1)
                        gm.gameDifficultyScaler = 1;
                    SoundManager.PlayASource("BadScore");
                }
                else if(plate.plateType == Plate.PlateType.Empty)
                {
                    Destroy(plate.gameObject);
                    Dead();
                }
            }   
        }
    }
}
