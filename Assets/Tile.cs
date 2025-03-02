using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Renderer renderer;
    private Vector2 index;
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
            Destroy(gameObject);
        }
    }
    public void SetIndex(int x  , int y)
    {
        index = new Vector2(x, y);
    }

}
