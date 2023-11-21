using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Managers
{
	public class GameSettings
	{
		//gameplay settings
		private bool _headBob=true;
		private bool _canShoot = true;
		private bool _ended = false;


		private void SetHeadBob(bool CanHeadBob = true)
		{ 
			_headBob = CanHeadBob;
		}

		public bool CanHeadBob() {
			return _headBob;
		}

		private void SetCanShoot(bool canShot)
		{		
			_canShoot= canShot;
		}

		public bool GetCanShoot() { return _canShoot; }


		public void SetEnded(bool ended) { _ended = ended; }
		public bool GetEnded() { return _ended;}

	}
}
