using UnityEngine;
using Core;
using System.Collections.Generic;
using FMODUnity;
using ScriptableObjects;
using FMOD.Studio;
using Debug = UnityEngine.Debug;
using System.Linq;

namespace Audio
{
    public enum MusicState { }
    public class AudioManagerView : MonoBehaviour, IView
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private AudioLibrary _audioLibrarySounds;
        [SerializeField] private AudioLibrary _audioLibraryMusic;
        [SerializeField] private string _musicTransitionParameter;
        ///  PRIVATE VARIABLES         ///
        private EventReference _eventReference;
        private Dictionary<string, EventInstance> _instancesClips = new Dictionary<string, EventInstance>();
        private Dictionary<string, EventInstance> _instancesMusic = new Dictionary<string, EventInstance>();

        ///  PRIVATE METHODS           ///
        private void Start()
        {

        }
        ///  PUBLIC API                ///
        public void PlayOneShot(string sound, Vector3 worldPos)
        {
            _eventReference = new EventReference();

            if (_audioLibrarySounds.TryGetClip(sound, out _eventReference))
            {
                //RuntimeManager.PlayOneShot(_eventReference, worldPos);
                var instance = RuntimeManager.CreateInstance(_eventReference);
                _instancesClips.Add(sound, instance);
                if (worldPos != null)
                {
                    instance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));
                }
                instance.start();
                instance.release();
                _instancesClips.Remove(sound);

            }
            else
            {
                UnityEngine.Debug.LogError("could not find Event reference with key: " + sound);
            }

        }

        public void PlaySound(string sound, Vector3 worldPos)
        {
            _eventReference = new EventReference();

            if (_audioLibrarySounds.TryGetClip(sound, out _eventReference))
            {
                //RuntimeManager.PlayOneShot(_eventReference, worldPos);
                var instance = RuntimeManager.CreateInstance(_eventReference);
                _instancesClips.Add(sound, instance);
                if (worldPos != null)
                {
                    instance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));
                }
                instance.start();


            }
            else
            {
                Debug.LogError("could not find Event reference with key: " + sound);
            }
        }

        public void StopClipInstance(string sound) {
            if (_instancesClips.ContainsKey(sound))
            {

                _instancesClips[sound].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                _instancesClips[sound].release();
                _instancesClips.Remove(sound);
            }
        }

        public void KillAllClips() {

            foreach (var sound in _instancesClips.Keys) {
                _instancesClips[sound].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                _instancesClips[sound].release();
            }
            _instancesClips.Clear();

        }

        public void PlayMusic(string sound)
        {
            _eventReference = new EventReference();
            if (!_instancesClips.ContainsKey(sound))
            {
                if (_audioLibrarySounds.TryGetClip(sound, out _eventReference))
                {
                    //RuntimeManager.PlayOneShot(_eventReference, worldPos);
                    var instance = RuntimeManager.CreateInstance(_eventReference);
                    _instancesMusic.Add(sound, instance);

                    instance.start();


                }
                else
                {
                    Debug.LogError("could not find Event reference with key: " + sound);
                }
            }
            else
            {
                _instancesMusic[sound].setPaused(false);

            }
        }

        public void PauseMusic(string sound)
        {
            if (_instancesMusic.ContainsKey(sound))
            {

                _instancesMusic[sound].setPaused(true);


            }
        }

        public void TransitionMusicState(MusicState musicState)
        {
            string key = _instancesMusic.Keys.ElementAt(0);
            _instancesMusic[key].setParameterByName(_musicTransitionParameter, (float)musicState);



        }

    }
}
