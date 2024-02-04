using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;

namespace Gameplay
{
	public class ChecklistView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private Objective[] _startingObjectives;

		[SerializeField] private int _totalObjectives;

        ///  PRIVATE VARIABLES         ///
		private ChecklistMediator _mediator;
        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            
        }

        private void Start()
        {
            _mediator.InitializeObjectiveMenu(_startingObjectives);

        }

        ///  PUBLIC API                ///
        public Objective[] GetObjectives() { return _startingObjectives; }

		public int TotalObjectives { 
			get { return _totalObjectives; }
			private set { _totalObjectives = value; }
		}

		public void Initilize( ChecklistMediator mediator) { 
			_mediator = mediator;
		}

		public int AddObjective(Objective objective)
		{

			if (_startingObjectives.Contains(objective))
			{
				return 0;
			}


            List<Objective> objectiveList = _startingObjectives.ToList();
			objectiveList.Add(objective);
            _startingObjectives = objectiveList.ToArray();
			return 1;
         //   _mediator.InitializeObjectiveMenu(_objectives);


        }

        public void RemoveObjective(Objective obj)
		{
			if (_startingObjectives.Contains(obj))
			{
				List<Objective> list = new List<Objective>();
				foreach (Objective o in _startingObjectives)
				{
					if (o != obj)
					{ 
						list.Add(o);
					}
				}

                _startingObjectives = list.ToArray();
			
			}
		}
	}
}
