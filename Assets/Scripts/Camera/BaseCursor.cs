using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class BaseCursor : MonoBehaviour
    {
        [Inject] private CursorDetector _detector;

        private IDisposable _disposable;

        private void OnEnable()
        {
            _disposable = _detector.CursorPosition
                .Subscribe(OnPositionChanged);
        }

        protected virtual void OnPositionChanged(Vector3 cursorPosition)
        {
            transform.position = cursorPosition;
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}