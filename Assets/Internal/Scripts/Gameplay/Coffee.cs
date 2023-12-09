using UnityEngine;
using Zenject;
using NPC;
using UnityEngine.Events;
using System;

namespace Gameplay
{
	public class Coffee: MonoBehaviour
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] float _deathTime = 2.5f;

        ///  PRIVATE VARIABLES         ///
        public delegate void OnHit(Vector3 pos);
        private OnHit _onHit;
        ///  PRIVATE METHODS           ///
        private void OnCollisionEnter(Collision collision)
        {
            NPCView npc;
            if (_onHit.GetInvocationList().GetLength(0)>0)
            {
                _onHit(collision.GetContact(0).point);
            }
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
        public void SetOnHit(OnHit todo)
        {
            _onHit=todo;
        }
        ///  IMPLEMENTATION            ///

    }
}
