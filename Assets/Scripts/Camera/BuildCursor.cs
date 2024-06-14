using System;
using Building;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Camera
{
    public class BuildCursor : BaseCursor
    {
        [Inject] private BuildingGrid _grid;
        
        protected override void OnPositionChanged(Vector3 cursorPosition)
        {
            var gridPosition = _grid.SnapPosition(cursorPosition, true);

            transform.position = _grid.IsInGrid(gridPosition) 
                ? _grid.GetRealPosition(gridPosition) 
                : cursorPosition;
        }
    }
}