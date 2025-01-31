using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class Movement : MonoBehaviour
    {
        private Joystick joystick;
        private Rigidbody2D rb;
        private bool facingRight = true;
        private bool isGrounded;
        
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _moveInput;
        [SerializeField] private Transform footPosition;
        [SerializeField] private float checkRadius;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Animator _animator;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            isGrounded = Physics2D.OverlapCircle(footPosition.position, checkRadius, whatIsGround);

            if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
            {
                rb.linearVelocity = Vector2.up * _jumpForce;
                _animator.SetTrigger("takeOf");
            }
            if (isGrounded == true)
            {
                _animator.SetBool("IsJumping", false);
            }
            else
            {
                _animator.SetBool("IsJumping", true);
            }
        }

        private void FixedUpdate()
        {
            _moveInput = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(_moveInput * _speed, rb.linearVelocityY);

            _animator.SetFloat("Speed", Mathf.Abs(_moveInput));

            if (facingRight == false && _moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && _moveInput < 0)
            {
                Flip();
            }
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector2 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }
    }
}