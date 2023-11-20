using Gameplay;
using UnityEngine;
using Utility;
using Zenject;

public class CollectableInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindMediatorView<CollectableMediator, CollectableView>();

    }
}