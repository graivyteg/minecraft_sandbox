using Blocks;
using Building;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BuildingInstaller : MonoInstaller
    {
        [SerializeField] private Block _blockPrefab;
        [SerializeField] private BuildingGrid _grid;
        [SerializeField] private string _blocksFolder;

        private BlockBuilder _blockBuilder;
        
        public override void InstallBindings()
        {
            _blockBuilder = new BlockBuilder(_blockPrefab, _blocksFolder);
            
            Container.Bind<BlockBuilder>().FromInstance(_blockBuilder).AsSingle();
            Container.Bind<BuildingGrid>().FromInstance(_grid).AsSingle();
        }
    }
}