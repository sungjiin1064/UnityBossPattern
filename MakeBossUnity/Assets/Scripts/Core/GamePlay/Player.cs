using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Controls controls;
    Rigidbody2D rd;

    [Header("มกวม")]
    [SerializeField]LayerMask groundMask;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float groundDistance = 1f;

    public Action<bool> OnFire;

    public Action<bool> newOnFire;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls = new Controls();
        controls.Player.Enable();

        controls.Player.Jump.performed += HandleJump;
        controls.Player.Fire.performed += OnFirePerformed;
        controls.Player.Fire.performed += OnFireCancled;
    }

    private void OnDisable()
    {
        controls.Player.Jump.performed -= HandleJump;
        controls.Player.Fire.performed -= OnFirePerformed;
        controls.Player.Fire.performed -= OnFireCancled;
        controls.Player.Disable();
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        newOnFire?.Invoke(true);   
    }

    private void OnFireCancled(InputAction.CallbackContext context)
    {
        newOnFire?.Invoke(false);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float dir = controls.Player.Move.ReadValue<float>();
        rd.linearVelocity = new Vector2(dir * moveSpeed, rd.linearVelocityY);

    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if(ISGround())
        {
            rd.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        } 
    }

    private bool ISGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(Vector2.down * groundDistance));
    }
}
