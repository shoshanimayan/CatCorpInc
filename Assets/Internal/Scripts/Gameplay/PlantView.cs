using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;

namespace Gameplay
{
	public class PlantView: MonoBehaviour,IView
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private GameObject _wateringCan;
        [SerializeField] private ParticleSystem _waterParticles;
        ///  PRIVATE VARIABLES         ///

        private string _tweenKey;
        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            _tweenKey= Guid.NewGuid().ToString("N");
            _wateringCan.transform.localEulerAngles = new Vector3(_wateringCan.transform.localEulerAngles.x, _wateringCan.transform.localEulerAngles.y, 0);
            _wateringCan.SetActive(false);
        }

        private void PlayParticle()
        { 
        _waterParticles.Play();
        }

        private void KillWater()
        {
            _waterParticles.Stop(); _wateringCan.SetActive(false);
        }
        ///  PUBLIC API                ///
        public void StartWatering()
        {
            _wateringCan.SetActive(true);

            DOTween.Sequence()
            .Append(_wateringCan.transform.DOLocalRotate(new Vector3(_wateringCan.transform.localEulerAngles.x, _wateringCan.transform.localEulerAngles.y, 45), 0.25f, RotateMode.Fast).SetEase(Ease.Linear))
            .AppendInterval(.2f)
            .AppendCallback(PlayParticle)
            .AppendInterval(1.5f)
            .AppendCallback(KillWater)
            .OnKill(KillWater ) 
            .SetId(_tweenKey);
        }

       
        public void  StopWatering()
        {
            DOTween.Kill(_tweenKey);
        }

        

    }
}
