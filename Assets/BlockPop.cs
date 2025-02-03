using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class BlockPop : MonoBehaviour
{
    public List<GameObject> chain = new List<GameObject>();
    private int type = 0;
    
    void Start()
    {
        type = GetComponent<Block>().type;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Block")
        {
            if(type == other.GetComponent<Block>().type && !chain.Contains(other.gameObject)){chain.Add(other.gameObject);}
            other.gameObject.GetComponent<BlockPop>().AddChain(chain);
        }
    }

    public void AddChain(List<GameObject> newChain)
    {
        foreach (GameObject block in newChain)
        {
            if(!chain.Contains(block)){chain.Add(block);}
        }
    }
}
