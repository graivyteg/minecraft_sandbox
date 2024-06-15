using System;
using UnityEngine;

namespace Blocks
{
    public class Block : MonoBehaviour
    {
        private BlockSO _so;

        public void Init(BlockSO so)
        {
            _so = so;

            GetComponent<Renderer>().material.mainTexture = _so.Texture;
        }
    }
}