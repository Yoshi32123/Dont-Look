﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // gets mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Trying to rotate");
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        // Anchoring to mouse
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

        // Rotating the object
        //move.x = 0;
        //move.z = 0;
        transform.Rotate(move, Space.World);
    }
}
