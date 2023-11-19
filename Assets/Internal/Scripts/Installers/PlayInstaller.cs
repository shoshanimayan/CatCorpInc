using Managers;
using UnityEngine;
using Utility;
using Zenject;
using Player;

public class PlayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindMediatorView<PlayerControllerMediator, PlayerControllerView>();

    }
}