using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInventoryManager>().To<InventoryManager_V3>().FromComponentInHierarchy().AsTransient();

        Container.Bind<IShootModel>().To<ShootModel>().FromComponentInHierarchy().AsSingle();

        // Bind for SaveLoadItemsData
        Container.Bind<ISaveLoad>().WithId("ItemsData").To<SaveLoadItemsData>().FromComponentInHierarchy().AsSingle().NonLazy();

        // Bind for SaveLoadSlotsData
        Container.Bind<ISaveLoad>().WithId("SlotsData").To<SaveLoadSlotsData>().FromComponentInHierarchy().AsSingle().NonLazy();

        // Bind for SaveLoadAmmoData
        Container.Bind<ISaveLoad>().WithId("AmmoData").To<SaveLoadAmmoData>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
