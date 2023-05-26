using UnityEngine;

namespace Gameplay.Grid
{
    public static class GridInfo
    {
        public static readonly int MinRowIndex    = -13;
        public static readonly int MinColumnIndex = -13;

        public static readonly int MaxRowIndex    = 23;
        public static readonly int MaxColumnIndex = 12;


        public static Vector3Int GetRandomIndexInGrid()
        {
            return new Vector3Int(
                Random.Range(MinRowIndex, MaxRowIndex),
                Random.Range(MinColumnIndex, MaxColumnIndex)
            );
        }
    }
}