using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Building
{
    public class BuildingGrid : MonoBehaviour
    {
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private float _blockSizeMultiplier = 1;
        [SerializeField] private Transform _debugPoint;

        private Dictionary<Vector3Int, string> _blocks = new ()
        {
            [new Vector3Int(3, 0, 0)] = "block",
            [new Vector3Int(0, 0, 3)] = "block",
            [new Vector3Int(3, 0, 3)] = "block"
        };

        public bool IsFree(Vector3Int gridPosition)
        {
            return !_blocks.ContainsKey(gridPosition);
        }
        
        public bool IsInGrid(Vector3Int gridPosition)
        {
            return gridPosition.x >= 0 && gridPosition.x < _gridSize.x
                && gridPosition.z >= 0 && gridPosition.z < _gridSize.y;
        }

        public Vector3Int SnapPosition(Vector3 position, bool onlyFree = false)
        {
            var result = Vector3Int.RoundToInt((position - transform.position) / _blockSizeMultiplier);
            result.y = Mathf.Max(result.y, 0);

            if (!onlyFree || IsFree(result)) return result;
            
            Debug.Log(result + " is busy");
            
            var direction = position - GetRealPosition(result);
            var absDirection = direction;
            
            absDirection.Set(
                Mathf.Abs(direction.x),
                Mathf.Abs(direction.y),
                Mathf.Abs(direction.z)
            );
            
            var maxValue = Mathf.Max(
                absDirection.x, 
                absDirection.y, 
                absDirection.z
            );

            if (Math.Abs(maxValue - absDirection.x) < 0.01f) result.x += direction.x > 0 ? 1 : -1;
            if (Math.Abs(maxValue - absDirection.y) < 0.01f) result.y += direction.y > 0 || result.y == 0 ? 1 : -1;
            if (Math.Abs(maxValue - absDirection.z) < 0.01f) result.z += direction.z > 0 ? 1 : -1;

            return result;
        }

        public Vector3 GetRealPosition(int x, int y, int z)
        {
            Vector3 tempPos = new Vector3(
                x * _blockSizeMultiplier,
                y * _blockSizeMultiplier,
                z * _blockSizeMultiplier
            );
            return transform.position + tempPos;
        }

        public Vector3 GetRealPosition(Vector3Int gridPosition)
            => GetRealPosition(gridPosition.x, gridPosition.y, gridPosition.z);

        private void OnDrawGizmos()
        {
            var pointPosition = Vector3Int.one * -1;
            if (_debugPoint != null)
            {
                pointPosition = SnapPosition(_debugPoint.position);   
            }

            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int z = 0; z < _gridSize.y; z++)
                {
                    var pos = new Vector3Int(x, 0, z);
                    if (pointPosition == pos)
                    {
                        Gizmos.color = Color.red;
                    }
                    else if (!IsFree(pos))
                    {
                        Gizmos.color = Color.yellow;
                    }
                    else
                    {
                        Gizmos.color = Color.white;
                    }
                    
                    Gizmos.DrawWireCube(
                            GetRealPosition(x, 0, z),  
                        Vector3.one * _blockSizeMultiplier
                    );
                }
            }
        }
    }
}