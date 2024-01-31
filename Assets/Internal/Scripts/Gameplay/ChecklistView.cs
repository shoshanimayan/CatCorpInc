using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay
{
	public class ChecklistView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private Objective[] _objectives;
        ///  PRIVATE VARIABLES         ///
		private ChecklistMediator _mediator;
        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            
        }

        private void Start()
        {
            _mediator.InitializeObjectiveMenu(_objectives);

        }

        ///  PUBLIC API                ///
        public Objective[] GetObjectives() { return _objectives; }

		public void Initilize( ChecklistMediator mediator) { 
			_mediator = mediator;
		}

		public int AddObjective(Objective objective)
		{

			if (_objectives.Contains(objective))
			{
				return 0;
			}


            List<Objective> objectiveList = _objectives.ToList();
			objectiveList.Add(objective);
			_objectives=objectiveList.ToArray();
			return 1;
         //   _mediator.InitializeObjectiveMenu(_objectives);


        }

        public void RemoveObjective(Objective obj)
		{
			if (_objectives.Contains(obj))
			{
				List<Objective> list = new List<Objective>();
				foreach (Objective o in _objectives)
				{
					if (o != obj)
					{ 
						list.Add(o);
					}
				}		

				_objectives= list.ToArray();
			
			}
		}
	}
}
