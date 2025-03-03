using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Renderer renderer;
    Grid grid;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        grid = FindObjectOfType<Grid>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            grid.RemoveTile(gameObject);
        }
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log(grid.FindTilePostion(gameObject));
        }
    }

}
