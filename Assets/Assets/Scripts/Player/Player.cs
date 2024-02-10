using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int Health { get; set; }

    public int gems = 0;

    private Rigidbody2D _rigid;
    [SerializeField] private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField] private float _speed = 3.0f;
    private Player_Animation _player_anim;
    private SpriteRenderer _playerSprite;
    private bool _isGrounded;
    private SpriteRenderer _swordArcSprite;
    void Start()
    {
        _player_anim = GetComponent<Player_Animation>();
        _rigid = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {

        Movement();
        Attack();
    }
    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && IsGrounded() == true)
        {
            _player_anim.Attack();
        }
    }
    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        FlipSprite(move);
        _isGrounded = IsGrounded();
        
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _player_anim.Jump(true);
        }

        _player_anim.Move(move);

        _rigid.velocity = new Vector2(move*_speed, _rigid.velocity.y);

    }

    void FlipSprite(float move)
    {
        if (move > 0)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;
            Vector3 pos = _swordArcSprite.transform.localPosition;
            pos.x = 1.01f;
            _swordArcSprite.transform.localPosition = pos;
        }
        else if (move < 0)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;
            Vector3 pos = _swordArcSprite.transform.localPosition;
            pos.x = -1.01f;

            _swordArcSprite.transform.localPosition = pos;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);

        if(hitInfo.collider != null)
        {
            if (_resetJump == false)
                _player_anim.Jump(false);
                return true;
        }
        return false;
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        Debug.Log("Player Damaged");
    }
}
