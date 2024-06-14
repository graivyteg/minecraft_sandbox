using System.Collections.Generic;
using System.IO;
using Blocks;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace Tools
{
    public class BlockSOGenerator : MonoBehaviour
    {
        [SerializeField] private List<Texture2D> _textures;

        [Button]
        private void GenerateBlocks()
        {
            foreach (var texture in _textures)
            {
                var so = ScriptableObject.CreateInstance<BlockSO>();
                so.Key = texture.name;
                so.Texture = texture;

                AssetDatabase.CreateAsset(so, $"Assets/Resources/Blocks/{so.Key}.asset");
            }
            AssetDatabase.SaveAssets();
        }
    }
}