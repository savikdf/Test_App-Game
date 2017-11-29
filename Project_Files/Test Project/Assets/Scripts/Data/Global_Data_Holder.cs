using UnityEngine;
using System.Collections;

#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
	

namespace GlobalData_NameSpace{
	public static class Global_Data_Holder {
	//INPUTS
		//accelerometer inputs
		public static float gyro_X_Input{ 
			get {
				if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer){
					return (Input.GetAxis("Horizontal") / 2f) ;
				}
				if (Application.platform == RuntimePlatform.Android) {
					return Input.acceleration.x; 
				} else {
					return 0f;
				}
			} 
		}	
		public static float gyro_Y_Input{ 
			get {			
				if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer){
					return (Input.GetAxis("Vertical") / 2f) ;
				}
				if (Application.platform == RuntimePlatform.Android) {
					return Input.acceleration.y; 
				} else {
					return 0f;
				}
			} 
		}	
		public static float gyro_Z_Input{ 
			get {
				if (Application.platform == RuntimePlatform.Android) {
					return Input.acceleration.z; 
				}else {
					return 0f;
				}
			} 
		}	
	
		public static Vector3 input_Vector{
			get{ 
				return new Vector3 (gyro_X_Input, gyro_Y_Input, gyro_Z_Input);
			}
		}



	}
}