using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class PlayerAnim : MonoBehaviourPunCallbacks,IPunObservable
{
    public Animator Player_ani;
    public bool isAction;
    public PlayerManager playerManager;
    public PlayerMove playerMove;
    public BoxCollider sword;
    public GameObject Sword,SwordTeam;
    public bool Attacking;
    public float Sp;
    PlayerUI playerUI;

    void Start()
    {
        Attacking = false;
        isAction = false;
        playerUI = GameObject.Find("PlayerUI" + photonView.Owner.NickName + photonView.ViewID).GetComponent<PlayerUI>();
    }


    void Update()
    {


        if (photonView.IsMine)
        {
            if (!playerManager.IsDead)
            {
                if (Input.GetMouseButton(0) && Attacking == false && isAction == false)
                {
                    Sp = playerManager.PlayerSp;
                    Debug.Log(Sp);
                    if (Sp >= 50)
                    {
                        Attacking = true;
                        isAction = true;
                        Player_ani.SetBool("IsAttack", true);
                    }
                }
                if (Input.GetKeyDown(KeyCode.H))
                {
                    playerManager.CoHp(10);
                }
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    Player_ani.SetBool("IsWalking", true);
                }
                else
                {
                    Player_ani.SetBool("IsWalking", false);
                }
            }
        }
    }
    #region Trigger判定
    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (gameObject.tag == "RedPlayer")
        {
            if (other.CompareTag("BlueSword"))
            {

                playerManager.CoHp(10);
            }

        }
        else if (gameObject.tag == "BluePlayer")
        {
            if (other.CompareTag("RedSword"))
            {
                playerManager.CoHp(10);
            }

        }
        if (other.CompareTag("RedTeam"))
        {
            playerUI.playerNameText.color = new Color(255, 0, 0);
            SwordTeam.tag = "RedSword";
            gameObject.tag = "RedPlayer";

        }
        if (other.CompareTag("BlueTeam"))
        {
            playerUI.playerNameText.color = new Color(0, 0, 255);
            SwordTeam.tag = "BlueSword";
            gameObject.tag = "BluePlayer";

        }
    }
    #endregion

    #region IPunObservable implementation  Photon傳值
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.tag);
            stream.SendNext(SwordTeam.tag);
            stream.SendNext(playerUI.playerNameText.color.r);
            stream.SendNext(playerUI.playerNameText.color.g);
            stream.SendNext(playerUI.playerNameText.color.b);
        }
        else
        {
            this.tag = (string)stream.ReceiveNext();
            SwordTeam.tag = (string)stream.ReceiveNext();
            playerUI.playerNameText.color = new Color((float)stream.ReceiveNext(), (float)stream.ReceiveNext(), (float)stream.ReceiveNext());
        }
    }
#endregion
    public void AttackFalse()
    {

        Player_ani.SetBool("IsAttack", false);
    }


    public void AttackCoSp()
    {
        playerManager.CoSp(40);
    }



    public void AllEnd()
    {
        Attacking = false;
        Player_ani.SetBool("IsAttack", false);
        isAction = false;
    }

}


