using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float[] rows = new float[9] { 4.67f, 3.67f, 2.67f, 1.67f, 0.67f, -0.33f, -1.33f, -2.33f, -3.33f };
    public float[] columns = new float[9] {-5.41f, -4.41f, -3.41f, -2.41f, -1.41f, -0.41f, 0.59f, 1.59f, 2.59f};
    private List<int> column1 = new List<int>();
    GameObject[] Column2;
    GameObject[] Column3;
    GameObject[] Column4;
    GameObject[] Column5;
    GameObject[] Column6;
    GameObject[] Column7;
    GameObject[] Column8;
    GameObject[] Column9;


    void Start()
    {
        Application.targetFrameRate=60;
        FirstSpawner();
        //Spawner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FirstSpawner()
    {
        float x = 0;
        float y = 19;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 18; j++)
            {
                Block block = Instantiate(Resources.Load("Prefabs/Blocks/Block01"), new Vector3(x, y, 1), Quaternion.identity).GetComponent<Block>();
                block.SetRow(j);
                block.SetType(Random.Range(0,6));
                y -= 1;
            }
            x += 1;
            y = 19f;
        }

        /*float x1 = -5.41f;
        float y1 = 13.67f;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Block block = Instantiate(Resources.Load("Prefabs/Blocks/Block01"), new Vector3(x1, y1, 1), Quaternion.identity).GetComponent<Block>();
                block.SetRow(j);
                block.SetType(Random.Range(0,6));
                y1 -= 1;
            }
            x1 += 1;
            y1 = 13.67f;
        }*/
    }
    private void Spawner()
    {
        float x = -5.41f;
        float y = 13.67f;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Block block = Instantiate(Resources.Load("Prefabs/Blocks/Block01"), new Vector3(x, y, 1), Quaternion.identity).GetComponent<Block>();
                block.SetRow(j);
                block.SetType(Random.Range(0,6));
                y -= 1;
            }
            x += 1;
            y = 13.67f;
        }
    }
    
}
