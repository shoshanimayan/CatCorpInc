using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using ScriptableObjects;
using FMOD.Studio;
using UnityEngine.UIElements;

namespace Audio
{
	public class AudioManagerView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private AudioLibrary _audioLibrarySounds;
        [SerializeField] private AudioLibrary _audioLibraryMusic;

        ///  PRIVATE VARIABLES         ///
        private EventReference _eventReference;
		private Dictionary<string, EventInstance> _instances = new Dictionary<string, EventInstance>();
		///  PRIVATE METHODS           ///
		
		///  PUBLIC API                ///
		public void PlayOneShot(string sound, Vector3 worldPos)
        {
			_eventReference = new EventReference();

			if (_audioLibrarySounds.TryGetClip(sound, out _eventReference))
			{
                //RuntimeManager.PlayOneShot(_eventReference, worldPos);
                var instance = RuntimeManager.CreateInstance(_eventReference);
				_instances.Add(sound, instance);
				if (worldPos != null)
				{
					instance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));
				}
                instance.start();
                instance.release();
				_instances.Remove(sound);

            }
			else
			{
				Debug.LogError("could not find Event reference with key: "+sound);
			}

        }

		public void PlaySound(string sound, Vector3 worldPos)
		{
            _eventReference = new EventReference();

            if (_audioLibrarySounds.TryGetClip(sound, out _eventReference))
            {
                //RuntimeManager.PlayOneShot(_eventReference, worldPos);
                var instance = RuntimeManager.CreateInstance(_eventReference);
                _instances.Add(sound, instance);
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

		public void StopInstance(string sound) {
			if (_instances.ContainsKey(sound))
			{
				
				_instances[sound].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                _instances[sound].release();
				_instances.Remove(sound);
            }
		}

		public void KillAllSounds() {

            foreach (var sound in _instances.Keys) {
                _instances[sound].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                _instances[sound].release();
            }
            _instances.Clear();

		}


	}
}
