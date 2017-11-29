using UnityEngine;
using System.Collections;

using GlobalData_NameSpace;
using Player_NameSpace;

namespace Player_NameSpace{
	[RequireComponent (typeof (Rigidbody))]
	[RequireComponent (typeof (SphereCollider))]


    public class Player_Manager : MonoBehaviour {
	//VARS
		//instance
		public static Player_Manager instance;
		//Components
		public Player_Class player_Class;
		[HideInInspector] public Player_Controls player_Controls;
		public PhysicMaterial default_Hamster_PHY_Mat;
		//objects
		[HideInInspector] public GameObject player_Object;
		//data
		float gravity_Accel = 9.8f;


	//STARTS
		void OnEnable(){
			if (!instance)
				instance = this;
			
			Set_Up_Player ();
			//setting player up
		}

		void Set_Up_Player() {
            player_Class = new Player_Class();
            player_Object = gameObject;
            player_Controls = gameObject.AddComponent<Player_Controls>();
            //setting the defaults for the components
            GetComponent<SphereCollider>().radius = 1f;
            GetComponent<SphereCollider>().material = default_Hamster_PHY_Mat;
            player_Controls.Set_Up_Controls();
        }

        void Start() {
            Change_Player_Physics();
        }

	//GAME MANAGER COMMUNICATION
		public void AllowPlayerToMove(bool canMove){
			player_Controls.Set_PlayerCanMove (canMove);
		}             

    //COMPONENT COMMUNICATION
        void Change_Player_Physics(){
            player_Controls.player_Rigid_Body.mass = 5.0f;
        }

	}
}
