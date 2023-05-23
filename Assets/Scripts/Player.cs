using System;
using Gameplay;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : Character
    {
        private Vector2 _newPosition;

        protected override void Update()
        {
            base.Update();
            if (!EnableMovement) return;
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.D))
            {
                MoveDirection = Vector2.right;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                MoveDirection = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                MoveDirection = Vector2.down;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                MoveDirection = Vector2.left;
            }
            else
            {
                MoveDirection = Vector2.zero;
            }
#endif
            float hor = SimpleInput.GetAxis("Horizontal");
            float ver = SimpleInput.GetAxis("Vertical");

            MoveDirection = new Vector2(hor, ver);
        }

        private void FixedUpdate()
        {
            _newPosition = Rb2D.position + MoveDirection * (Time.deltaTime * Speed);

            RaycastHit2D hit = Physics2D.Raycast(Rb2D.position, MoveDirection, 0.3f / 1.5f,
                ~LayerMask);
            if (hit.collider == null)
            {
                Rb2D.MovePosition(_newPosition);
            }
        }
    }
}