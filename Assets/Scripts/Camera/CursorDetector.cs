using System;
using UniRx;
using UnityEngine;

namespace Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CursorDetector : MonoBehaviour
    {
        [SerializeField] private float _raycastDistance = 100;
        [SerializeField] private LayerMask _layerMask;
        
        private UnityEngine.Camera _camera;
        private IDisposable _disposable;

        public ReactiveProperty<Vector3> CursorPosition;

        private void OnEnable()
        {
            _camera = GetComponent<UnityEngine.Camera>();

            _disposable = Observable
                .EveryUpdate()
                .Subscribe(_ => SetCursorPosition());
        }

        private void SetCursorPosition()
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, _raycastDistance, _layerMask))
            {
                CursorPosition.Value = hit.point;
                return;
            }
            
            CursorPosition.Value = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        
        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}