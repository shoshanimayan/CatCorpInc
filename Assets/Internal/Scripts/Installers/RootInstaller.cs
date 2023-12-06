using Managers;
using UnityEngine;
using Utility;
using Zenject;
using Ui;

public class RootInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //signal

        //binding
        Container.BindMediatorView<SceneManagerMediator, SceneManagerView>();
        Container.BindMediatorView<LoadingUIMediator, LoadingUIView>();

    }
}