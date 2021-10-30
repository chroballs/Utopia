using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour
{
    public string Cena;

    void Update() {

    }

    void OnTriggerEnter()
    {
        PhotonNetwork.LoadLevel("Restless");
    }
}
