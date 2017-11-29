using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using Server_NameSpace;

namespace Menu_NameSpace{
	public class Menu_Manager : MonoBehaviour {
	//VARS
		//data
		public enum MenuStates{
			S_Main,
			a_Main_Public,
			a_Main_Custom,
			b_Public_Choose_Mode,
			b_Custom_Join,
			b_Custom_Host,
			c_Connection_Menu,
			d_Pre_Game_Lobby,
			e_Pre_Game_Host_Choice,
			e_Pre_Game_Customize,
			F_Load,
			G_Settings
		};
		public MenuStates menu_State;

		public MenuStates most_Recent_Menu;
		int current_Menu_Index = 0;


		//Lists & Arrays
		public List<Canvas> UI_Canvases = new List<Canvas>();

	//STARTS
		void Awake(){
			Switch_To_Menu ((int)MenuStates.S_Main);
			most_Recent_Menu = MenuStates.S_Main;
		}

	//SWITCHES
		///Enables all but the index of the passed List element number.
		public void Switch_To_Menu(int menu_Num){
			try{
				On_Menu_State_Change((MenuStates)menu_Num);
				for (int i = 0; i < UI_Canvases.Count; i++) {
					if (i != menu_Num) {
						UI_Canvases [i].enabled = false;
					} else {
						UI_Canvases [i].enabled = true;
						current_Menu_Index = i;
						//Debug.Log ("Switched to menu: " + UI_Canvases [i].name.ToString ());
					}
				}
			}catch{
				Debug.LogError ("Error in Switch_To_Menu Function!");
			}
		}
		public void Back_Button_Hit(){
			try{
				if (menu_State == MenuStates.a_Main_Custom) {
					Switch_To_Menu ((int)MenuStates.S_Main);
				} else if (menu_State == MenuStates.b_Custom_Join || menu_State == MenuStates.b_Custom_Host) {
					Switch_To_Menu ((int)MenuStates.a_Main_Custom);
				} else if (menu_State == MenuStates.e_Pre_Game_Customize) {
					Switch_To_Menu ((int)MenuStates.d_Pre_Game_Lobby);
				} else if (menu_State == MenuStates.G_Settings) {
					Switch_To_Menu ((int)most_Recent_Menu);
				}else {
					Switch_To_Menu (current_Menu_Index - 1);
				}
			}catch{
				Debug.LogError ("There was an error in the Menu_Manager/Back_Button_Hit Function");
			}
		}

		///Switched between the menu states
		void On_Menu_State_Change(MenuStates toState){
			try{
				switch (toState) {
				case MenuStates.S_Main:
					break;
				case MenuStates.a_Main_Public:
					Server_Manager.instance.Setup_Server();
					break;
				case MenuStates.a_Main_Custom:
					Server_Manager.instance.Setup_Server();
					break;
				case MenuStates.b_Public_Choose_Mode:
					break;
				case MenuStates.b_Custom_Join:
					break;
				case MenuStates.b_Custom_Host:
					break;
				case MenuStates.c_Connection_Menu:
					break;
				case MenuStates.d_Pre_Game_Lobby:
					break;
				case MenuStates.e_Pre_Game_Host_Choice:
					break;
				case MenuStates.e_Pre_Game_Customize:
					break;				
				case MenuStates.F_Load:
					break;
				case MenuStates.G_Settings:
					break;
				default:
					Debug.LogError("Menu Defaulted to S_MAIN.");
					break;
				}
				most_Recent_Menu = menu_State;
				menu_State = toState;
			}catch{
				Debug.LogAssertion ("Assert Error: Switch_Menu_State failed to switch states.");
			}
		}

	

	}
}
