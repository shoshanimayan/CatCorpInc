using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using ScriptableObjects;
using Audio;

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
        public SceneState SceneToLoad;
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
        public int Key;
    }

    public class UnblockedConversationSignal
    {
        public TextStep Unblock;
    }

    public class PlayOneShotSignal
    {
        public string ClipName;
        public Vector3 WorldPos;
    }

    public class PlaySoundSignal
    {
        public string ClipName;
        public Vector3 WorldPos;
    }


    public class StopSoundSignal
    {
        public string ClipName;
    }

    public class StopAllSoundsSignal
    {
        public string ClipName;
    }


    public class PlayMusicSignal
    {
        public string ClipName;
    }

    public class PauseMusicSignal
    {
        public string ClipName;
    }

    public class TransitionMusicSignal
    {
        public MusicState musicState;
    }
}
