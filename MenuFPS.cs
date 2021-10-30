
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class MenuFPS : MonoBehaviour
{
    public Button buttonConnect;
    public Text status;

    public void buttonConnectClicked()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            StartCoroutine(Connecting(3));
        }
    }


    IEnumerator Connecting(int time)
    {
        yield return new WaitForSeconds(time);
        status.text = "Connecting...";
    }
}

