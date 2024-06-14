using Building;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BuildingInstaller : MonoInstaller
    {
        [SerializeField] private BuildingGrid _grid;
        
        public override void InstallBindings()
        {
            Container.Bind<BuildingGrid>().FromInstance(_grid).AsSingle();
        }
    }
}