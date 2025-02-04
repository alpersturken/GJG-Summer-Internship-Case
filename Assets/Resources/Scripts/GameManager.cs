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
    [SerializeField] private bool deadlock = false;
    [SerializeField] private int blockCount = 0;
    [SerializeField] private int maxBlockCount = 0;
    [SerializeField] private int chainCount = 0;
    [SerializeField] private float shuffleCooldown = 0;
    public List<Sprite> icons = new List<Sprite>();
    private int column = 9;
    private int row = 12;
    private int score = 0;
    [SerializeField] private bool isAFK = false;
    private float afkTimer = 0;


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
        if (shuffleCooldown <= 3.2f) { shuffleCooldown += 0.8f * Time.deltaTime; }
        if (blockCount == maxBlockCount && shuffleCooldown >= 3)
        {
            foreach (GameObject _block in GameObject.FindGameObjectsWithTag("Block"))
            {
                if (_block.GetComponent<Block>().chain.Count >= 2) {deadlock = false; break; }
                deadlock = true;
            }
            if (deadlock) {Shuffle(); deadlock = false; shuffleCooldown = 0; }
            afkTimer += 1*Time.deltaTime;
            if(afkTimer >= 15){isAFK = true;}
        }
    }

    private void FirstSpawner(int x, int y)
    {
        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
        {
            Destroy(block);
        }
        for (int i = 0; i < x; i++)
        {

            Block block = Instantiate(Resources.Load("Prefabs/Blocks/Block01"), new Vector3(i, row, 1), Quaternion.identity).GetComponent<Block>();
            IncreaseBlockCount();
            block.SetType(Random.Range(0, 6));

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
        NotAFK();
        Debug.Log("Shuffled!");
        foreach (GameObject trigger in GameObject.FindGameObjectsWithTag("Trigger"))
        {
            Destroy(trigger);
        }
        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
        {
            Destroy(block);
        }
        for (int i = 0; i < column; i++)
        {
            Instantiate(Resources.Load("Prefabs/Trigger"), new Vector3(i, row - 1, 1), Quaternion.identity);
        }
        int a = Random.Range(0, column - 3);
        int b = Random.Range(0, 6);
        for (int j = 0; j < column; j++)
        {
            Block block = Instantiate(Resources.Load("Prefabs/Blocks/Block01"), new Vector3(j, row + 1, 1), Quaternion.identity).GetComponent<Block>();
            IncreaseBlockCount();
            if (j >= a && j < a + 3) { block.SetType(b); }
            else block.SetType(Random.Range(0, 6));
        }
    }

    public void NotAFK(){isAFK = false; afkTimer = 0;}

    public void updateScore(int _blockCount) { score += (int)Mathf.Pow(_blockCount, 2f) / 2; scoreText.text = score.ToString(); }

    public void IncreaseBlockCount() { blockCount++; }
    public void DecreaseBlockCount() { blockCount--; }

    public bool GetAFKStatus(){return isAFK;}

}
