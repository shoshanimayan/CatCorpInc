using Audio;
using Menu;
using UnityEngine;
using Utility;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindMediatorView<MenuMediator, MenuView>();

    }
}