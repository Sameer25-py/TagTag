using System;
using UnityEngine;

namespace TagTag
{
    public static class GridInfo
    {
        public static readonly int Rows    = 26;
        public static readonly int Columns = 16;

        public static readonly int MinRowIndex    = -Rows / 2;
        public static readonly int MinColumnIndex = -Columns / 2 - 1;

        public static readonly int MaxRowIndex    = Rows / 2;
        public static readonly int MaxColumnIndex = Columns / 2 - 1;


        public static Vector3Int GetRandomIndexInGrid()
        {
            return new Vector3Int(
                UnityEngine.Random.Range(MinRowIndex, MaxRowIndex),
                UnityEngine.Random.Range(MinColumnIndex, MaxColumnIndex)
            );
        }
    }
}