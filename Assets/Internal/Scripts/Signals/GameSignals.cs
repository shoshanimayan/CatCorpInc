using Gameplay;
using Player;
using ScriptableObjects;
using UnityEngine;

namespace Signals.Game
{
    public class WalkStateChangedSignal
    {
        public WalkState ToState;
    }

    

    public class HoveringSignal
    {
        public string Hovering;
        public bool DontShowInteract;
    }


    public class ChecklistCompletionCheckSignal
    {
        
    }

    public class ObjectiveListSignal
    {
        public Objective[] Objectives;
    }

    public class AddObjectiveSignal
    {
        public Objective Objective;
    }


    public class ChoiceListSignal
    {
        public string[] Choices;
    }

    public class SetTypingSignal
    {
        public string Prompt;
    }

    public class SetDragSignal
    {
        public string Message;
    }

    public class FinishDragSignal
    {
       
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

    public class SendStorageSignal
    { 
    
    }

    public class SendTextStepSignal {
        public TextStep TextStep;
        public Interactable Origin = null;
        public bool Storage=false;
    }


    public class ProgressReaderSignal { }

    public class FinishStepSignal { }

    public class  SetCoffeeSignal
    {
        
    }

    public class GotCoffeeSignal
    { 
    
    }

    public class SendCoffeePositionSignal
    {
        public Vector3 Position;
    }

    public class UpdateObjectiveCountSignal
    {
        public Objective Objective;
        public int total;
        public int current;
    }

    public class SendTypedMessageSignal
    {
        public string Message;
    }

    public class  EnableTimerSignal
    {
        
    }

    public class  DisableTimerSignal
    {
        
    }

    



}
