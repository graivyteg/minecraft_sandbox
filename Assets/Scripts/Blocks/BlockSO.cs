using UnityEngine;

namespace Blocks
{
    [CreateAssetMenu(fileName = "Block", menuName = "Blocks/Block", order = 0)]
    public class BlockSO : ScriptableObject
    {
        public string Key;
        public Texture2D Texture;
    }
}