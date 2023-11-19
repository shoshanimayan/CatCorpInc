using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using Player;
using UnityEngine.InputSystem.XR;

namespace Managers
{
	public class SceneManagerView: MonoBehaviour,IView
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] AssetReference _sceneMenu;
        [SerializeField] AssetReference _sceneLevel;
        ///  PRIVATE VARIABLES         ///
        private SceneManagerMediator _mediator;
        ///  PRIVATE METHODS           ///
       
        ///  PUBLIC API                ///

        public void Init(SceneManagerMediator mediator)
        {
            _mediator = mediator;
        }

        public AssetReference GetMenuAsset() { return _sceneMenu; }
        public AssetReference GetLevelAsset() { return _sceneLevel; }

    }
}
