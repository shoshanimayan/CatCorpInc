using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
namespace Gameplay
{
	public class CoffeeParticleView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		private ParticleSystem _particleSystem;
        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
        ///  PUBLIC API                ///
        public void SetAndPlay(Vector3 position) 
        {
            transform.position = position;  
            _particleSystem?.Play();
        }

        public void KillParticle()
        { 
            _particleSystem?.Stop();
        }
    }
}
