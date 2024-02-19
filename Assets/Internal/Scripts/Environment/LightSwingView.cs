using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace Environment
{
	public class LightSwingView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private Transform _spotLight;
		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  PUBLIC API                ///
		public void EnableSwing()
        {

            _spotLight.DORotate(new Vector3(85,0,0),2).SetLoops(-1,LoopType.Yoyo).SetId("swing");
        }

        public void DisableSwing()
        {
			DOTween.Kill("swing");
        }
    }
}
