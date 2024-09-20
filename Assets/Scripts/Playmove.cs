using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum PlayState
{
    Moving,
    Idle,
    Jump
}
public class Playmove : MonoBehaviour
{
    public float speed = 3;
    public float runSpeed = 5;
    public float jumpSpeed = 2;//��Ծ�ٶ�
    public float gravity = 2;//ģ������
    public float RotateSpeed = 1;
    public PlayState playState = PlayState.Idle;
    public bool isMoving = false;
    public bool isJump = false;//�Ƿ�����Ծ
    public float jumpTime = 0.5f;//��Ծʱ��
    public float jumpTimeFlag = 0;//�ۼ���Ծʱ�������ж��Ƿ������Ծ
    //define CharacterController��name PlayerController
    //instantiate object
    public CharacterController PlayerController;
    public Animator anim;

    Vector3 Player_Move;

    void Start()
    {
        PlayerController = this.GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        //Determine whether PlayerController is on the ground
        if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            playState = PlayState.Jump;
            PlayerController.Move(transform.forward * speed * Time.deltaTime);
        }


        else if (Input.GetKey(KeyCode.W))
        {
            isMoving = true;
            playState = PlayState.Moving;
            PlayerController.Move(transform.forward * speed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            isMoving = true;
            playState = PlayState.Moving;
            PlayerController.Move(transform.forward * speed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            playState = PlayState.Jump;
            PlayerController.Move(transform.forward * speed * Time.deltaTime);
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            playState = PlayState.Jump;
        }
        else
        {
            playState = PlayState.Idle;
            isMoving = false;
        }
        if (isJump)
        {
            if (jumpTimeFlag < jumpTime)
            {
                PlayerController.Move(transform.up * jumpSpeed * Time.deltaTime);
                jumpTimeFlag += Time.deltaTime;
            }
            else if (jumpTime < jumpTimeFlag)
            {
                PlayerController.Move(transform.up * -gravity * Time.deltaTime);
            }
            if (PlayerController.collisionFlags == CollisionFlags.Below)
            {
                jumpTimeFlag = 0;
                isJump = false;
            }
        }
        else
        {
            if (PlayerController.collisionFlags != CollisionFlags.Below)
                PlayerController.Move(transform.up * -gravity * Time.deltaTime);
        }
        var horizontal = Input.GetAxis("Horizontal");
        //var vertical = Input.GetAxis("vertical");

        transform.Rotate(Vector3.up, horizontal * RotateSpeed);
        //transform.Rotate(Vector3.up, vertical * RotateSpeed);
        UpdateAnim();
    }
    void UpdateAnim()
    {
        anim.SetFloat("Speed", Player_Move.magnitude);
        anim.SetTrigger("down0");
        anim.SetTrigger("sam0126");
    }
}

