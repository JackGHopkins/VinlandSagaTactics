using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCursor : MonoBehaviour
{
    private Vector3 originalCameraPosition;
    private float originalZoom = 1.35f;
    private float maxZoom = 1.35f * 2;
    private float minZoom = 1.35f / 2;
    private float currentZoom;

    public Cursor cursor;
    public bool cursorLock = false;
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        currentZoom = originalZoom;
        originalCameraPosition = this.transform.position;

        if(cursorLock)
            this.transform.position = new Vector3(cursor.transform.position.x, cursor.transform.position.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (cursorLock)
            this.transform.position = new Vector3(cursor.transform.position.x, cursor.transform.position.y, -10);

        if (Input.GetKeyDown(KeyCode.Q) && currentZoom != maxZoom)
            currentZoom *= 2;

        if (Input.GetKeyDown(KeyCode.E) && currentZoom != minZoom)
            currentZoom /= 2;

        // Tool Bar
        if (Input.GetKeyDown(KeyCode.F1))
        {
            cursorLock = !cursorLock;
            this.transform.position = (cursorLock) ? new Vector3(cursor.transform.position.x, cursor.transform.position.y, -10) : originalCameraPosition;
        }

        camera.orthographicSize = currentZoom;
    }
}
