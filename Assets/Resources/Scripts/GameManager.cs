using System.Collections.Generic;
using System.Data;
using JetBrains.Annotations;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public List<Sprite> icons = new List<Sprite>();
    private int column = 9;
    private int row = 12;
    private int score = 0;
    void Start()
    {
        Application.targetFrameRate = 60;
        FirstSpawner(column, row);
    }

    private void FirstSpawner(int x, int y)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Block block = Instantiate(Resources.Load("Prefabs/Blocks/Block01"), new Vector3(i, y - j - 1, 1), Quaternion.identity).GetComponent<Block>();
                block.SetType(Random.Range(0, 6));
            }
            Instantiate(Resources.Load("Prefabs/Trigger"), new Vector3(i, y - 1, 1), Quaternion.identity);
        }
    }
    public void SingleSpawner(Transform _transform)
    {
        Block block = Instantiate(Resources.Load("Prefabs/Blocks/Block01"), _transform.position, Quaternion.identity).GetComponent<Block>();
        block.SetType(Random.Range(0, 6));
    }

    public void Shuffle()
    {
        

        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
        {
            Destroy(block);
        }

        int a = Random.RandomRange(0, column - 3);
        int b = Random.RandomRange(0, 6);
        Debug.Log(a + " - " + b);
        for (int j = 0; j < column; j++)
        {
            Block block = Instantiate(Resources.Load("Prefabs/Blocks/Block01"), new Vector3(j, row - 2.1f, 1), Quaternion.identity).GetComponent<Block>();
            if (j >= a && j < a + 3) { block.SetType(b); Debug.Log("j is: " + j); }
            else block.SetType(Random.Range(0, 6));
        }

        





    }

    public void updateScore(int _blockCount){score += (int)Mathf.Pow(_blockCount, 2f)/2; scoreText.text = score.ToString();}
}
