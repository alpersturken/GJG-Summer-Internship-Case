using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    //float[] rows = new float[18] { 13.67f, 12.67f, 11.67f, 10.67f, 9.67f, 8.67f, 7.67f, 6.67f, 5.67f, 4.67f, 3.67f, 2.67f, 1.67f, 0.67f, -0.33f, -1.33f, -2.33f, -3.33f };
    //public float[] rows = new float[18] {17, 16, 15f, 14.33f, 0.67f, 1.67f, 2.67f, 3.67f, 4.67f, 5.67f, 6.67f, 7.67f, 8.67f, 9.67f, 10.67f, 11.67f, 12.67f, 13.67f};
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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Gatcha!");
        if (other.gameObject.tag == "Block" && transform.position.x == other.gameObject.transform.position.x && transform.position.y > other.gameObject.transform.position.y)
        {
            belowBlock = true; transform.position = new Vector3(transform.position.x, (float)Math.Round(transform.position.y), 1);
        }
        if (other.gameObject.tag == "Border") { belowBorder = true; transform.position = new Vector3(transform.position.x, 0, 1);}
    }
    //void OnTriggerEnter2D(Collider2D other) { if (other.gameObject.tag == "Block" && transform.position.x == other.gameObject.transform.position.x && transform.position.y > other.gameObject.transform.position.y) { Debug.Log("Block entered!"); if (belowBlock == false) { transform.position = new Vector3(transform.position.x, row, 1); } belowBlock = true; } if (other.gameObject.tag == "Border") { belowBorder = true; } }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Block" && transform.position.x == other.gameObject.transform.position.x && transform.position.y > other.gameObject.transform.position.y)
        {
           belowBlock = false;
        }
        if (other.gameObject.tag == "Border") { belowBorder = false; }
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
    }





}


