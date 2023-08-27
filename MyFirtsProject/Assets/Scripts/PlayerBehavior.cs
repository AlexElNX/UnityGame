using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;

    public float jumpVelocity = 5f;

    private float vInput;
    private float hInput;

    private Rigidbody _rb;

    private CapsuleCollider _col;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    private GameBehavior _gameManager;
    public delegate void JumpingEvent();
    public event JumpingEvent playerJump;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
     }

    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;
    }

    void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
        if (Input.GetMouseButtonDown(1))
        {
            GameObject newBullet = Instantiate(bullet,
            transform.position + new Vector3(1, 0, 0),
            transform.rotation) as GameObject;

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity = transform.forward * bulletSpeed;

            playerJump();
        }
    }

    private bool IsGrounded()
    {
        float capsuleHeight = _col.height * 0.5f;
        Vector3 capsuleBottom = _col.bounds.center - Vector3.up * capsuleHeight + Vector3.up * _col.radius;

        float rayLength = capsuleHeight + 0.2f; // Добавляем небольшой отступ, чтобы убедиться, что нижний капсульный коллайдер находится над поверхностью.

        bool grounded = Physics.Raycast(capsuleBottom, -Vector3.up, rayLength, LayerMask.GetMask("Ground"));
        return grounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        // 4
        if (collision.gameObject.name == "Enemy")
        {
            // 5
            _gameManager.HP -= 1;
        }
    }
}
