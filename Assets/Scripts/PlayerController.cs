using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    HingeJoint2D hand;
    new Rigidbody2D rigidbody2D;
    float inputHorizontal = 0f;
    // float inputVertical = 0f;
    bool freezeMovement = false;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        hand = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (freezeMovement) 
        {
            inputHorizontal = 0f;
            // inputVertical = 0f;
        } else {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            // inputVertical = Input.GetAxisRaw("Vertical");
            
            rigidbody2D.MovePosition(
                new Vector2(
                    rigidbody2D.position.x + inputHorizontal*Time.deltaTime*10f,
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
