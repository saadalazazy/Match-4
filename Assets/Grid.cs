using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] float hight = 8;
    [SerializeField] float width = 8;
    [SerializeField] float distance = 1;
    [SerializeField] List<GameObject> tiles;
    List<List<GameObject>> gridTiles;

    [SerializeField] float removeDelay = 0.5f;

    private void Start()
    {
        IntializeGrid();
    }

    private void IntializeGrid()
    {
        gridTiles = new List<List<GameObject>>();
        for (int y = 0; y < hight; ++y)
        {
            gridTiles.Add(new List<GameObject>());
            for (int x = 0; x < width; ++x)
            {
                Vector3 postion = new Vector3(x * distance, y * distance, -0.5f);
                int randomTile = Random.Range(0, tiles.Count);
                GameObject tile = Instantiate(tiles[randomTile], postion, Quaternion.identity);
                gridTiles[y].Add(tile);
            }
        }
    }

    public void RemoveTile(GameObject tile)
    {
        StartCoroutine(RemoveTileWithDelay(tile));
    }

    private IEnumerator RemoveTileWithDelay(GameObject tile)
    {
        Vector2 tilePosition = FindTilePostion(tile);
        if (tilePosition.x == -1 && tilePosition.y == -1) yield break;

        Destroy(gridTiles[(int)tilePosition.y][(int)tilePosition.x]);
        gridTiles[(int)tilePosition.y][(int)tilePosition.x] = null;

        DropTilesAbove(tilePosition);
        yield return new WaitForSeconds(removeDelay);
        CheckAndRemoveHorizontalMatches(tilePosition);
    }

    public Vector2 FindTilePostion(GameObject tile)
    {
        for (int y = 0; y < hight; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                if (tile == gridTiles[y][x])
                    return new Vector2(x, y);
            }
        }
        return new Vector2(-1, -1);
    }

    private void DropTilesAbove(Vector2 position)
    {
        for (int y = (int)position.y + 1; y < hight; y++)
        {
            if (gridTiles[y][(int)position.x] != null)
            {
                gridTiles[y - 1][(int)position.x] = gridTiles[y][(int)position.x];
                gridTiles[y][(int)position.x] = null;
            }
        }
    }

    public void CheckAndRemoveHorizontalMatches(Vector2 tilePostion)
    {
        bool matchesRemoved = false;
        int y = (int)tilePostion.y;
        for (int x = 0; x < width - 2; x++)
        {
            GameObject currentTile = gridTiles[y][x];
            if (currentTile != null)
            {
                string currentTileType = currentTile.tag;
                if (gridTiles[y][x + 1] != null && gridTiles[y][x + 1].tag == currentTileType &&
                    gridTiles[y][x + 2] != null && gridTiles[y][x + 2].tag == currentTileType)
                {
                    StartCoroutine(RemoveMatchedTilesWithDelay(y, x));
                    matchesRemoved = true;
                }
            }
        }

        if (matchesRemoved)
        {
            StartCoroutine(CheckMatchesAgainAfterDelay(tilePostion));
        }
    }

    private IEnumerator RemoveMatchedTilesWithDelay(int y, int x)
    {
        yield return new WaitForSeconds(removeDelay);
        RemoveTile(gridTiles[y][x]);
        RemoveTile(gridTiles[y][x + 1]);
        RemoveTile(gridTiles[y][x + 2]);
    }

    private IEnumerator CheckMatchesAgainAfterDelay(Vector2 tilePostion)
    {
        yield return new WaitForSeconds(removeDelay);
        CheckAndRemoveHorizontalMatches(tilePostion);
    }
}