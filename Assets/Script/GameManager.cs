using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;




public class GameManager : MonoBehaviourPunCallbacks
{
    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;

    public static GameManager Instance;

    void Start()
    {
        Instance = this;
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Room for 2");
            return;
        }
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            if (PlayerManager.LocalPlayerInstance == null)
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
        }

    }

    #region Photon Callbacks

    /// <summary>
    /// 當本地播放器離開房間時調用。 我們需要加載啟動器場景。
    /// </summary>
    //public override void OnLeftRoom()
    // {
    //     SceneManager.LoadScene(0);
    //}
    #endregion

    #region Public Methods
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    #endregion


    #region Photon Callbacks

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // 如果您正在連接播放器，則看不到


    }
    public override void OnPlayerLeftRoom(Player other)
    {

    }
    #endregion
}
