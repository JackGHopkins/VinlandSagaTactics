using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] float Height = 0.16f;
    [SerializeField] float Width = 0.32f;
    [SerializeField] MainGame Game;
    public Vector2 Position;

    // Start is called before the first frame update
    void Start()
    {
        Position = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && Position.y > 0 ) { 
            this.transform.position = new Vector2(this.transform.position.x + Width, this.transform.position.y + Height);
            Position.y--;
        }
        if (Input.GetKeyDown(KeyCode.S) && Position.y < Game.GetMapHeight() - 1) { 
            this.transform.position = new Vector2(this.transform.position.x - Width, this.transform.position.y - Height); 
            Position.y++;
        }
        if (Input.GetKeyDown(KeyCode.A) && Position.x > 0) { 
            this.transform.position = new Vector2(this.transform.position.x - Width, this.transform.position.y + Height); 
            Position.x--;
        }
        if (Input.GetKeyDown(KeyCode.D) && Position.x < Game.GetMapWidth() - 1) {
            this.transform.position = new Vector2(this.transform.position.x + Width, this.transform.position.y - Height); 
            Position.x++;
        }
    }
}
