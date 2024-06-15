using System;
using UnityEngine;

namespace Building
{
    [Serializable]
    public class MapBlockInfo
    {
        public Vector3Int Position;
        public string Key;

        public MapBlockInfo(Vector3Int position, string key)
        {
            Position = position;
            Key = key;
        }

        public override string ToString()
        {
            return $"{Position} - {Key}";
        }
    }
}