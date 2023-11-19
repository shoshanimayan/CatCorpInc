using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Managers
{
	public class GameSettings
	{

		private bool _headBob=true;



		private void SetHeadBob(bool CanHeadBob = true)
		{ 
			_headBob = CanHeadBob;
		}

		public bool CanHeadBob() {
			return _headBob;
		}

	}
}
