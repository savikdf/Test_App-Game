using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fade_In_Out : MonoBehaviour {
	bool is_Fading;
	Image fade_Image;

	public IEnumerator Fade_With_Color(Color fade_Color, bool is_Fade_In){
		Set_Image_Color (fade_Color, is_Fade_In);
		Debug.Log ("anal");
		while (is_Fading) {

			yield return null;
		}
		is_Fading = false;
		Debug.Log ("Fade finished");
	}

	void Set_Image_Color(Color color_To_Set, bool is_Transparent){
		Color temp = new Color ();
		temp.r = color_To_Set.r;
		temp.g = color_To_Set.g;
		temp.b = color_To_Set.b;
		if (is_Transparent) {
			temp.a = 0;
		} else {
			temp.a = 1;
		}

	}


}
