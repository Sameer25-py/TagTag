using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public  float       Speed           = 2f;
    private bool        _enableMovement = true;
    private Rigidbody2D _rb2D;
    public  LayerMask   LayerMask;
    private Vector2     _newPosition;

    private Vector2 _direction2D;

    private void OnEnable()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _direction2D = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            _direction2D = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _direction2D = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _direction2D = Vector2.left;
        }
        else
        {
            _direction2D = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        _newPosition = _rb2D.position + _direction2D * (Time.deltaTime * Speed);

        RaycastHit2D hit = Physics2D.Raycast(_rb2D.position, _direction2D, 0.3f / 2,
            ~LayerMask);
        if (hit.collider == null)
        {
            _rb2D.MovePosition(_newPosition);
        }
    }
}