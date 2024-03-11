using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Signals.Core;
using Audio;

namespace Managers
{
	public class SceneManagerMediator: MediatorBase<SceneManagerView>, IInitializable, IDisposable
	{

       

            ///  INSPECTOR VARIABLES       ///

            ///  PRIVATE VARIABLES         ///
            private AsyncOperationHandle<SceneInstance> _handle;
            private bool _unloaded = true;
            private SceneState _stateLoading=SceneState.Test;
            ///  PRIVATE METHODS           ///



            private void Load(AssetReference scene)
            {
              //  _stateManager.SetState(State.Loading);

                Debug.Log("loading level");
                if (!_unloaded)
                {
                    _unloaded = true;
                    UnloadScene();
                }
                Addressables.LoadSceneAsync(scene, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += SceneLoadCompleted;
            }

            private void SceneLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
            {
                if (obj.Status == AsyncOperationStatus.Succeeded)
                {
                    _handle = obj;
                    _unloaded = false;
                    switch (_stateLoading)
                    {
                        case SceneState.Game:
                        _signalBus.Fire(new TransitionMusicSignal() { musicState = MusicState.song1 });
                        _signalBus.Fire(new StateChangeSignal() { ToState = State.Text });

                        break;
                        case SceneState.Menu:
                        _signalBus.Fire(new TransitionMusicSignal() { musicState = MusicState.menu });

                        _signalBus.Fire(new StateChangeSignal() { ToState = State.Menu });
                        break;
                    case SceneState.Test:
                        _signalBus.Fire(new StateChangeSignal() { ToState = State.Text });
                        break;
                }
                    Debug.Log(_stateLoading.ToString() + " loaded");
                }
            }

            private void UnloadScene()
            {
                Debug.Log("unloading " + _stateLoading.ToString());

                Addressables.UnloadSceneAsync(_handle, true).Completed += op =>
                {
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        Debug.Log("Successfully unloaded scene.");
                    }
                    else
                    {
                        Debug.Log(op.Status.ToString());
                    }
                };
            }
            ///  LISTNER METHODS           ///
            private void OnLoadSceneChanged(LoadSceneSignal signal)
            {


            _signalBus.Fire(new StateChangeSignal() { ToState = State.Loading });
            _stateLoading = signal.SceneToLoad;
                switch (_stateLoading)
                {
                    case SceneState.Game:

                    Load(_view.GetLevelAsset());
                        break;
                    case SceneState.Menu:

                    Load(_view.GetMenuAsset());
                        break;
                    default:
                        break;
                }
            }

            private void OnEndedGame()
            {
            _signalBus.Fire(new StateChangeSignal() { ToState = State.Loading });

            _stateLoading = SceneState.Menu;
                Load(_view.GetMenuAsset());
            }
            ///  PUBLIC API                ///


            ///  IMPLEMENTATION            ///

            [Inject]
            private StateManager _stateManager;

            [Inject]
            private SignalBus _signalBus;

            readonly CompositeDisposable _disposables = new CompositeDisposable();

            public void Initialize()
            {
                _signalBus.GetStream<LoadSceneSignal>()
                           .Subscribe(x => OnLoadSceneChanged(x)).AddTo(_disposables);
                _signalBus.GetStream<EndTransitionSignal>()
                         .Subscribe(x => OnEndedGame()).AddTo(_disposables);
                _view.Init(this);
                if (SceneManager.sceneCount == 1)
                {
                _signalBus.Fire(new StateChangeSignal() { ToState=State.Loading});
                    _stateLoading = SceneState.Menu;
                    Load(_view.GetMenuAsset());
                }
                else
                {
                    //just for testing in editor
                    if (SceneManager.GetSceneAt(0).name.Contains("Menu"))
                    {
                    Debug.Log("open from menu scene");
                    _signalBus.Fire(new StateChangeSignal() { ToState = State.Menu });
                    Cursor.lockState = CursorLockMode.Confined;


                }
                else
                    {
                    Cursor.lockState = CursorLockMode.Confined;

                    _signalBus.Fire(new StateChangeSignal() { ToState = State.Text });


                }
            }



            }

            public void Dispose()
            {

                _disposables.Dispose();

            }

        

    }
}
