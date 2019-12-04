using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Photon.Pun;



    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {


        #region IPunObservable implementation
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                //We own this player: send the others our data
                stream.SendNext(IsFiring);
                stream.SendNext(PlayerHp);
                stream.SendNext(playhp.localScale.x);
                stream.SendNext(playhp.localScale.y);
                stream.SendNext(playhp.localScale.z);
        }
            else
            {
                //Network player, receive data
                this.IsFiring = (bool)stream.ReceiveNext();
                this.PlayerHp = (float)stream.ReceiveNext();
                playhp.localScale = new Vector3((float)stream.ReceiveNext(), (float)stream.ReceiveNext(), (float)stream.ReceiveNext());
        }
        }

        #endregion

        #region Private Fields
        public HpUI hpui;
        public SpUI spui;
        public float PlayerHp = 500;
        public float PlayerSp = 500;
        public bool IsDead = false;
        public PlayerAnim playerAnim;
        public bool IsFiring;
        public GameObject sword;
        public GameObject PlayerMainCamera;
    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    public GameObject PlayerUiPrefab;
    public RectTransform playhp;
    public GameObject PlayerUI;

    #endregion
    public void Start()
    {
        PlayerUI.SetActive(false);
        PlayerMainCamera.SetActive(false);
        if (photonView.IsMine)
        {
            PlayerMainCamera.SetActive(true);
            PlayerUI.SetActive(true);
        }

            if (PlayerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(PlayerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            playhp = GameObject.Find("PlayerUI" + photonView.Owner.NickName + photonView.ViewID).GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }

    }

        public void CoHp(float damage)
        {
            PlayerHp -= damage;
            hpui.cohp(damage);
        }
        public void CoSp(float damage)
        {
            PlayerSp -= damage;
            spui.CoSp(damage);
            if (PlayerSp < 0)
            {
                PlayerSp = 0;
            }
        }

    }

