using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Grid : MonoBehaviour
{
    [SerializeField] float hight = 8;
    [SerializeField] float width = 8;
    [SerializeField] float distance = 1;
    [SerializeField] List<GameObject> tiles;
    List<List<GameObject>> gridTiles;
    [SerializeField] float timeDelay= 0.5f;

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
                tile.GetComponent<Tile>().SetColor((Tile.Color)randomTile);
                gridTiles[y].Add(tile);
            }
        }
    }

    public void RemoveTile(GameObject tile)
    {
        Vector2 tilePosition = FindTilePostion(tile);
        if (tilePosition.x == -1 && tilePosition.y == -1) return;

        Destroy(gridTiles[(int)tilePosition.y][(int)tilePosition.x]);
        gridTiles[(int)tilePosition.y][(int)tilePosition.x] = null;
        DropTilesAbove(tilePosition);
        StartCoroutine(RemoveTileWithDelay());
    }
    private IEnumerator RemoveTileWithDelay()
    {
        yield return new WaitForSeconds(timeDelay);
        CheckAndRemoveHorizontalMatchesInAllGrid();
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
                gridTiles[y - 1][(int)position.x].GetComponent<Tile>().isMove = true;
                gridTiles[y][(int)position.x] = null;
            }
        }
    }
    public void CheckAndRemoveHorizontalMatchesInAllGrid()
    {
        StartCoroutine(CheckAndRemoveHorizontalMatchesWithDelay());
    }

    private IEnumerator CheckAndRemoveHorizontalMatchesWithDelay()
    {
        bool findMatches = false;
        for (int y = 0; y < hight; y++)
        {
            for (int x = 0; x < width - 2; x++)
            {
                GameObject currentTile = gridTiles[y][x];
                if (currentTile == null) continue;

                Tile.Color currentTileType = currentTile.GetComponent<Tile>().GetColor();
                List<GameObject> matchedTiles = new List<GameObject>();

                int matchCount = GetMatchCount(x, y, currentTileType, matchedTiles);

                if (matchCount >= 3 && IsAnyTileMoved(matchedTiles))
                {
                    foreach (GameObject tile in matchedTiles)
                    {
                        RemoveTile(tile);
                    }
                    findMatches = true;
                    yield return new WaitForSeconds(timeDelay);
                    break;
                }
            }
            if(findMatches)
                break;
        }
    }

    private int GetMatchCount(int x, int y, Tile.Color color, List<GameObject> matchedTiles)
    {
        int count = 0;
        for (int i = x; i < width; i++)
        {
            GameObject tile = gridTiles[y][i];
            if (tile != null && tile.GetComponent<Tile>().GetColor() == color)
            {
                matchedTiles.Add(tile);
                count++;
            }
            else
            {
                break;
            }
        }
        return count;
    }

    private bool IsAnyTileMoved(List<GameObject> tiles)
    {
        foreach (GameObject tile in tiles)
        {
            if (tile.GetComponent<Tile>().isMove)
            {
                return true;
            }
        }
        return false;
    }

}
