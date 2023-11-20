using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Gameplay
{
	public class Coffee: MonoBehaviour
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] float _deathTime = 2.5f;
        
        ///  PRIVATE VARIABLES         ///
        ///  PRIVATE METHODS           ///
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }

       
        private void Start()
        {
            Destroy(gameObject, _deathTime);
        }

        ///  LISTNER METHODS           ///

        ///  PUBLIC API                ///

        ///  IMPLEMENTATION            ///

    }
}
