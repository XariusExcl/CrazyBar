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
    public AudioManager audioManager;
    bool isGameOver = false;
    float sceneLoadedTime;
    float gameOverTime;
    
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
        sceneLoadedTime = Time.realtimeSinceStartup;

        objectSpawner = GameObject.FindGameObjectWithTag("ObjectSpawner").GetComponent<ObjectSpawner>();
        plateauController = GameObject.FindGameObjectWithTag("Plateau").GetComponent<PlateauController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        uIController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
        audioManager = GameObject.FindGameObjectWithTag("Jukebox").GetComponent<AudioManager>();
        isGameOver = false;

        if (retryCount == 0)
        {
            uIController.ShowTutorial();
        }
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

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    float pitch;
    void Update()
    {
        if (!isGameOver)
        {
            pitch = (Time.realtimeSinceStartup-sceneLoadedTime)/100f+0.8f;
            audioManager.SetMusicPitch(Mathf.Max(1f,pitch));
        } else {
            audioManager.SetMusicPitch(Mathf.Max(1f,pitch - (Time.realtimeSinceStartup-gameOverTime/10f)));
        }
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
            gameOverTime = Time.realtimeSinceStartup;
            uIController.ShowPopup();
        }
    }
}
