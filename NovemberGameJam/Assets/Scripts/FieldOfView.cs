using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    #region Variables

    // main vectors
    private Vector3 playerPos;
    private Vector3 lockedForwardVec;
    private Vector3 lockedUpVec;
    private Vector3 lockedRightVec;
    private Vector3 actualFowardVec;

    // unit vectors
    private Vector3 degree45 = new Vector3(Mathf.Sin(Mathf.PI * 45 / 180), 0.0f, Mathf.Sin(Mathf.PI * 45 / 180));
    private Vector3 negDegree45 = new Vector3(-Mathf.Sin(Mathf.PI * 45 / 180), 0.0f, Mathf.Sin(Mathf.PI * 45 / 180));
    private Vector3 degree60 = new Vector3(Mathf.Sin(Mathf.PI * 60 / 180), 0.0f, Mathf.Cos(Mathf.PI * 60 / 180));
    private Vector3 negDegree60 = new Vector3(-Mathf.Sin(Mathf.PI * 60 / 180), 0.0f, Mathf.Cos(Mathf.PI * 60 / 180));

    // fov vectors
    private Vector3 safeFOVleft;
    private Vector3 safeFOVright;
    private Vector3 dangerFOVleft;
    private Vector3 dangerFOVright;

    [Header("Toggle Debug Lines")]
    [SerializeField] bool lines = true;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // storing position
        playerPos = transform.position;

        // creating main vectors
        lockedUpVec = playerPos + new Vector3(0.0f, 5.0f, 0.0f);
        lockedForwardVec = playerPos + new Vector3(0.0f, 0.0f, 5.0f);
        lockedRightVec = playerPos + new Vector3(5.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVectors();

        if (lines)
        {
            DebugLines();
        }
    }

    /// <summary>
    /// Update main vectors based on player position
    /// </summary>
    public void UpdateVectors()
    {
        // updating player pos
        playerPos = transform.position;

        // updating main vectors
        lockedUpVec = playerPos + new Vector3(0.0f, 5.0f, 0.0f);
        lockedForwardVec = playerPos + new Vector3(0.0f, 0.0f, 5.0f);
        lockedRightVec = playerPos + new Vector3(5.0f, 0.0f, 0.0f);

        // updating side vectors
        safeFOVright = playerPos + (5 * degree45);
        safeFOVleft = playerPos + (5 * negDegree45);
        dangerFOVright = playerPos + (5 * degree60);
        dangerFOVleft = playerPos + (5 * negDegree60);

        // storing the actual forward
        actualFowardVec = playerPos + (5 * transform.forward);
    }

    /// <summary>
    /// Draws lines for all main vectors
    /// </summary>
    public void DebugLines()
    {
        // forward
        Debug.DrawLine(playerPos, lockedForwardVec, Color.green);

        // up vector
        Debug.DrawLine(playerPos, lockedUpVec, Color.blue);

        // fov
        Debug.DrawLine(playerPos, safeFOVright, Color.white);
        Debug.DrawLine(playerPos, safeFOVleft, Color.white);

        // fov danger
        Debug.DrawLine(playerPos, dangerFOVright, Color.red);
        Debug.DrawLine(playerPos, dangerFOVleft, Color.red);

        // actual forward
        Debug.DrawLine(playerPos, actualFowardVec, Color.yellow);
    }
}
