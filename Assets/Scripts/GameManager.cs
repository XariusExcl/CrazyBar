using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectSpawner objectSpawner;
    public PlateauController plateauController;
    public PlayerController playerController;
    bool isGameOver = false;
    
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        if (!isGameOver)
        {            
            objectSpawner.StopSpawning();
            playerController.GameOver();
            plateauController.Fall();

            // Explode objects on plateau
            GameObject[] objects;
            objects = GameObject.FindGameObjectsWithTag("Object");
            
            foreach(GameObject obj in objects)
            {
                Destroy(obj.GetComponent<FixedJoint2D>());

                obj.GetComponent<Rigidbody2D>().AddExplosionForce(
                    3f,
                    plateauController.transform.position+Vector3.down*2f,
                    10f
                );
            }

            isGameOver = true;
        }
        
        // TODO: Affiher menus retry

    }
}
