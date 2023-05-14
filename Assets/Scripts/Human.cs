using UnityEngine;

namespace TagTag
{
    public class Human : Brain
    {
        private bool _isFirstInput = true;

        private delegate void MovementFunction();

        private MovementFunction _lastCalledMovement = null;

        private void MoveUp()
        {
            MoveInDirection(Vector3Int.up);
            _lastCalledMovement = MoveUp;
        }

        private void MoveDown()
        {
            MoveInDirection(Vector3Int.down);
            _lastCalledMovement = MoveDown;
        }

        private void MoveLeft()
        {
            MoveInDirection(Vector3Int.left);
            _lastCalledMovement = MoveLeft;
        }

        private void MoveRight()
        {
            MoveInDirection(Vector3Int.right);
            _lastCalledMovement = MoveRight;
        }

        private void InputMethod()
        {
# if UNITY_EDITOR
            if (Input.GetKey(KeyCode.UpArrow))
            {
                MoveUp();
            }

            else if (Input.GetKey(KeyCode.DownArrow))
            {
                MoveDown();
            }

            else if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }

            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            else
            {
                _isFirstInput = false;
            }

#endif

            float hor = SimpleInput.GetAxis("Horizontal");
            float ver = SimpleInput.GetAxis("Vertical");

            if (hor == 0f && ver == 0f)
            {
                _isFirstInput = true;
                return;
            }

            if (Mathf.Abs(hor) >= Mathf.Abs(ver))
            {
                if (hor > 0)
                {
                    MoveRight();
                }
                else
                {
                    MoveLeft();
                }
            }
            else
            {
                if (ver > 0)
                {
                    MoveUp();
                }
                else
                {
                    MoveDown();
                }
            }
        }

        protected override void Update()
        {
            if (_isFirstInput)
            {
                _isFirstInput = false;
                InputMethod();
                return;
            }

            base.Update();
            if (ElapsedTime < PollingRate) return;
            ElapsedTime = 0f;
            InputMethod();
        }
    }
}