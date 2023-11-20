using Managers;
using UnityEngine;
using Utility;
using Zenject;
using Player;
using Signals.Game;
using Gameplay;

public class PlayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindMediatorView<PlayerControllerMediator, PlayerControllerView>();
        Container.BindMediatorView<HeadBobMediator, HeadBobView>();
        Container.BindMediatorView<ShooterMediator, ShooterView>();
        Container.BindMediatorView<InteractorMediator, InteractorView>();
        Container.BindMediatorView<CrosshairMediator, CrosshairView>();

        Container.DeclareSignal<WalkStateChangedSignal>();
        Container.DeclareSignal<HoveringSignal>();


    }
}