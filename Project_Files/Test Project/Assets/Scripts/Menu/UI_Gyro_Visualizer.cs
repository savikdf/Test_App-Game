using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using GlobalData_NameSpace;

public class UI_Gyro_Visualizer : MonoBehaviour {
	RectTransform gyro_Dot;

	void Awake(){
		gyro_Dot = transform.GetChild (0).gameObject.GetComponent<RectTransform> ();
	}

	void Start(){
		StartCoroutine (Visualize_Gyro_Input ());
	}

	IEnumerator Visualize_Gyro_Input(){
		while (gyro_Dot) {
			try{
				gyro_Dot.anchoredPosition = new Vector2 (Global_Data_Holder.gyro_X_Input * 50f, Global_Data_Holder.gyro_Y_Input * 50f);
			}catch{
				//shits fucked
			}
			yield return null;
		}
	}

}
