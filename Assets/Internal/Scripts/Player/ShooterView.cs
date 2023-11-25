using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine.AddressableAssets;
using Unity.VisualScripting;
using System.Threading;
using System.Threading.Tasks;

namespace Player
{
    [RequireComponent(typeof(InputReciever))]

    public class ShooterView: MonoBehaviour,IView
	{

        ///  INSPECTOR VARIABLES       ///
        [Header("References")]
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private AssetReferenceGameObject _objectThrownAsset;

        [Header("Settings")]
        [SerializeField] private float _throwForce;
        [SerializeField] private float _throwUpwardForce;
        [SerializeField] private float _coolDownSeconds=1;



        ///  PRIVATE VARIABLES         ///
        private ShooterMediator _mediator;
        private InputReciever _inputReciever;
        private bool _canShoot;
        private bool _cooledDown=true;
        private GameObject _shootObject;
        private Transform _cam;


        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            _inputReciever = GetComponent<InputReciever>();
            _objectThrownAsset.LoadAssetAsync<GameObject>().Completed += handle =>
            {
                _shootObject= handle.Result;
                Addressables.Release(handle);
            };

        }

        private void Start()
        {
            _cam = Camera.main.transform;

        }



        private async void DoCoolDownTimer()
        {
            await Task.Delay((int)(_coolDownSeconds * 1000));
            _cooledDown=true;
        }

        private void Throw()
        {
            _cooledDown = false;

            GameObject projectile = Instantiate(_shootObject, _attackPoint.position, _cam.rotation);

            Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

            Vector3 forceDirection = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0)).direction;

            RaycastHit hit;

            if (Physics.Raycast(_cam.position, _cam.forward, out hit, 500f))
            {
                forceDirection = (hit.point - _attackPoint.position).normalized;
            }
            Vector3 forceToAdd = forceDirection  * _throwForce + transform.up * _throwUpwardForce;

            projectileRB.AddForce(forceToAdd, ForceMode.Impulse);
            DoCoolDownTimer();
        }
        ///  PUBLIC API                ///
        public void Init(ShooterMediator mediator)
		{ 
			_mediator = mediator;
            _canShoot = _mediator.GetCanShoot();
		}


        public void Update() {
            _canShoot = _mediator.GetCanShoot();

            if (_canShoot && _inputReciever.PlayerFired() && _cooledDown)
            { 
                Throw();
            }

        }

        public void ForceCoolDown()
        {
            _cooledDown = false;
            DoCoolDownTimer();

        }
    }
}
