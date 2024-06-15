using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Blocks
{
    [Serializable]
    public class BlockBuilder
    {
        private string _folder;
        private Block _blockPrefab;
        
        private List<BlockSO> _blocks;

        public BlockBuilder(Block blockPrefab, string loadFolder)
        {
            _folder = loadFolder;
            _blockPrefab = blockPrefab;
        }


        public Block BuildBlock(Vector3 position, string key)
        {
            var so = GetBlockSO(key);

            var obj = Object.Instantiate(_blockPrefab);
            obj.transform.position = position;
            obj.Init(so);

            return obj;
        }
        
        public BlockSO GetBlockSO(string key)
        {
            if (_blocks == null) LoadBlocksSO();

            return _blocks.Find(x => x.Key == key);
        }

        public void LoadBlocksSO()
        {
            _blocks = Resources.LoadAll<BlockSO>(_folder).ToList();
        }
    }
}