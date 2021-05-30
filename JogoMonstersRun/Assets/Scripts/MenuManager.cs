using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public GameObject canvasPanelSearch;
    public GameObject canvasPanelWaiting;
    public GameObject canvasPanelCountdown;
    public GameObject canvasPanelLoading;

    public GameObject spawnObject;
    public Transform[] spawnPosition;

    int spawnPositionUsed;

    private void Start()
    {
        SetActivePanel(canvasPanelSearch.name);

        PhotonNetwork.SendRate = 25;
        PhotonNetwork.SerializationRate = 15;
    }

    void SetActivePanel(string activePanel)
    {
        canvasPanelSearch.SetActive(activePanel.Equals(canvasPanelSearch.name));
        canvasPanelWaiting.SetActive(activePanel.Equals(canvasPanelWaiting.name));
        canvasPanelCountdown.SetActive(activePanel.Equals(canvasPanelCountdown.name));
        canvasPanelLoading.SetActive(activePanel.Equals(canvasPanelLoading.name));

    }

    /*Buttons*/

    public void ButtonSearch()
    {
        Debug.Log("ButtonSearch");
        SetActivePanel(canvasPanelLoading.name);
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ButtonCancel()
    {
        Debug.Log("ButtonCancel");
        SetActivePanel(canvasPanelLoading.name);
        PhotonNetwork.Disconnect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;

        string roomName = "Room" + Random.Range(100, 10000);

        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        SetActivePanel(canvasPanelWaiting.name);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            spawnPositionUsed = 0;
        }
        else
        {
            spawnPositionUsed = 1;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected - " + cause.ToString());
        SetActivePanel(canvasPanelSearch.name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");

        Hashtable props = new Hashtable
        {
            {CountdownTimer.CountdownStartTime, (float) PhotonNetwork.Time}
        };

        PhotonNetwork.CurrentRoom.SetCustomProperties(props);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        Debug.Log("OnRoomPropertiesUpdate");
        SetActivePanel(canvasPanelCountdown.name);

    }

    void ContdownAction()
    {
        Debug.Log("ContdownAction");
        SetActivePanel("");
        PhotonNetwork.Instantiate(spawnObject.name, spawnPosition[spawnPositionUsed].position, spawnPosition[spawnPositionUsed].rotation);

    }

    public override void OnEnable()
    {
        base.OnEnable();
        CountdownTimer.OnCountdownTimerHasExpired += ContdownAction;

    }
    public override void OnDisable()
    {
        base.OnDisable();
        CountdownTimer.OnCountdownTimerHasExpired -= ContdownAction;

    }
}
