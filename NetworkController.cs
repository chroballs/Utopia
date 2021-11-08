using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public GameObject player = null;
    public Transform playerSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void OnConnected()
    {
        Debug.Log("Conectou!");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectou no Master!");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        //PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("RoomNew");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entrou na sala!");
        Debug.Log("Nome da Sala: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Players na Sala: " + PhotonNetwork.CurrentRoom.PlayerCount);
        Vector3 playerSpawnPoint = new Vector3(Random.Range(0,10), Random.Range(30, 10), Random.Range(0, 10));

        PhotonNetwork.Instantiate(player.name, playerSpawnPoint, Quaternion.identity);
        PhotonNetwork.LoadLevel("GAME");
 
        
        
    }




}
