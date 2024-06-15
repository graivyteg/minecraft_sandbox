using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UniRx;
using UnityEngine;

namespace Building
{
    [Serializable]
    public class Map
    {
        [SerializeField, ReadOnly] private List<MapBlockInfo> _blocks;

        public ReactiveCommand<MapBlockInfo> OnBuild = new();

        public Map()
        {
            _blocks = new List<MapBlockInfo>();
        }
        
        public Map(List<MapBlockInfo> blocks)
        {
            _blocks = new List<MapBlockInfo>(blocks);
        }

        public bool TryBuild(Vector3Int position, string key)
        {
            if (!IsFree(position)) return false;
            
             var blockInfo = new MapBlockInfo(position, key);
             _blocks.Add(blockInfo);

             OnBuild.Execute(blockInfo);
             
             return true;
        }

        public bool IsFree(Vector3Int position)
        {
            return !TryGetBlock(position, out var info);
        }

        public bool TryGetBlock(Vector3Int position, out MapBlockInfo info)
        {
            info = _blocks.Find(x => x.Position == position);
            return info != null;
        }
    }
    
}