using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using NPC;

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
            NPCView npc;
            if (collision.gameObject.TryGetComponent<NPCView>(out npc))
            {
                npc.CoffeeInteraction();
            }
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
