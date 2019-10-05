using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimWalking : MonoBehaviour {

    public float speed;

    Vector3 movement;
    public Animator anim;

	// Use this for initialization
	void Start () {

        //anim = GetComponent<Animator>();

	}

    
    void Update () {
        //float v = Input.GetAxis("Vertical");
        //float h = Input.GetAxis("Horizontal");
        //movement = new Vector3(h, 0, v);
        //speed = movement.normalized.magnitude;
        anim.SetFloat("Speed", Mathf.Clamp(speed,0.2f,2f));




     //   anim = GetComponent<Animator>();

            //   float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            //float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

            //   movement.x = Input.GetAxisRaw("Horizontal");
            //   movement.y = Input.GetAxisRaw("Vertical");





            //   if (Input.GetAxis("Vertical") * speed > 1)

            //       AnimationPlayMode.Queue = ("HumanoidWalk");
            //   else
            //       animation("HumanoidIdle");

            //   }
            ////if (Input.GetKey(KeyCode.RightArrow))
            ////{
            ////    anim.Play("IsWalking");
            ////    transform.position += Vector3.right * speed * Time.deltaTime;
            ////}

            ////if (Input.GetKey(KeyCode.LeftArrow))
            ////{
            ////    anim.Play("IsWalking");
            ////    transform.position += Vector3.left * speed * Time.deltaTime;
            ////}

            ////if (Input.GetKey(KeyCode.UpArrow))
            ////{
            ////    anim.Play("IsWalking");
            ////    transform.position += Vector3.left * speed * Time.deltaTime;
            ////}

            ////if (Input.GetKey(KeyCode.DownArrow))
            ////{
            ////    anim.Play("IsWalking");
            ////    transform.position += Vector3.left * speed * Time.deltaTime;
            ////}

            // ****ONE WAY, DIDNT WORK****
            //float translation = Input.GetAxis("Vertical") *speed;
            //float rotation = Input.GetAxis("Horizontal") * rotationspeed;
            //translation *= Time.deltaTime;
            //rotation *= Time.deltaTime;

            //transform.Translate(0, 0, translation);
            //transform.Rotate(0, rotation, 0);


        //if (speed != 0)
        //{
        //    anim.SetBool("IsWalking", true);

        //}


        //else
        //{
        //    anim.SetBool("IsWalking", false);
        //}


    }
}
