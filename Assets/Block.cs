using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{   
    
    public List<GameObject> chain = new List<GameObject>();

    public int score = 0;
    public int type = 0;
    public bool belowBlock = true;
    public bool belowBorder = false;

    private GameManager gameManager;
    private Transform icon;
    private SpriteRenderer iconSR;
    void Awake()
    {
        icon = transform.GetChild(0);
        iconSR = icon.GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (belowBorder == false && belowBlock == false)
        {
            transform.position -= new Vector3(0, 3f * Time.deltaTime, 0);
        }
    }


    public void SetType(int i)
    {
        type = i;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case 0:
                spriteRenderer.color = Color.blue;
                break;
            case 1:
                spriteRenderer.color = Color.red;
                break;
            case 2:
                spriteRenderer.color = Color.green;
                break;
            case 3:
                spriteRenderer.color = Color.yellow;
                break;
            case 4:
                spriteRenderer.color = Color.magenta;
                break;
            case 5:
                spriteRenderer.color = Color.white;
                break;
        }
        iconSR.sprite = gameManager.icons[type];
    }

    public void AddChain(List<GameObject> newChain)
    {
        foreach (GameObject block in newChain)
        {
            if (!chain.Contains(block) && type == block.GetComponent<Block>().type) { chain.Add(block); }
            if (!chain.Contains(gameObject)) { chain.Add(gameObject); }
        }

    }

    public void ClearChains()
    {
        foreach (GameObject block in chain.ToList())
        {
            block.GetComponent<Block>().chain.Clear();
        }
    }




    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Block" && transform.position.x == other.gameObject.transform.position.x && transform.position.y > other.gameObject.transform.position.y)
        {
            belowBlock = true; transform.position = new Vector3(transform.position.x, (float)Math.Round(transform.position.y), 1);

        }
        if (other.gameObject.tag == "Border") { belowBorder = true; transform.position = new Vector3(transform.position.x, 0, 1); }
        ClearChains();
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Block" && transform.position.x == other.gameObject.transform.position.x && transform.position.y > other.gameObject.transform.position.y)
        {
            belowBlock = false;
        }
        if (other.gameObject.tag == "Border") { belowBorder = false; }
        ClearChains();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Block")
        {
            if (other.transform.position == transform.position) { Destroy(gameObject); }
            if (other.transform.position.y <= 8 && type == other.GetComponent<Block>().type)
            {
                if (belowBorder == true || belowBlock == true)
                {
                    if (transform.position.x == other.transform.position.x || transform.position.y == other.transform.position.y)
                    {
                        if (!chain.Contains(other.gameObject)) { chain.Add(other.gameObject); }
                        other.gameObject.GetComponent<Block>().AddChain(chain);
                        if (chain.Count >= 8) { iconSR.sprite = (Sprite)gameManager.icons[9]; icon.localScale = new Vector3(0.1f, 0.1f, 1);}
                        else if (chain.Count >= 6) { iconSR.sprite = (Sprite)gameManager.icons[8]; icon.localScale = new Vector3(0.1f, 0.1f, 1);}
                        else if (chain.Count >= 4) { iconSR.sprite = (Sprite)gameManager.icons[7]; icon.localScale = new Vector3(0.3f, 0.3f, 1);}
                        else { iconSR.sprite = (Sprite)gameManager.icons[type]; icon.localScale = new Vector3(0.3f, 0.3f, 1);}
                    }
                }
            }
        }
    }

    void OnMouseDown()
    {
        if (chain.Count >= 2)
        {
            gameManager.updateScore(chain.Count);
            chain.Remove(gameObject);
            foreach (GameObject block in chain.ToList())
            {
                Destroy(block);
            }
            Destroy(gameObject);
        }
        
    }



}


