using UnityEngine;

namespace TagTag
{
    public class Human : Brain
    {
        private void MoveUp()
        {
            MoveInDirection(Vector3Int.up);
        }

        private void MoveDown()
        {
            MoveInDirection(Vector3Int.down);
        }

        private void MoveLeft()
        {
            MoveInDirection(Vector3Int.left);
        }

        private void MoveRight()
        {
            MoveInDirection(Vector3Int.right);
        }

        protected override void Update()
        {
            base.Update();
            if (ElapsedTime < PollingRate) return;
            ElapsedTime = 0f;
            
# if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveUp();
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveDown();
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }

            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }

#endif

            float hor = SimpleInput.GetAxis("Horizontal");
            float ver = SimpleInput.GetAxis("Vertical");

            if (hor == 0f && ver == 0f) return;

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
    }
}