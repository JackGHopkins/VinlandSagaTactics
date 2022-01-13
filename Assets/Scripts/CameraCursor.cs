using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCursor : MonoBehaviour
{
    private Vector3 OriginalCameraPosition;
    private float OriginalZoom = 1.35f;
    private float MaxZoom = 1.35f * 2;
    private float MinZoom = 1.35f / 2;
    private float CurrentZoom;

    public Cursor Cursor;
    public bool CursorLock = false;
    public Camera Camera;

    // Start is called before the first frame update
    void Start()
    {
        CurrentZoom = OriginalZoom;
        OriginalCameraPosition = this.transform.position;

        if(CursorLock)
            this.transform.position = new Vector3(Cursor.transform.position.x, Cursor.transform.position.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (CursorLock)
            this.transform.position = new Vector3(Cursor.transform.position.x, Cursor.transform.position.y, -10);

        if (Input.GetKeyDown(KeyCode.Q) && CurrentZoom != MaxZoom)
            CurrentZoom *= 2;

        if (Input.GetKeyDown(KeyCode.E) && CurrentZoom != MinZoom)
            CurrentZoom /= 2;

        // Tool Bar
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CursorLock = !CursorLock;
            this.transform.position = (CursorLock) ? new Vector3(Cursor.transform.position.x, Cursor.transform.position.y, -10) : OriginalCameraPosition;
        }

        Camera.orthographicSize = CurrentZoom;
    }
}
