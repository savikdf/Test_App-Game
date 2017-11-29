using UnityEngine;
using System.Collections;
using Player_NameSpace;

namespace Spawn_NameSpace{
	public class Spawn_Manager : MonoBehaviour {	
	//VARS
		//instance
		public static Spawn_Manager instance;
		//lists
		public Player_Manager[] Active_Players = new Player_Manager[6];
		//objects
		public GameObject player_Object;
		//data
		[HideInInspector] public bool is_Spawn_Ready = false;

	//STARTS
		void OnEnable(){
			is_Spawn_Ready = false;
			Setup_Spawn_System ();	
		}

		void Setup_Spawn_System(){
			Active_Players = players_In_Scene;

			is_Spawn_Ready = true;
		}

	//SPAWNS- SETUPS
		Player_Manager[] players_In_Scene{
			get{ 
				Player_Manager[] return_Array;
				//check
				return_Array = FindObjectsOfType<Player_Manager>();
				//return
				if (return_Array != null) {
					//Debug.Log (return_Array.Length);
					return return_Array;
				} else {
					return null;
				}
			}
		}






	}
}