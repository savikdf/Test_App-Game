using UnityEngine;
using System.Collections;

using Camera_NameSpace;
using Camera_NameSpace.TriggerManager;

namespace Camera_NameSpace.TriggerManager{
	public class Camera_Target_Change_Trigger : MonoBehaviour {
	//VARS
		//Objects
		public int trigger_Index;
		[HideInInspector] public Transform new_Look_At_Target;

	//STARTS
		void Awake(){
			new_Look_At_Target = transform.GetChild (0);
		}

	//TRIGGER HITS
		void OnTriggerEnter(Collider col){
			if (col.gameObject.CompareTag ("Player")) {
				Camera_Target_Change_Manager.instance.On_Trigger_Hit (trigger_Index);
				//Camera_Target_Change_Manager.instance.change_Triggers.Remove (this);
				//Destroy (gameObject);
			}	
		}

	}
}
