using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [System.Serializable]
    public struct ColliderSettings
    {
        public Vector2 offset;
        public Vector2 size;
    }

    public class Movement : MonoBehaviour
    {
        private Rigidbody2D rb;
        private bool facingRight = true;
        private bool isGrounded;
        private bool isCrouching;
        private bool canStand;
        private BoxCollider2D boxCollider;

        [SerializeField] private Joystick _joystick;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _moveInput;
        [SerializeField] private Transform _footPosition;
        [SerializeField] private float _checkRadius = 0.3f;
        [SerializeField] private float _standUpCheckHeight = 2.0f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Animator _animator;

        public ColliderSettings normalSettings;
        public ColliderSettings crouchSettings;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            CheckGround();

            _moveInput = _joystick.Horizontal;

            canStand = CanStandUp();
            _animator.SetBool("canStand", canStand);

            _animator.SetFloat("Speed", Mathf.Abs(_moveInput));

            if (facingRight == false && _moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && _moveInput < 0)
            {
                Flip();
            }

            if (isCrouching)
            {
                ApplyColliderSettings(crouchSettings);
            }
            else
            {
                ApplyColliderSettings(normalSettings);
            }
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = new Vector2(_moveInput * _speed, rb.linearVelocityY);
        }

        private bool CanStandUp()
        {
            Vector2 checkPosition = transform.position + Vector3.up * _standUpCheckHeight;
            float checkRadius = 0.3f;
            return !Physics2D.OverlapCircle(checkPosition, checkRadius, _groundLayer);
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector2 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        private void CheckGround()
        {
            isGrounded = Physics2D.OverlapCircle(_footPosition.position, _checkRadius, _groundLayer);

            if (isGrounded == true)
            {
                _animator.SetBool("IsJumping", false);
            }
            else
            {
                _animator.SetBool("IsJumping", true);
            }
        }
        private void ApplyColliderSettings (ColliderSettings settings)
        {
            boxCollider.offset = settings.offset;
            boxCollider.size = settings.size;
        }
        public void PressCrouchButton()
        {
            if (isCrouching)
            {
                if (CanStandUp())
                {
                    isCrouching = false;
                    ApplyColliderSettings(normalSettings);
                    _animator.SetTrigger("StandUp");
                }
                else return;
            }
            else
            {
                isCrouching = true;
                ApplyColliderSettings(crouchSettings);
                _animator.SetTrigger("Crouch");
            }

            _animator.SetBool("isCrouching", isCrouching);
        }


        public void PressJumpButton()
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, _jumpForce);
                _animator.SetTrigger("takeOf");
                _animator.SetBool("isRunning", Mathf.Abs(_moveInput) > 0);
                _animator.SetBool("isCrouching", isCrouching);
            }
        }
    }
}