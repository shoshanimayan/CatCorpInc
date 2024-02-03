using Gameplay;
using UnityEngine;
using Utility;
using Zenject;

public class DoorInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindMediatorView<DoorMediator, DoorView>();

    }
}