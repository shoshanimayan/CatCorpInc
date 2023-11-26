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
        private bool _completed = false;
        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///

        ///  PUBLIC API                ///
        public void SetObjective(Objective obj)
        { 
            _name.text = obj.Name;
            _description.text = obj.Description;
            if (obj.HasCount)
            { 
                _description.text = _description.text+" "+obj.CurrentCount.ToString()+"/"+obj.Total.ToString();
            }
            _objective = obj;
        }

        public void UpdateCountUI(int total, int current)
        {
            _description.text = _objective.Description +" "+ current.ToString() + "/" + total.ToString();

        }

        public void Completed()
        {
            _completed = false;
            _checkSpot.sprite = _checkmark;
        }

        public bool ObjectiveEquals(Objective objective)
        {
            return objective == _objective;
        }

        public bool IsCompleted() { return _completed; }
        ///  IMPLEMENTATION            ///

    }
}
