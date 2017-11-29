using UnityEngine;
using System.Collections;

namespace Server_NameSpace{
	public class Server_Manager : MonoBehaviour {
    //VARS
        //instances
		public static Server_Manager instance;

        //data
		const string type_Name = "Ham_Smashers";			//unique game name
		const string game_Name = "Ham_Server_Test_Room";		//room name

		const int max_Players_For_Room = 2;
		const int port_Number = 2000;

        //server data
        public enum Server_Connection_Types {
            Host,
            Client
        };
        public Server_Connection_Types connection_Type;

        //join data
        private HostData[] host_List;


    //STARTS
        void Awake()
        {
			if (!instance)
				instance = this;
		}

		///sets up the server connection
		public void Setup_Server()
        {
			Network.InitializeServer (max_Players_For_Room, port_Number, !Network.HavePublicAddress ());
			MasterServer.RegisterHost (type_Name, game_Name);
			Start_Server ();
		}


	//SERVER STARTS
		///starts the server
		public void Start_Server()
        {
			Start_Server ();
		}

		//calls when the server is intialized/ started 
		void OnServerInitialized()
        {
			Debug.Log ("Server was initialized");
		}

    //JOINING SERVER
        ///joins a pre existing lobby
        public void Join_Existing_Lobby(HostData host_Data)
        {
            Network.Connect(host_Data);
        }

        ///calls when a server is joined
        void On_Connected_To_Server()
        {
            Debug.Log("Server Joined");
        }

        ///refreshing the possible lobbies to join
        public void Refresh_Host_List()
        {
            MasterServer.RequestHostList(type_Name);
        }

        ///sets the list of possible hosts
        void OnMasterServerEvent(MasterServerEvent ms_Event)
        {
            if (ms_Event == MasterServerEvent.HostListReceived)
            {
                host_List = MasterServer.PollHostList();
            }
        }

        ///Joins the User to the host server 
        public void On_Button_Join_Lobby_Hit(int index_Of_Host)
        {
            if(host_List != null && index_Of_Host < host_List.Length - 1)
            {
                Join_Existing_Lobby(host_List[index_Of_Host]);
            }
        }

    //HOSTING SERVER
        public void Host_New_Lobby()
        {



        }

    }
}