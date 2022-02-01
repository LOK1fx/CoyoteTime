using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region events

    public event Action OnJump;
    public event Action OnJumpKeyPressed;

    #endregion

    [Header("Movement")]
    [SerializeField] private KeyCode _jumpButton;

    public bool OnGround { get; private set; }

    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private int _maxJumpCount = 1;

    private int _leftJumps;

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Vector3 _groundCheckerPositionOffset;
    [SerializeField] private Vector2 _groundCheckerSize;

    [Header("Coyote time")]
    [SerializeField] private float _maxCoyoteTime = 0.1f;

    private float _coyoteTimer;


    private Rigidbody2D _rigidbody;

    private Vector2 _input;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(!OnGround)
        {
            _coyoteTimer += Time.deltaTime;
        }
        else
        {
            _coyoteTimer = 0;
            _leftJumps = _maxJumpCount;
        }

        HandleInput();

        OnGround = Physics2D.BoxCast(transform.position + _groundCheckerPositionOffset,
            _groundCheckerSize, 0f, Vector2.down, _groundCheckerSize.y, _groundMask);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void HandleInput()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);

        if (Input.GetKeyDown(_jumpButton))
        {
            Jump();

            OnJumpKeyPressed?.Invoke();
        }
    }

    private void Movement()
    {
        _rigidbody.velocity = new Vector2(_input.x * _moveSpeed, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if(_coyoteTimer < _maxCoyoteTime && _leftJumps > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);

            _coyoteTimer = 0;
            _leftJumps--;

            OnJump?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + _groundCheckerPositionOffset, new Vector3(_groundCheckerSize.x, _groundCheckerSize.y));
    }
}