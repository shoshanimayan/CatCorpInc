using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Managers
{
	public class GameSettings: MonoBehaviour
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
