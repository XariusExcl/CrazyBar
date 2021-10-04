using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablesController : MonoBehaviour
{
    public float minTimeBeforeServe;
    public float maxTimeBeforeServe;
    float currentTimeOffset;
    float timeBeforeServe;
    public bool canServe;
    int numberOfItems;
    new Collider2D collider;
    public GameObject serveSprite;
    public Sprite[] stuffArray;
    public SpriteRenderer stuffRenderer; 
    public PlayerController playerController;
    public ScoreManager scoreManager;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        SetNewTimeBeforeServe();
        collider.enabled = false; // WARN : vérifier si activer le collider pdt que le joueur est dedans capte la collision côté joueur
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canServe && !gameManager.getIsGameOver() && Time.realtimeSinceStartup - currentTimeOffset > timeBeforeServe)
        {
            serveSprite.SetActive(true);
            canServe = true;
            collider.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            playerController.SetCanServe(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            playerController.SetCanServe(false);
        }
    }
    public void AddNumberOfItems(int amount)
    {
        StartCoroutine(AddNumberOfItemsDelay(amount));
    }

    IEnumerator AddNumberOfItemsDelay(int amount)
    {
        yield return new WaitForSeconds(1.7f); // Wait for objects to come to table
        if (!gameManager.getIsGameOver())
        {
            numberOfItems += amount;

            // Show sprites on table
            Mathf.Clamp(numberOfItems/4, 0, 8);

            Sprite stuffToRender = stuffArray[Mathf.Clamp((numberOfItems+3)/6, 0, stuffArray.Length-1)];
            stuffRenderer.sprite = stuffToRender;

            scoreManager.AddToScore(amount, false);
            // TODO : Play sound ?
            
            SetNewTimeBeforeServe();
        }
    }

    void SetNewTimeBeforeServe()
    {
        serveSprite.SetActive(false);
        canServe = false;
        collider.enabled = false;
        timeBeforeServe = Random.Range(minTimeBeforeServe, maxTimeBeforeServe);
        currentTimeOffset = Time.realtimeSinceStartup;
    }
}
