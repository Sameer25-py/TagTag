using UnityEngine;

namespace Gameplay.Grid
{
    public static class GridInfo
    {
        public static readonly int MinRowIndex    = -12;
        public static readonly int MinColumnIndex = -9;

        public static readonly int MaxRowIndex    = 22;
        public static readonly int MaxColumnIndex = 11;


        public static Vector3Int GetRandomIndexInGrid()
        {
            return new Vector3Int(
                Random.Range(MinRowIndex, MaxRowIndex),
                Random.Range(MinColumnIndex, MaxColumnIndex)
            );
        }
    }
}