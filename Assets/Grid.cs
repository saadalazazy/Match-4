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
        for(int y = 0; y < hight; ++y)
        {
            for(int x = 0; x < width; ++x)
            {
                Vector3 postion = new Vector3(x * distance, y * distance, -0.5f);
                int randomTile = Random.Range(0, tiles.Count);
                GameObject tile = Instantiate(tiles[randomTile], postion, Quaternion.identity);
                tile.GetComponent<Tile>().SetIndex(x, y);
            }
        }
    }

}
