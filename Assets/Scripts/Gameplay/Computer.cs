using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using Random = System.Random;

namespace Gameplay
{
    public class Computer : Character
    {
        private Vector2 _newPosition = Vector2.zero;

        private Queue<Vector2> _directionCombo = new();

        private List<Vector2> _directions = new()
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        private void Start()
        {
            _newPosition = Rb2D.position;
            PopulateCombo();
        }

        private void PopulateCombo()
        {
            for (int i = 0; i < 10; i++)
            {
                _directionCombo.Enqueue(_directions[UnityEngine.Random.Range(0, _directions.Count)]);
            }
        }

        protected override void Update()
        {
            base.Update();
            if (!EnableMovement)
            {
                MoveDirection = Vector2.zero;
                return;
            }

            if (Rb2D.position == _newPosition && _directionCombo.TryDequeue(out Vector2 direction))
            {
                Vector2 candidateNewPosition = Rb2D.position + direction * (Time.deltaTime * Speed);
                RaycastHit2D hit = Physics2D.Raycast(Rb2D.position, direction, 0.2f,
                    ~LayerMask);
                if (hit.collider == null)
                {
                    MoveDirection = direction;
                    _newPosition  = candidateNewPosition;
                }
                else
                {
                    _newPosition = Rb2D.position;
                }
                
                if (_directionCombo.Count == 0)
                {
                    PopulateCombo();
                }
            }
        }

        private void FixedUpdate()
        {
            _newPosition = Rb2D.position + MoveDirection * (Time.deltaTime * Speed);
            RaycastHit2D hit = Physics2D.Raycast(Rb2D.position, MoveDirection, 0.2f,
                ~LayerMask);
            if (hit.collider == null)
            {
                Rb2D.MovePosition(_newPosition);
            }
            else
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }
}