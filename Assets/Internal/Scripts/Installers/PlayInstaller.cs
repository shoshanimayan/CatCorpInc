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
        //binding
        Container.BindMediatorView<PlayerControllerMediator, PlayerControllerView>();
        Container.BindMediatorView<HeadBobMediator, HeadBobView>();
        Container.BindMediatorView<ShooterMediator, ShooterView>();
        Container.BindMediatorView<InteractorMediator, InteractorView>();
        Container.BindMediatorView<CrosshairMediator, CrosshairView>();
        Container.BindMediatorView<NotificationMediator, NotificationView>();
        Container.BindMediatorView<ChecklistMediator, ChecklistView>();
        Container.BindMediatorView<ChecklistUIMediator, ChecklistUIView>();
        Container.BindMediatorView<GameUIManagerMediator, GameUIManagerView>();
        Container.BindMediatorView<GameManagerMediator, GameManagerView>();
        Container.BindMediatorView<ReaderMediator, ReaderView>();
        Container.BindMediatorView<TextDispalyMediator, TextDispalyView>();
        Container.BindMediatorView<TextChoiceMediator, TextChoiceView>();


        //signals
        Container.DeclareSignal<WalkStateChangedSignal>();
        Container.DeclareSignal<HoveringSignal>();
        Container.DeclareSignal<ChecklistCompletionCheckSignal>();
        Container.DeclareSignal<ObjectiveListSignal>();
        Container.DeclareSignal<ChoiceListSignal>();
        Container.DeclareSignal<ChoiceSendSignal>();
        Container.DeclareSignal<SendTextSignal>();
        Container.DeclareSignal<SendTextStepSignal>();
        Container.DeclareSignal<ChangeReadStateSignal>();
        Container.DeclareSignal<ChangedReadStateSignal>();
        Container.DeclareSignal<ProgressReaderSignal>();
        Container.DeclareSignal<FinishStepSignal>();






    }
}