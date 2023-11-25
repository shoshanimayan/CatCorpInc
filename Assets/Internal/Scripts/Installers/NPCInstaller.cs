using NPC;
using Ui;
using UnityEngine;
using Utility;
using Zenject;

public class NPCInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindMediatorView<NPCMediator, NPCView>();

    }
}