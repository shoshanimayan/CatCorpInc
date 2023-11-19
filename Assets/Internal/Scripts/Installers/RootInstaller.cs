using Managers;
using UnityEngine;
using Utility;
using Zenject;

public class RootInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //signal

        //binding
        Container.BindMediatorView<SceneManagerMediator, SceneManagerView>();

    }
}