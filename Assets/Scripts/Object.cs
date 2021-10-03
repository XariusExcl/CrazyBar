using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    GameManager gameManager;
    public float dampingRatio;
    new Rigidbody2D rigidbody2D;
    
    ScoreManager scoreManager;
    public bool isOnPlateau = false;
    
    void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        string colliderTag = col.transform.tag;

        if (!isOnPlateau)
        {
            if (colliderTag == "Plateau" || isProp(col))
            {
                ContactPoint2D contact = col.GetContact(0);
                float contactAngle = Vector2.Angle(contact.normal, Vector2.right);

                // If angle good enough, stick to plateau/object
                if (contactAngle > 45f && contactAngle < 135f)
                {
                    Transform oTransform = contact.collider.transform;
                    rigidbody2D.SetRotation(oTransform.rotation.eulerAngles.z);

                    FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>(); 
                    joint.anchor = (Vector2)transform.position - contact.point;
                    joint.connectedBody = oTransform.GetComponentInParent<Rigidbody2D>();
                    joint.enableCollision = false;
                    joint.dampingRatio = dampingRatio;

                    // Prevent further collisions
                    isOnPlateau = true;
                    
                    Destroy(transform.GetChild(0).gameObject);

					// Combo system if collision is on props
					
                    if(isProp(col))
                    {
                        scoreManager.AddToScore(
							1f, 
							GameObject.ReferenceEquals(col.gameObject, scoreManager.GetLastProp()) 
							? true 
							: false
						);
                    } else {
                        scoreManager.AddToScore(1f, false);
                    }
					scoreManager.AddLastProp(gameObject);
                }
            } else {
                // Object fell on ground
                gameManager.GameOver();
            }
        } else if (colliderTag == "World") {
            // Object fell from plateau to ground
            gameManager.GameOver();
        }
    }

    bool isProp(Collision2D col)
    {
        return col.transform.tag == "Object" && col.transform.GetComponent<Object>().isOnPlateau;
    }
}   
