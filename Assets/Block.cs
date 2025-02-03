using UnityEngine;

public class Block : MonoBehaviour
{
    float[] rows = new float[18] {13.67f, 12.67f, 11.67f, 10.67f, 9.67f, 8.67f, 7.67f, 6.67f, 5.67f, 4.67f, 3.67f, 2.67f, 1.67f, 0.67f, -0.33f, -1.33f, -2.33f, -3.33f};
    //public float[] rows = new float[18] {-3.33f, -2.33f, -1.33f, -0.33f, 0.67f, 1.67f, 2.67f, 3.67f, 4.67f, 5.67f, 6.67f, 7.67f, 8.67f, 9.67f, 10.67f, 11.67f, 12.67f, 13.67f};
    public int row = 0;
    public bool belowBlock = false;
    public bool belowBorder = false;
    void Start()
    {
        //SetRow();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (belowBorder == false)
        {
            if (rows[row] < transform.position.y) { transform.position -= new Vector3(0, 1f * Time.deltaTime, 0); }
        }
        if (transform.position.y > rows[row]) { transform.position = new Vector3(transform.position.x, rows[row], 1); }
    }

    public void SetRow(int _row)
    {
        row = _row;
        Debug.Log(rows[row] + "!");

        /*switch(transform.position.y)
        {
            case 4.67f:
                row = 9;
                break;
            case 3.67f:
                row = 8;
                break;
            case 2.67f:
                row = 7;
                break;
            case 1.67f:
                row = 6;
                break;
            case 0.67f:
                row = 5;
                break;
            case -0.33f:
                row = 4;
                break;
            case -1.33f:
                row = 3;
                break;
            case -2.33f:
                row = 2;
                break;
            case -3.33f:
                row = 1;
                break;
        }*/
    }

    void OnTriggerEnter2D(Collider2D other) { Debug.Log("Block entered!"); belowBlock = true; }
    void OnTriggerExit2D(Collider2D other) { if (other.gameObject.tag == "Block" && transform.position.x == other.gameObject.transform.position.x && transform.position.y > other.gameObject.transform.position.y) { Debug.Log("Block exit!"); belowBlock = false; if(!belowBorder){row++;}} }





}


