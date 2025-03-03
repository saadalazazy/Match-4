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
    private void Start()
    {
        gridTiles = new List<List<GameObject>>();
        for(int y = 0; y < hight; ++y)
        {
            gridTiles.Add(new List<GameObject>());
            for(int x = 0; x < width; ++x)
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
        Vector2 tilePostion = FindTilePostion(tile);
        Destroy(gridTiles[(int)tilePostion.y][(int)tilePostion.x]);
        DropTilesAbove(tilePostion);
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
            if (gridTiles[(int)position.y][y] != null)
            {
                gridTiles[y - 1][(int)position.x] = gridTiles[y][(int)position.x];
            }
            else
            {
                gridTiles[y - 1][(int)position.x] = null;
            }
        }
    }

}
