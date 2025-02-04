using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
        FirstSpawner(9, 12);

        //Spawner();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FirstSpawner(int x, int y)
    {

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Block block = Instantiate(Resources.Load("Prefabs/Blocks/Block01"), new Vector3(i, y - j - 1, 1), Quaternion.identity).GetComponent<Block>();
                block.SetRow(j);
                block.SetType(Random.Range(0, 6));
            }
            Instantiate(Resources.Load("Prefabs/Trigger"), new Vector3(i, y - 1, 1), Quaternion.identity);
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
    public void SingleSpawner(Transform _transform)
    {
        Block block = Instantiate(Resources.Load("Prefabs/Blocks/Block01"), _transform.position, Quaternion.identity).GetComponent<Block>();
        block.SetRow((int)_transform.position.y);
        block.SetType(Random.Range(0, 6));
    }

}
