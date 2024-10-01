using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{

    public static ChunkManager Instance;

    [Header("Elements")]
    [SerializeField] private Chunk[] chunkPrefabs;
    [SerializeField] private Chunk[] levelChunk;

    [SerializeField] private LevelSO[] levels;

    private GameObject finishLine;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        GenerateLevel();
        finishLine = GameObject.FindWithTag("Finish");

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateLevel()
    {
        int currentLevel = GetLevel();

        currentLevel = currentLevel % levels.Length;

        LevelSO level = levels[currentLevel];

        CreateLevel(level.Chunks);


    }


    private void CreateLevel(Chunk[] levelChunk)
    {
        Vector3 chunkPosition = Vector3.zero;

        for (int i = 0; i < levelChunk.Length; i++)
        {

            //chunkPosition.z = i * 10;

            Chunk chunkTocreate = levelChunk[i];

            if (i > 0)
            {
                chunkPosition.z += chunkTocreate.GetLength() / 2;
            }

            Chunk chunkInstance = Instantiate(chunkTocreate, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }

    private void CreateRandomLevel()
    {
        Vector3 chunkPosition = Vector3.zero;

        for (int i = 0; i < 5; i++)
        {

            //chunkPosition.z = i * 10;

            Chunk chunkTocreate = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

            if (i > 0)
            {
                chunkPosition.z += chunkTocreate.GetLength() / 2;
            }

            Chunk chunkInstance = Instantiate(chunkTocreate, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength()/2;
        }
    }
   
    public float GetFinishZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevel()
    {
       return PlayerPrefs.GetInt("level",0);
    }
}
