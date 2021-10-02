using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectSpawner objectSpawner;
    public PlateauController plateauController;
    public PlayerController playerController;
    bool isGameOver = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

            // Explode
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
        }
        
        // TODO: Affiher menus retry

    }
}
