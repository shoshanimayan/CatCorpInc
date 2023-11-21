using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace Ui
{
	public class ObjectiveUI: MonoBehaviour
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] TextMeshProUGUI _name;
        [SerializeField] TextMeshProUGUI _description;
        [SerializeField] Image _checkSpot;
        [SerializeField] Sprite _checkmark;



        ///  PRIVATE VARIABLES         ///
        private Objective _objective;
        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///

        ///  PUBLIC API                ///
        public void SetObjective(Objective obj)
        { 
            _name.text = obj.Name;
            _description.text = obj.Description;
            _objective = obj;
        }

        public void Completed()
        { 
            _checkSpot.sprite = _checkmark;
        }

        public bool ObjectiveEquals(Objective objective)
        {
            return objective == _objective;
        }
        ///  IMPLEMENTATION            ///

    }
}
