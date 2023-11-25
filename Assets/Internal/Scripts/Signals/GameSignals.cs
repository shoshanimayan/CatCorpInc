using Gameplay;
using NPC;
using Player;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace Signals.Game
{
    public class WalkStateChangedSignal
    {
        public WalkState ToState;
    }

    public class HoveringSignal
    {
        public string Hovering;
    }


    public class ChecklistCompletionCheckSignal
    {
        
    }

    public class ObjectiveListSignal
    {
        public Objective[] Objectives;
    }


    public class ChoiceListSignal
    {
        public string[] Choices;
    }

    public class ChoiceSendSignal
    {
        public int Choice;
    }

    public class SendTextSignal
    { 
        public  TextObject Text;
    }

    public class ChangeReadStateSignal
    { 
       public  ReadState ReadState;
    }

    public class ChangedReadStateSignal
    {
       public  ReadState ReadState;
    }

    public class SendTextStepSignal {
        public TextStep TextStep;
        public Interactable Origin = null;
    }


    public class ProgressReaderSignal { }

    public class FinishStepSignal { }


}
