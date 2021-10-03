using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int retryCount = -1;
    public ObjectSpawner objectSpawner;
    public PlateauController plateauController;
    public PlayerController playerController;
    public UIController uIController;
    bool isGameOver = false;
    
    // called first
    void OnEnable()
    {
        // Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        retryCount++;
        // Debug.Log("OnSceneLoaded: " + scene.name);
        // Debug.Log(mode);
    }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // called third
    void Start()
    {
        Application.targetFrameRate = 60;
        objectSpawner = GameObject.FindGameObjectWithTag("ObjectSpawner").GetComponent<ObjectSpawner>();
        plateauController = GameObject.FindGameObjectWithTag("Plateau").GetComponent<PlateauController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        uIController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
        isGameOver = false;

        if (retryCount == 0)
        {
            uIController.ShowTutorial();
        }
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
            uIController.ShowPopup();
        }
    }
}
