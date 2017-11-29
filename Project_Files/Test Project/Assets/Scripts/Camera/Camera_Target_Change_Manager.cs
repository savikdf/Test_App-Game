using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Camera_NameSpace;

namespace Camera_NameSpace.TriggerManager{
	public class Camera_Target_Change_Manager : MonoBehaviour {
	//VARS
		//static instances
		public static Camera_Target_Change_Manager instance;
		//objects
		[HideInInspector] public List<Camera_Target_Change_Trigger> change_Triggers;
		// = new List<Camera_Target_Change_Trigger>();
		//int current_Trigger_Index = 0;

	//STARTS
		void Awake(){
			if (!instance) 
				instance = this;			
			for (int i = 0; i < transform.childCount; i++) {
				change_Triggers.Add( transform.GetChild (i).GetComponent<Camera_Target_Change_Trigger> ());
			}
		}
	//SETUPS
		public void Setup_Triggers(){
			try{				
				for (int i = 0; i < change_Triggers.Count; i++) {
					change_Triggers [i].trigger_Index = i; 								//setting the list index of the trigger.
					change_Triggers[i].GetComponent<MeshRenderer>().enabled = false;	//making the triggers invisible
				}
				Debug.Log("done");
			}catch{
				Debug.LogError("Error setting the Setup_Triggers function.");
			}
		}
	//TRIGGER HITS
		public void On_Trigger_Hit(int index_Of_Hit){
			Debug.Log ("Trig hit");
//			try{
				//Transform old = Camera_Smooth_Follow.instance.look_At_Object;
				Camera_Smooth_Follow.instance.target_Player_For_Target = false;
				Camera_Smooth_Follow.instance.look_At_Object = change_Triggers [index_Of_Hit].new_Look_At_Target;	//telling the camera to look at the new trigger.
				//current_Trigger_Index = index_Of_Hit;
				//Destroy(old.gameObject);	//destorying the old look at target's gameobject

//			}catch{
//				Debug.LogError ("Error with the On_Trigger_Hit Function!");
//			}
		}

	
	}
}