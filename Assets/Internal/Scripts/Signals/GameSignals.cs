using Player;
using System.Collections;
using System.Collections.Generic;
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
    }
}
