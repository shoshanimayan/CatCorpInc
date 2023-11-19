using Managers;
using UnityEngine;
using Utility;
using Zenject;
using Player;
using Signals.Game;

public class PlayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindMediatorView<PlayerControllerMediator, PlayerControllerView>();
        Container.BindMediatorView<HeadBobMediator, HeadBobView>();


        Container.DeclareSignal<WalkStateChangedSignal>();


    }
}