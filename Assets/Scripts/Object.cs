using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    GameManager gameManager;
    public float dampingRatio;
    new Rigidbody2D rigidbody2D;
    bool isOnPlateau = false;
    
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!isOnPlateau)
        {
            if (col.transform.tag == "Plateau" || col.transform.tag == "Object")
            {
                ContactPoint2D contact = col.GetContact(0);
                Transform oTransform = contact.collider.transform;
                rigidbody2D.SetRotation(oTransform.rotation.eulerAngles.z);

                FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>(); 
                joint.anchor = (Vector2)transform.position - contact.point;
                joint.connectedBody = oTransform.GetComponentInParent<Rigidbody2D>();
                joint.enableCollision = false;
                joint.dampingRatio = dampingRatio;

                // transform.parent = oTransform;

                isOnPlateau = true;
            } else {
                Destroy(this.gameObject);
                gameManager.GameOver();
            }
        }
    }
}   
