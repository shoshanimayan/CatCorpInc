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
        Container.BindMediatorView<CameraFocuserMediator, CameraFocuserView>();
        Container.BindMediatorView<CoffeeManagerMediator, CoffeeManagerView>();
        Container.BindMediatorView<TextTypeMediator, TextTypeView>();
        Container.BindMediatorView<DragPanelMediator, DragPanelView>();
        Container.BindMediatorView<DraggableMediator, DraggableView>();
        Container.BindMediatorView<CoffeeParticleMediator, CoffeeParticleView>();
        Container.BindMediatorView<TimerMediator,TimerView>();


        //signals
        Container.DeclareSignal<WalkStateChangedSignal>();
        Container.DeclareSignal<HoveringSignal>();
        Container.DeclareSignal<ChecklistCompletionCheckSignal>();
        Container.DeclareSignal<ObjectiveListSignal>();
        Container.DeclareSignal<ChoiceListSignal>();
        Container.DeclareSignal<ChoiceSendSignal>();
        Container.DeclareSignal<SendTextSignal>();
        Container.DeclareSignal<ChangeReadStateSignal>();
        Container.DeclareSignal<ChangedReadStateSignal>();
        Container.DeclareSignal<ProgressReaderSignal>();
        Container.DeclareSignal<FinishStepSignal>();
        Container.DeclareSignal<UpdateObjectiveCountSignal>();
        Container.DeclareSignal<SendTypedMessageSignal>();
        Container.DeclareSignal<SetTypingSignal>();
        Container.DeclareSignal<SetDragSignal>();
        Container.DeclareSignal<FinishDragSignal>();
        Container.DeclareSignal<SendCoffeePositionSignal>();






    }
}