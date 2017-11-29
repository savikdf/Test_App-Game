using UnityEngine;
using System.Collections;
using Player_NameSpace;

using Camera_NameSpace.TriggerManager;

namespace Camera_NameSpace{
	public class Camera_Smooth_Follow : MonoBehaviour {
	//VARS
		//instances
		public static Camera_Smooth_Follow instance;
		//Objects
		Transform player_Transform;
		[HideInInspector] public Transform look_At_Object;
        //data
		public bool is_Free_Control;
		public float free_Control_Angle = 45;

        bool is_Searching_For_Player = false;
		bool follow_Player = false;
		[HideInInspector] public bool target_Player_For_Target = true;
		float damp_Speed = 5f;
		public float[] camera_OffSets_Array = new float[3];	//setup like a vec3: 0,1,2 = x,y,z;
		float look_At_Speed;
		[HideInInspector] public bool is_Camera_Ready = false;

	//STARTS
		void Awake(){
			if (!instance) {
				instance = this;
			}
		}

		void Start(){
          
		}

	//SETUPS
		public void Setup_Camera(bool to_Follow){           
		    try{
			    player_Transform = Player_Manager.instance.player_Object.transform;	//get of the players
			    follow_Player = to_Follow;											//telling the cam it can follow
		    //race
			    if (Game_State_Manager.instance.game_Mode == Game_State_Manager.GameModes.Race) {
				    if (!look_At_Object) {
					    GameObject temp = new GameObject ();
					    look_At_Object = temp.transform;
					    look_At_Object.name = "Look_At_Transform";
					    look_At_Object.SetParent (gameObject.transform);
				    } else {
					    look_At_Object.SetParent (gameObject.transform);
				    }
				    if (Camera_Target_Change_Manager.instance) {
					    Camera_Target_Change_Manager.instance.Setup_Triggers ();
				    }
				    look_At_Speed = .3f;
		    //tag & menu
			    }else if (Game_State_Manager.instance.game_Mode == Game_State_Manager.GameModes.Tag || Game_State_Manager.instance.game_Mode == Game_State_Manager.GameModes.Menu) {
				    if (!look_At_Object) {
					    GameObject temp = new GameObject ();
					    look_At_Object = temp.transform;
					    look_At_Object.name = "Look_At_Transform";
					    look_At_Object.transform.position = Vector3.zero;
				    } else {
					    look_At_Object.transform.position = Vector3.zero;
				    }
				    look_At_Speed = 10f;
			    }
				is_Camera_Ready = true;
			    StartCoroutine (Smooth_Follow_Player ());	//starting the camera following
		    }catch{
			    Debug.LogAssertion ("Assert Error: Camera_Smooth_Follow/Setup_Camera has some errors.");
				is_Camera_Ready = false;
		    }           
		}

        //SMOOTH FOLLOW
        //Searches for the player and stops all functions until found.
        IEnumerator Searching_For_Player() {
			Debug.Log ("SEARCHING");
            while (is_Searching_For_Player) {				
                if(Player_Manager.instance != null) {
                    player_Transform = Player_Manager.instance.player_Object.transform;
                    is_Searching_For_Player = false;
                    Setup_Camera(true);
                }
                yield return null;
            }
        }

		///This will enable the camera to follow the player if the player_Object is set in the Player_Manager and the follow_Player bool is true
		IEnumerator Smooth_Follow_Player(){              
			while (!is_Searching_For_Player && follow_Player && player_Transform) {                
                try{                 
					//free look input ---------

					free_Control_Angle += ((-Input.GetAxis("Horizontal_Look") / .6f) * Mathf.Deg2Rad);
					camera_OffSets_Array[2] += (-Input.GetAxis("Vertical_Look") / 5f);


					//-------------------------
				    gameObject.transform.position = Camera_New_Posion;	//follow the player object
				    Handle_Look_Rotation();								//look at the target                    
				}catch{
					Debug.LogError("Smooth_Follow_Player() routine has some errors!! Stopping coroutine.");
					StopCoroutine (Smooth_Follow_Player ());
				}
				yield return null;
			}
		}
	//CAMERA LOOK ROTATION
		void Handle_Look_Rotation(){
			try{
				if(Game_State_Manager.instance.game_Mode != Game_State_Manager.GameModes.Race){
					transform.LookAt(Look_At_Transform);
				}else{
					Quaternion targetRot = Quaternion.LookRotation(look_At_Object.position - transform.position);
					transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * look_At_Speed);
				}
			}catch{
				Debug.LogError ("Error with the Handle_Look_Rotation function");
			}
		}

	//VECS
		///The vec3 the Smooth_Follow_Player routine gets
		Vector3 Camera_New_Posion{			
			get{ 
				try{
					return Vector3.Lerp (gameObject.transform.position, Camera_Ideal_Position, Time.deltaTime * damp_Speed);
				}catch{
					Debug.LogError ("Camera_New_Posion ERROR!");
					return Vector3.zero;
				}
			}		
		}
		//The Ideal position of where the camera should be
		Vector3 Camera_Ideal_Position{
			get{
				try{
					if(is_Free_Control){
						//mainly dev thing. possibly feature in the product.
						Transform temp = Player_Manager.instance.player_Object.transform;
						Vector3 temp_Return_Vec3 = new Vector3(temp.position.x + (camera_OffSets_Array[2] * Mathf.Cos(free_Control_Angle)),
							temp.transform.position.y + camera_OffSets_Array[1],
							temp.position.z + (camera_OffSets_Array[2] * Mathf.Sin(free_Control_Angle)));

						return temp_Return_Vec3;

					}else{
						if (Game_State_Manager.instance.game_Mode == Game_State_Manager.GameModes.Race) {
							return new Vector3 (player_Transform.position.x + camera_OffSets_Array [0],
												player_Transform.position.y + camera_OffSets_Array [1],
												player_Transform.position.z + camera_OffSets_Array [2]);
						}else if (Game_State_Manager.instance.game_Mode == Game_State_Manager.GameModes.Tag || Game_State_Manager.instance.game_Mode == Game_State_Manager.GameModes.Menu) {	
							int[] temp_Quadrants = Camera_Ideal_Position_Quadrant;
							Vector3 temp_Return_Vec3 = new Vector3 (Player_Manager.instance.player_Object.transform.position.x + (camera_OffSets_Array[1] * (Mathf.Sin(45* temp_Quadrants[0]))), 
								Player_Manager.instance.player_Object.transform.position.y - camera_OffSets_Array[2], 	
								Player_Manager.instance.player_Object.transform.position.z + (camera_OffSets_Array[1] * (Mathf.Sin(45 * temp_Quadrants[1]))));

							return temp_Return_Vec3;
						}else{
							Debug.LogError ("Camera Ideal Position error. Defaulting to 0,0,0.");
							return Vector3.zero;
						}
					}
				}catch{
					Debug.LogError ("Camera_Ideal_Position ERROR!");
					return Vector3.zero;
				}
			}
		}

		int[] Camera_Ideal_Position_Quadrant{
			get{ 
				try{
					int[] ideal_Pos_Quadrants = new int[2]{-1,-1};
					if(Player_Manager.instance.player_Object.transform.position.x > 0f){
						ideal_Pos_Quadrants[0] = 1;
					}
					if(Player_Manager.instance.player_Object.transform.position.z > 0f){
						ideal_Pos_Quadrants[1] = 1;
					}
					return ideal_Pos_Quadrants;
				}catch{
					Debug.LogError("Camera_Ideal_Position_Quadrant ERROR!");
					int[] catchReturn = new int[2]{1,1};
					return catchReturn;
				}
			}
		}
	//TRANSFORMS
		//the transform the camera will look at
		Transform Look_At_Transform{
			get{
				try{
					if(is_Free_Control){
						if(target_Player_For_Target){
							return Player_Manager.instance.player_Object.transform;
						}
					}else{
						if (Game_State_Manager.instance.game_Mode == Game_State_Manager.GameModes.Tag || Game_State_Manager.instance.game_Mode == Game_State_Manager.GameModes.Menu) {
							
						} else if (Game_State_Manager.instance.game_Mode == Game_State_Manager.GameModes.Race) {
							if(target_Player_For_Target){
								look_At_Object.position = player_Transform.position + (Vector3.up * 5f);
							}
						} else {
							look_At_Object.position = player_Transform.position + (Vector3.up * 5f);
						}
					}
					return look_At_Object;
				}catch{
					Debug.LogError ("Look_At_Transform ERROR!");
					return null;
				}
			}
			
		}


	}
}
