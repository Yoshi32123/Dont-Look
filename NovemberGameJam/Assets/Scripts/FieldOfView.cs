using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public enum PlayerState
{
    safe,
    danger,
    death,
    gameOver
}

public class FieldOfView : MonoBehaviour
{
    #region Variables

    [Header("Toggle Debug Lines")]
    [SerializeField] bool lines = true;

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

    // x camera storage
    public GameObject xCam;

    // playerState track
    public PlayerState playerState = PlayerState.safe;

    // fps controller script
    

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

        if (playerState != PlayerState.gameOver)
        {
            DetermineDanger();
        }
        else
        {
            Debug.Log("trying to change rotation...");

            gameObject.GetComponent<FirstPersonController>().enabled = false;

            transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 1.0f);
            xCam.transform.rotation = new Quaternion(0.0f, 180.0f, -20.0f, 1.0f);
            transform.position = playerPos;
        }
        

        if (lines)
        {
            DebugLines();
        }

        FogHandle(playerState);
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

    /// <summary>
    /// Check players forward vector amongst others
    /// </summary>
    public void DetermineDanger()
    {
        // death
        if (actualFowardVec.x > dangerFOVright.x || actualFowardVec.x < dangerFOVleft.x || actualFowardVec.z < lockedRightVec.z)
        {
            //Debug.Log("Death...");
            playerState = PlayerState.death;
        }

        // red tint
        else if (actualFowardVec.x >= safeFOVright.x && actualFowardVec.x <= dangerFOVright.x || actualFowardVec.x <= safeFOVleft.x && actualFowardVec.x >= dangerFOVleft.x)
        {
            //Debug.Log("Danger!!");
            playerState = PlayerState.danger;
        }

        // normal screen tint
        else
        {
            //Debug.Log("Safe...");
            playerState = PlayerState.safe;
        }
    }

    /// <summary>
    /// Handles the fog in the game
    /// </summary>
    /// <param name="state"></param>
    public void FogHandle(PlayerState state)
    {
        RenderSettings.fog = true;

        if (state == PlayerState.safe)
        {
            RenderSettings.fogColor = Color.grey;
            RenderSettings.fogDensity = 0.07f;
        }
        else if (state == PlayerState.danger)
        {
            RenderSettings.fogColor = new Color(0.5f, 0, 0, 1);
            RenderSettings.fogDensity = 0.1f;
        }
        else if (state == PlayerState.death || state == PlayerState.death)
        {
            RenderSettings.fogColor = Color.black;
            RenderSettings.fogDensity = 0.3f;
        }
    }
}
