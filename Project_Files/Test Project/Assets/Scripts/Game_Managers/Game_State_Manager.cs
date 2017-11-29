using UnityEngine;
using System.Collections;

using Player_NameSpace;
using Camera_NameSpace;
using Spawn_NameSpace;

[RequireComponent (typeof (Spawn_Manager))]

public class Game_State_Manager : MonoBehaviour {
//VARS
	public static Game_State_Manager instance;

	//data
	public enum GameStates{
		Load,
		Start,
		Intra,
		End,
		Social
	};
	public enum GameModes{
		Menu,
		Race,
		Tag,
		Battle
	}

	public GameModes game_Mode;
	public GameStates game_State;
	//load data
	bool is_Game_Ready{	//will return true when all managers are setup correctly
		get{ 
			if(Camera_Smooth_Follow.instance.is_Camera_Ready && Spawn_Manager.instance.is_Spawn_Ready){
				return true;
			}else{
				return false;
			}
		}
	}
	float fade_In_Time = 1.6f;

//STARTS
	void Awake(){
		if (!instance) {
			instance = this;
		}
	}

	void Start(){
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToPortrait = false;
		Screen.orientation = ScreenOrientation.Portrait;

		Switch_Game_State (game_State);
		Switch_Game_Mode (game_Mode);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

//STATE SWITCHING
	///Switches the Game's state
	void Switch_Game_State(GameStates toState){
		try{
			switch (toState) {
		//Load
			case GameStates.Load:
				if(!Camera.main.GetComponent<Camera_Smooth_Follow>()){			//setting up the camera if it isn't already
					Camera.main.gameObject.AddComponent<Camera_Smooth_Follow>();
				}
				if(Player_Manager.instance != null) { 
					Camera_Smooth_Follow.instance.Setup_Camera(true);	//sets the camera to follow the player
					Player_Manager.instance.AllowPlayerToMove(true);    //temp.. will only actrivate upon the game starting. this if for testing. 
				}else{
					Debug.Log("no player detected");
				}
				Screen.orientation = ScreenOrientation.Portrait;

				break;
		//Start
			case GameStates.Start:
					

				break;
		//Intra
			case GameStates.Intra:

				break;
		//End
			case GameStates.End:

				break;
		//Social
			case GameStates.Social:

				break;
			}
			game_State = toState;
		}catch{
			Debug.LogAssertion ("ASSERT ERROR: GameState Switch Error.");
		}
	}

	///Switches the Game's mode
	void Switch_Game_Mode(GameModes toMode){
		try{
			switch (toMode) {
		//Race
			case GameModes.Race:

				break;
		//Tag
			case GameModes.Tag:

				break;
		//Battle
			case GameModes.Battle:

				break;
			}
			game_Mode = toMode;
		}catch{
			Debug.LogAssertion ("ASSERT ERROR: GameMode Switch Error.");
		}
	}

    //GAME READY CHECK
	IEnumerator Check_If_Ready_To_Start(){
		while (!is_Game_Ready) {
			//game isnt ready to start yet
			yield return null;
		}
		Switch_Game_State (GameStates.Start);
	}

	//START FADE

}
