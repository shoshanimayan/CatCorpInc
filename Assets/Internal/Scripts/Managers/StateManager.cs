using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UniRx;
using Zenject;
using Signals.Core;

namespace Managers
{
    public enum State { Menu, Play, Loading, Text,Paused,Objective }
    public class StateManager : IDisposable
    {

        ///  INSPECTOR VARIABLES      ///

        ///  PRIVATE VARIABLES         ///
        private State _state;

        ///  PRIVATE METHODS          ///

        ///  LISTNER METHODS          ///

        private void OnStateChanged(StateChangeSignal signal)
        {
            Debug.Log(signal.ToState);
            SetState(signal.ToState);
        }

        ///  PUBLIC API               ///

        public State GetState()
        {
            return _state;
        }

        private void SetState(State state)
        {
            if (_state != state)
            {
                _state = state;
                _signalBus.Fire(new StateChangedSignal() { ToState = state });

                
            }
        }


        ///    Implementation        ///

        readonly SignalBus _signalBus;
        readonly CompositeDisposable _disposables = new CompositeDisposable();

        public StateManager(SignalBus signalBus)
        {

            _signalBus = signalBus;
            _state = State.Loading;
            _signalBus.GetStream<StateChangeSignal>()
                       .Subscribe(x => OnStateChanged(x)).AddTo(_disposables);


        }




        public void Dispose()
        {

            _disposables.Dispose();

        }


    }
}
