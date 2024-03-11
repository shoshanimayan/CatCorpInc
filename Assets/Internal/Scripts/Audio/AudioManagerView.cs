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
    public enum MusicState {song1,song2,menu, victory }
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
            PlayMusic(_audioLibraryMusic.GetAtIndex(0).key);
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
                if (worldPos != Vector3.zero)
                {
                    instance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));
                }
                else
                {

                    instance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));

                }
                instance.start();
                instance.release();
                _instancesClips.Remove(sound);

            }
            else
            {
                Debug.LogError("could not find Event reference with key: " + sound);
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
                if (worldPos != Vector3.zero)
                {
                    instance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));
                }
                else
                {

                    instance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));

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

        public void PlayMusic(string sound=null)
        {
            if (sound == null)
            {
                sound = _audioLibraryMusic.GetAtIndex(0).key;
            }
            _eventReference = new EventReference();
            if (!_instancesMusic.ContainsKey(sound))
            {
                if (_audioLibraryMusic.TryGetClip(sound, out _eventReference))
                {
                    //RuntimeManager.PlayOneShot(_eventReference, worldPos);
                    var instance = RuntimeManager.CreateInstance(_eventReference);
                    _instancesMusic.Add(sound, instance);
                  //  instance.set3DAttributes(RuntimeUtils.To3DAttributes());
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

        public void PauseMusic(string sound=null)
        {
            if (sound == null)
            {
                sound = _audioLibraryMusic.GetAtIndex(0).key;
            }
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
