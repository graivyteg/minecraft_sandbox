using Camera;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private UnityEngine.Camera _camera;

        public override void InstallBindings()
        {
            Container.Bind<UnityEngine.Camera>().FromInstance(_camera).AsSingle();
            Container.Bind<CursorDetector>().FromComponentOn(_camera.gameObject).AsSingle();
        }
    }
}