using System.Collections.Generic;
using System.Data;
using JetBrains.Annotations;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public List<Sprite> icons = new List<Sprite>();
    private int column = 9;
    private int row = 12;
    private int score = 0;
    private bool deadlock = false;
    private int blockCount = 0;
    private int maxBlockCount = 0;
    private int chainCount = 0;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        maxBlockCount = column * row;
        FirstSpawner(column, row);
    }

    void Update()
    {
        Debug.Log(blockCount);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            deadlock = true;
        }
        if (blockCount >= maxBlockCount)
        {
            foreach (GameObject _block in GameObject.FindGameObjectsWithTag("Block"))
            {
                //if (_block.GetComponent<Block>().chain.Count >= 2) {chainCount++;}
                if (_block.GetComponent<Block>().chain.Count >= 2) {Debug.Log(_block.GetComponent<Block>().chain.Count); deadlock = false; break;}
                deadlock = true;
            }
            if (deadlock) { Shuffle(); deadlock = false; }
        }
    }

    private void FirstSpawner(int x, int y)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Block block = Instantiate(Resources.Load("Prefabs/Blocks/Block01"), new Vector3(i, y - j - 1, 1), Quaternion.identity).GetComponent<Block>();
                IncreaseBlockCount();
                block.SetType(Random.Range(0, 6));
            }
            Instantiate(Resources.Load("Prefabs/Trigger"), new Vector3(i, y - 1, 1), Quaternion.identity);
        }
    }
    public void SingleSpawner(Transform _transform)
    {
        chainCount = 0;
        Block block = Instantiate(Resources.Load("Prefabs/Blocks/Block01"), _transform.position, Quaternion.identity).GetComponent<Block>();
        IncreaseBlockCount();
        block.SetType(Random.Range(0, 6));
    }

    public void Shuffle()
    {
        Debug.Log("Shuffled!");
        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
        {
            Destroy(block);
        }

        int a = Random.RandomRange(0, column - 3);
        int b = Random.RandomRange(0, 6);
        for (int j = 0; j < column; j++)
        {
            Block block = Instantiate(Resources.Load("Prefabs/Blocks/Block01"), new Vector3(j, row - 1.8f, 1), Quaternion.identity).GetComponent<Block>();
            IncreaseBlockCount();
            if (j >= a && j < a + 3) { block.SetType(b);}
            else block.SetType(Random.Range(0, 6));
        }

    }

    public void updateScore(int _blockCount) { score += (int)Mathf.Pow(_blockCount, 2f) / 2; scoreText.text = score.ToString(); }

    public void IncreaseBlockCount() { blockCount++; }
    public void DecreaseBlockCount() { blockCount--; }

}
