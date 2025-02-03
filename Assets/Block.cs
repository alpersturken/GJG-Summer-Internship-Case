using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    public List<GameObject> chain = new List<GameObject>();
    public int row = 0;
    public int type = 0;
    public bool belowBlock = true;
    public bool belowBorder = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (belowBorder == false && belowBlock == false)
        {
            transform.position -= new Vector3(0, 3f * Time.deltaTime, 0);
        }
    }

    public void SetRow(int _row)
    {
        row = _row;
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
    }

    public void AddChain(List<GameObject> newChain)
    {
        Debug.Log(gameObject.transform.position);
        foreach (GameObject block in newChain)
        {
            if (!chain.Contains(block) && type == block.GetComponent<Block>().type) { chain.Add(block); }
            if (!chain.Contains(gameObject)) { chain.Add(gameObject); }
        }

    }



    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.tag == "Block" && transform.position.x == other.gameObject.transform.position.x && transform.position.y > other.gameObject.transform.position.y)
        {
            belowBlock = true; transform.position = new Vector3(transform.position.x, (float)Math.Round(transform.position.y), 1);

        }


        if (other.gameObject.tag == "Border") { belowBorder = true; transform.position = new Vector3(transform.position.x, 0, 1); }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Block" && transform.position.x == other.gameObject.transform.position.x && transform.position.y > other.gameObject.transform.position.y)
        {
            belowBlock = false;
        }
        if (other.gameObject.tag == "Border") { belowBorder = false; }
        Debug.Log("Chain cleaning!");
        chain.Clear();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(1);
        if (other.gameObject.tag == "Block" && other.transform.position.y <= 8 && type == other.GetComponent<Block>().type )
        {
            Debug.Log(2);
            if (belowBorder == true || belowBlock == true)
            {
                Debug.Log(3);
                if(!chain.Contains(other.gameObject)){chain.Add(other.gameObject);}
                Debug.Log(4);
                other.gameObject.GetComponent<Block>().AddChain(chain);
                Debug.Log(5);
                Debug.Log(transform.position + " - " + other.transform.position);
            }
        }
    }

    void OnMouseDown()
    {
        if (chain.Count >= 2)
        {
            chain.Remove(gameObject);
            foreach (GameObject block in chain.ToList())
            {
                Destroy(block);
            }
            Destroy(gameObject);
        }
    }



}


