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

        private void Update()
        {
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
        }
    }
}