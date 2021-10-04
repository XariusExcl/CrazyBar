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

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        SetNewTimeBeforeServe();
        collider.enabled = false; // WARN : vérifier si activer le collider pdt que le joueur est dedans capte la collision côté joueur
    }

    // Update is called once per frame
    void Update()
    {
        if (!canServe && Time.realtimeSinceStartup - currentTimeOffset > timeBeforeServe)
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
        numberOfItems += amount;

        // Show sprites on table
        Sprite stuffToRender = stuffArray[0];
        if (numberOfItems > 1)
        {
            stuffToRender = stuffArray[1];
        } else if (numberOfItems > 10) {
            stuffToRender = stuffArray[2];
        }  else if (numberOfItems > 20) {
            stuffToRender = stuffArray[3];
        }else if (numberOfItems > 30) {
            stuffToRender = stuffArray[4];
        }
        // Désolé je sais pas comment rendre ça plus joli et j'ai pas le temps d'y réfléchir
        stuffRenderer.sprite = stuffToRender;
        
        SetNewTimeBeforeServe();
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
