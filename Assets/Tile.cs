using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Renderer renderer;
    Grid grid;
    public bool isMove = false;
    public enum Color
    {
        red,
        green,
        blue
    }
    private Color color;
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
            Debug.Log(isMove);
            Debug.Log(color);
        }
    }
    public void SetColor(Color colorType)
    {
        color = colorType;
    }
    public Color GetColor() { return color; }

}
