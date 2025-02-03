using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class BlockPop : MonoBehaviour
{
    public List<GameObject> chain = new List<GameObject>();
    public int type = -1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Block")
        {
            if (type == other.GetComponent<BlockPop>().type && !chain.Contains(other.gameObject)) { chain.Add(other.gameObject); other.gameObject.GetComponent<BlockPop>().AddChain(chain); }
        }
    }

    public void AddChain(List<GameObject> newChain)
    {
        foreach (GameObject block in newChain)
        {
            if (!chain.Contains(block) && type == block.GetComponent<BlockPop>().type) { chain.Add(block);}
        }
    }

    void OnMouseDown()
    {
        if (chain.Count >= 2)
        {
            foreach (GameObject block in chain)
            {
                Destroy(block);
            }
        }
    }
}
