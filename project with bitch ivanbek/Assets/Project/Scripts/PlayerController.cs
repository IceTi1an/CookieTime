using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;
    private Rigidbody2D rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
        {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(hor, ver);
        rb.linearVelocity = movement * _speed * Time.deltaTime;
        }

        private void FixedUpdate()
        {
        }
    }