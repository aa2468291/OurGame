using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class PlayerAnim : MonoBehaviourPun
{
    public Animator Player_ani;
    public bool isAction;
    public PlayerManager playerManager;
    public PlayerMove playerMove;
    public GameObject Sword;
    public bool Attacking;
    public float Sp;

    void Start()
    {
        Attacking = false;
        isAction = false;
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


