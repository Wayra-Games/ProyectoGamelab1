using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movimiento y velocidad de movimiento
    float _horizontalMove;
    [SerializeField] float _speedPlayer;
    private Vector3 velocity;
    //Fuerza de salto
    [SerializeField] float _jumpForce;

    private Rigidbody2D _rigidbody2D;
    //El layer sobre el que debe estar para salta
    [SerializeField] LayerMask _layer;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        PlayerDirection();
    }

    private void FixedUpdate()
    {

        Move();
    }

    void Move()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal");
        velocity = new Vector2(_horizontalMove, 0) * _speedPlayer * Time.deltaTime;
        transform.position += velocity;

    }

    #region Jump
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnLayer())
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

    }

    //Detecta si el player esta en el piso para que pueda saltar
    bool IsOnLayer()
    {
        RaycastHit2D _raycast = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, _layer);
        return _raycast.collider != null;
    }
    #endregion

    void PlayerDirection()
    {
        if (_horizontalMove > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (_horizontalMove < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
