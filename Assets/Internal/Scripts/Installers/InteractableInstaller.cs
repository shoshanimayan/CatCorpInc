using Managers;
using UnityEngine;
using Utility;
using Zenject;
using Gameplay;


public class InteractableInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindMediatorView<InteractableMediator, InteractableView>();

    }
}