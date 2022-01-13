using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    private Map Map;

    [SerializeField] int Width = 5;
    [SerializeField] int Height = 5;


    // Start is called before the first frame update
    void Awake()
    { 
        Map = new Map(Width, Height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetMapWidth() { return Width; }
    public int GetMapHeight() { return Height; }
}
