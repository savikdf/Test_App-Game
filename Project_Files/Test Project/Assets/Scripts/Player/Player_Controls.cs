using UnityEngine;
using System.Collections;
using GlobalData_NameSpace;
using Player_NameSpace;

namespace Player_NameSpace{
	public class Player_Controls : MonoBehaviour {
	//VARS
		//components
		public Rigidbody player_Rigid_Body;
		Player_Class player_Class;
		//gameObjects
		Transform hamster;
		//data
		public bool playerCanMove = true;
		Vector3 forward, right;

		float gravity_Accel = 9.8f;
		RaycastHit ground_Hit;
		bool is_Grounded{
			get{ 
				//Debug.DrawLine (transform.position, transform.position + (Vector3.down * 2f), Color.red, 2f); 
				return Physics.SphereCast (transform.position, 0.3f, Vector3.down, out ground_Hit, 1f);
			}
		}
	//STARTS
		public void Set_Up_Controls(){
			player_Rigid_Body = GetComponent<Rigidbody> ();
            player_Class = GetComponent<Player_Manager>().player_Class;
			hamster = transform.FindChild ("HAM");
			//Debug.Log ("P_CONTROLS was successfully setup");
		}

	//MANAGER COMMUNICATION
		public void Set_PlayerCanMove(bool setState){
			playerCanMove = setState;
			StartCoroutine (Move_Player ());
		}

	//MOVEMENT
		IEnumerator Move_Player(){
			while (playerCanMove) {
				try{
					if(is_Grounded){
						player_Rigid_Body.AddForce(Move_Vec3_Gyro, ForceMode.Acceleration);		//ground movement
					}else{
						player_Rigid_Body.AddForce(Move_Vec3_Gyro / 3f, ForceMode.Acceleration);//air movement
					}
					if(Input.GetButtonDown("Jump")){
						player_Rigid_Body.AddForce(Move_Vec3_Gyro * 5f, ForceMode.Impulse);
					}
					Handle_Hamster_Rotation();
				}catch{		
					Debug.LogError ("An Error is Occuring witht the Move_Player function.");			
				}
				yield return null;
			}	
			Debug.Log ("Disabling Player Movement.");
		}

		//the movement acceleration vector for the hamster ball
		//Phone input Vec3
		Vector3 Move_Vec3_Gyro{
			get{ 				
				try{
					forward = Camera.main.transform.TransformDirection(Vector3.forward);
					right = Camera.main.transform.TransformDirection(Vector3.right);

					Vector3 return_Vec3 = Camera.main.transform.TransformDirection(Global_Data_Holder.input_Vector) * player_Class.acceleration;
					return_Vec3.y = 0f;

					//Debug.Log (Vector3.Dot (player_Rigid_Body.velocity.normalized, return_Vec3.normalized).ToString ());
					//This will only allow the User to move sideways and backwards when they are moving at max speed
					if(player_Rigid_Body.velocity.magnitude >= player_Class.topSpeed){
						if(Vector3.Dot(player_Rigid_Body.velocity.normalized , return_Vec3.normalized) < .2f){
							return return_Vec3;
						}else{
							return Vector3.zero;
						}	
					}else{
						return return_Vec3;
					}

				}catch{
					Debug.LogAssertion("Player_Controls/Move_Vec3 assertion error.");
					return Vector3.zero;
				}
			}
		}

		//TEMP

		void Handle_Hamster_Rotation(){
			if (hamster) {
				hamster.transform.rotation = Quaternion.Euler (Vector3.zero);
			}
		}


	}
}
