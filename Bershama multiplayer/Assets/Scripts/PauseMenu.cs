using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PauseMenu : MonoBehaviourPunCallbacks
{
    public static bool paused = false;
    bool disconnected = false;

    public void TogglePause()
    {
        if(disconnected)
            return;

        paused = !paused;

        transform.GetChild(0).gameObject.SetActive(paused);
        Cursor.lockState = (paused) ? CursorLockMode.None : CursorLockMode.Confined;
        Cursor.visible = paused;
    }

    void Update()
    {
         if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(true);
            PlayerController.Instance.enabled = false;
        }
        else if(Input.GetKeyUp(KeyCode.Escape)) 
        {
            gameObject.SetActive(false);
            PlayerController.Instance.enabled = true;
        }
    }

    public void LeaveRoom()
    {
        disconnected = true;
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }

}
