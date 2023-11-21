using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using Player;

namespace Ui
{
	public class ChecklistUIView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private GameObject _objectiveUIPrefab;
        [SerializeField] private Transform _contentContainer;
        [SerializeField] private Canvas _objectiveCanvas;
        ///  PRIVATE VARIABLES         ///
        private ObjectiveUI[] _objectives;
        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            
           // _objectiveCanvas.enabled = false;
           // Debug.Log(_objectiveCanvas.enabled);
        }

        
        ///  PUBLIC API                ///

        public void SetObjectives(Objective[] objectives)
        { 
            List<ObjectiveUI> list = new List<ObjectiveUI>();
            foreach (Objective obj in objectives)
            {
                GameObject o= Instantiate(_objectiveUIPrefab, _contentContainer);
                ObjectiveUI ui = o.GetComponent<ObjectiveUI>();
                ui.SetObjective(obj);
                list.Add(ui);

            }
            _objectives = list.ToArray();
        }

        public void completeObjectiveUI(Objective objective)
        {   
            foreach (ObjectiveUI obj in _objectives)
            {

                if (obj.ObjectiveEquals(objective))
                {
                    obj.Completed();
                    return;
                }
            }
        }

        public void EnableObjectiveCanvas(bool enable)
        { 
            _objectiveCanvas.enabled=enable;
        }


    }
}
