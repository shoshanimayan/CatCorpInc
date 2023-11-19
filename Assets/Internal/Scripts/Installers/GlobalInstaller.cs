using Managers;
using Player;
using Signals.Core;
using UnityEngine;
using Utility;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        //signals
        Container.DeclareSignal<StateChangeSignal>();
        Container.DeclareSignal<StateChangedSignal>();
        Container.DeclareSignal<LoadSceneSignal>();
        Container.DeclareSignal<AudioEffectSignal>();
        Container.DeclareSignal<StartGameSignal>();
        Container.DeclareSignal<EndingGameSignal>();
        Container.DeclareSignal<EndedGameSignal>();


        //binding
        Container.Bind<StateManager>().AsSingle();
        Container.Bind<GameSettings>().AsSingle();






    }
}