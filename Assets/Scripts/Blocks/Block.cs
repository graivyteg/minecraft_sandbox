using System;
using UnityEngine;

namespace Blocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private BlockSO _so;

        private void OnValidate()
        {
            GetComponent<Renderer>().material.SetTexture("_MainTex", _so.Texture);
        }
    }
}