using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
    private const string typeName = "NoMoreTanks";
    private const string gameName = "Room13";
    private HostData[] hostList;

    public GameObject player;
	public GameObject DemoPlayer;
	GameObject[] SpawnPoint;

    private Timer hostRefreshTimer;
    private Timer connectionTimeoutTimer;
    private bool connected;
    private bool connecting;
    private int numConnected;
    private bool connectionsSet;
    private bool playerInitialized;

    public List<PlayerManager> allPlayers;

    private void StartServer() {
        Network.InitializeServer(6, 25555, !Network.HavePublicAddress());

        MasterServer.RegisterHost(typeName, gameName);
        Network.sendRate = 500;

    }

    private void RefreshHostList() {
        MasterServer.RequestHostList(typeName);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent) {
        if (msEvent == MasterServerEvent.HostListReceived)
            hostList = MasterServer.PollHostList();
    }



    private void JoinServer(HostData hostData) {
        Network.Connect(hostData);
    }

    void OnConnectedToServer() {
        connected = true;
        networkView.RPC("AddConnection", RPCMode.Server);
    }

    void OnServerInitialized() {
        Debug.Log("Server Initializied");
		//GameObject newPlayer = Network.Instantiate(player, SpawnPoint[Network.connections.Length%SpawnPoint.Length].transform.position, Quaternion.identity, 0) as GameObject;
    }

	// Use this for initialization
	void Start () {
        playerInitialized = false;
        connectionsSet = false;
		SpawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
		if(DataGod.currentGameState == DataGod.GameMode.NetWorkPlay)
		{
	        if (DataGod.isClient) {
	            //Client initialization here
	            connected = false;
	            hostRefreshTimer = new Timer(1);
	            connectionTimeoutTimer = new Timer(100);
	            RefreshHostList();
	        } else {
	            //Server initialization here
	            StartServer();
	        }
			Network.sendRate = 500;
		}else if(DataGod.currentGameState == DataGod.GameMode.Demo)
		{
			GameObject demoPlayer = Instantiate(DemoPlayer, new Vector3(0,10,0), Quaternion.identity) as GameObject;
            //GameObject demoPlayer2 = Instantiate(DemoPlayer, new Vector3(5, 10, 0), Quaternion.identity) as GameObject;
			Camera.main.GetComponent<CameraFollowMouse>().Player = demoPlayer;
            demoPlayer.GetComponent<PlayerManager>().teamNumber = 0;
            //demoPlayer2.GetComponent<PlayerManager>().teamNumber = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (DataGod.isClient) {
            if (!connected)
            {
                if (connecting)
                {
                    connectionTimeoutTimer.Update();
                    if (connectionTimeoutTimer.HasCompleted())
                    {
                        connecting = false;
                    }
                }
                else
                {
                    if (hostList == null)
                    {
                        LookForServer();
                    }
                    else if (hostList.Length == 0)
                    {
                        LookForServer();
                    }
                    else
                    {
                        connecting = true;
                        connectionTimeoutTimer.Reset();
                        Network.Connect(hostList[0]);
                    }
                }
            }
            else if (!playerInitialized)
            {
                int teamNumber = numConnected % SpawnPoint.Length;
                GameObject newPlayer = Network.Instantiate(player, SpawnPoint[teamNumber].transform.position, Quaternion.identity, 0) as GameObject;
                newPlayer.GetComponent<PlayerManager>().playerNumber = numConnected;
                newPlayer.GetComponent<PlayerManager>().teamNumber = teamNumber;
                Camera.main.GetComponent<CameraFollowMouse>().Player = newPlayer;
                playerInitialized = true;
            }
        }
		if(DataGod.currentGameState == DataGod.GameMode.Demo)
		{

		}

	}

    private void LookForServer() {
        hostRefreshTimer.Update();
        if (hostRefreshTimer.HasCompleted()) {
            RefreshHostList();
        }
    }

    [RPC]
    public void AddConnection()
    {
        numConnected++;
        networkView.RPC("SendConnectionCount", RPCMode.OthersBuffered, numConnected);
    }

    [RPC]
    public void SendConnectionCount(int num)
    {
        numConnected = num;
        connectionsSet = true;
    }
}
