using Gameplay;
using UnityEngine;
using Utility;
using Zenject;

public class PlantInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindMediatorView<InteractableMediator, InteractableView>();
        Container.BindMediatorView<PlantMediator,PlantView>();

    }
}