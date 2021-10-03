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
    float currentPlayerSpeed;
    public Image staminaBar;
    float stamina = 100f;

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
        Destroy(hand);
        // TODO: Animations quelconques
    }
}
