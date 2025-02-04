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
    private SpriteRenderer icon;
    void Awake()
    {
        icon = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (belowBorder == false && belowBlock == false)
        {
            transform.position -= new Vector3(0, 5f * Time.deltaTime, 0);
        }
        if(chain.Count < 4){ icon.sprite = gameManager.icons[type*4];}
        else if(chain.Count < 6){ icon.sprite = gameManager.icons[type*4 + 1];}
        else if(chain.Count < 8){ icon.sprite = gameManager.icons[type*4 + 2];}
        else{ icon.sprite = gameManager.icons[type*4 + 3];}
    }


    public void SetType(int _tpye)
    {
        type = _tpye;
        icon.sprite = gameManager.icons[type*4 + 3];
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

    void OnDestroy() {
        gameManager.DecreaseBlockCount();
    }


}


