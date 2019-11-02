using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    private Vector3 position;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }


    public void UpdatePosition()
    {
        // detect key press for left/right and forward/backward
        if (Input.GetKey(KeyCode.W))
        {
            position.z += 0.2f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            position.z -= 0.2f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            position.x += 0.2f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            position.x -= 0.2f;
        }

        gameObject.transform.position = position;
    }
}
