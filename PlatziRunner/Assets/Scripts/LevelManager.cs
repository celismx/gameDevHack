using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { set; get; }

    public List<GameObject> tiles = new List<GameObject>();
    private Transform playerTransform;
    private float spawnX = 0.0f;
    private float titleLength = 17f;
    private int amnTiles = 25;
    private float safeZone = 25f;
    private int lastTileIndex;

    public GameObject panel_;

    private List<GameObject> activeTiles;

    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        activeTiles = new List<GameObject>();
        for (int i = 0; i < amnTiles; i++)
        {
            if (i < 2)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x - safeZone > (spawnX - amnTiles * titleLength))
        {
            SpawnTile();
            DeleteTile();
        }

        if(GameManager.sharedInstance.currentGameState == GameState.inWin)
        {
            panel_.SetActive(true);
        }
    }

    void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
        {
            go = Instantiate(tiles[RandomTileIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate(tiles[prefabIndex]) as GameObject;
        }
        go.transform.SetParent(transform);
        go.transform.position = Vector3.right * spawnX;
        spawnX += titleLength;
        activeTiles.Add(go);

    }
    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
    private int RandomTileIndex()
    {
        if (tiles.Count <= 1)
        {
            return 0;
        }
        int randomIndex = lastTileIndex;
        while (randomIndex == lastTileIndex)
        {
            randomIndex = Random.Range(0, tiles.Count);
        }
        lastTileIndex = randomIndex;
        return randomIndex;

    }

}
