using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    HingeJoint2D hand;
    new Rigidbody2D rigidbody2D;
    float inputHorizontal = 0f;
    // float inputVertical = 0f;
    bool freezeMovement = false;
    public float playerSpeed = 10f;
    public float sprintSpeed = 20f;
    public float sprintReloadRate = 30f;
    public float sprintDuration = 100f;
    public AudioSource fail;
    float currentPlayerSpeed;
    public Image staminaBar;
    float stamina = 100f;
    bool canServe = false;

    public GameObject spriteHolder;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        hand = GetComponent<HingeJoint2D>();
        currentPlayerSpeed = playerSpeed;
    }   

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (freezeMovement) 
        {
            inputHorizontal = 0f;
        } else {

            inputHorizontal = Input.GetAxisRaw("Horizontal");
            
            // Sprint System

            staminaBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, stamina * 2f);

            if(Input.GetButton("Sprint"))
            {
                if(stamina > 0f) {
                    currentPlayerSpeed = sprintSpeed;
                    stamina = stamina - sprintDuration * Time.deltaTime;
                } else {
                    currentPlayerSpeed = playerSpeed;
                    stamina = 0f;
                }
            }
            if(!Input.GetButton("Sprint"))
            {
                currentPlayerSpeed = playerSpeed;
                if(stamina < 100f) {
                    stamina = stamina + sprintReloadRate * Time.deltaTime;
                } else {
                    stamina = 100f;
                }
            }

            // Serve Input

            if(canServe)
            {
                if (Input.GetButton("Serve"))
                {
                    // Move objects to Table
                    GameObject[] objects;
                    objects = GameObject.FindGameObjectsWithTag("Object");
                    
                    foreach(GameObject obj in objects)
                    {
                        obj.GetComponent<Object>().FlyToTable();
                    }
                }
            }

            // Movement System

            if(inputHorizontal > 0f) {
                spriteHolder.GetComponent<SpriteRenderer>().flipX = false;
            } else if(inputHorizontal < 0f) {
                spriteHolder.GetComponent<SpriteRenderer>().flipX = true;
            }

            if(inputHorizontal != 0f) {
                spriteHolder.GetComponent<Animator>().SetBool("Walking", true);
            } else {
                spriteHolder.GetComponent<Animator>().SetBool("Walking", false);
            }

            // Movement Rigidbody

            rigidbody2D.MovePosition(
                new Vector2(
                    rigidbody2D.position.x + inputHorizontal*Time.deltaTime*currentPlayerSpeed,
                    rigidbody2D.position.y
                )
            );
        }
    }

    public void GameOver()
    {
        freezeMovement = true;
        fail.Play();
        Destroy(hand);
        spriteHolder.GetComponent<Animator>().SetTrigger("Failed");
    }

    public void SetCanServe(bool value)
    {
        canServe = value;
    }
}
