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

        [SerializeField] private string _blockKey;
        [Header("Colors")]
        [SerializeField] private Color _validColor = new Color(1, 1, 1, 0.5f);
        [SerializeField] private Color _invalidColor = new Color(1, 0, 0, 0.5f);

        private IDisposable _onMouseUpDisposable;
        private Renderer _renderer;
        private Vector3 _cursorPosition = Vector3.zero;

        protected override void OnEnable()
        {
            base.OnEnable();
            _onMouseUpDisposable = Observable.EveryUpdate()
                .Where(_ =>
                {
                    if (Input.touchCount > 0)
                        return Input.GetTouch(0).phase == TouchPhase.Ended;
                    return Input.GetMouseButtonUp(0);
                })
                .Subscribe(_ => OnMouseUp());
        }

        protected override void OnPositionChanged(Vector3 cursorPosition)
        {
            _cursorPosition = cursorPosition;
            Vector3Int gridPosition = _grid.SnapPosition(cursorPosition, true);

            bool isInGrid = _grid.IsInGrid(gridPosition);
            transform.position = isInGrid  
                ? _grid.GetRealPosition(gridPosition) 
                : cursorPosition;
            
            SetColor(isInGrid);
        }

        private void OnMouseUp()
        {
            if (_cursorPosition == Vector3.zero) return;

            var gridPosition = _grid.SnapPosition(_cursorPosition, true);
            if (!_grid.IsInGrid(gridPosition)) return;
            
            _grid.Build(gridPosition, _blockKey);
            SetColor(false);
        }

        private void SetColor(bool isValid)
        {
            if (_renderer == null) _renderer = GetComponent<Renderer>();
            
            _renderer.material.color = isValid
                ? _validColor
                : _invalidColor;
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            _onMouseUpDisposable?.Dispose();
        }
    }
}