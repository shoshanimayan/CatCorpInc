using Managers;
using UnityEngine;
using Utility;
using Zenject;
using Player;
using Signals.Game;
using Gameplay;
using Ui;

public class PlayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindMediatorView<PlayerControllerMediator, PlayerControllerView>();
        Container.BindMediatorView<HeadBobMediator, HeadBobView>();
        Container.BindMediatorView<ShooterMediator, ShooterView>();
        Container.BindMediatorView<InteractorMediator, InteractorView>();
        Container.BindMediatorView<CrosshairMediator, CrosshairView>();
        Container.BindMediatorView<NotificationMediator, NotificationView>();
        Container.BindMediatorView<ChecklistMediator, ChecklistView>();
        Container.BindMediatorView<GameUIManagerMediator, GameUIManagerView>();
        Container.BindMediatorView<GameManagerMediator, GameManagerView>();




        Container.DeclareSignal<WalkStateChangedSignal>();
        Container.DeclareSignal<HoveringSignal>();
        Container.DeclareSignal<ChecklistCompletionCheck>();


    }
}