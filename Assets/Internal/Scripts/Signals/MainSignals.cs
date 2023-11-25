using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Signals.Core
{
    public class CameraFocusSignal
    {
        public Transform Focus;
    }
    public class StateChangeSignal
    {
        public State ToState;
    }

    public class StateChangedSignal
    {
        public State ToState;
    }

    public class LoadSceneSignal
    {
        public State StateToLoad;
    }

    public class AudioEffectSignal
    {
        public string AudioEffectName;
    }

    public class StartGameSignal
    {

    }

    public class EndedGameSignal
    {

    }

    public class EndingGameSignal
    {

    }

    public class ObjectiveCompleteSignal 
    {
        public Objective Objective;
    }

    public class ObjectiveCompletedSignal
    {
        public Objective Objective;
    }

    public class GotCollectableSignal
    {
    }
}
