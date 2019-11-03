using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFollow : MonoBehaviour
{
    #region Variables

    // main vectors
    private Vector3 entityPos;
    public float distanceFromPlayer;

    // player object storage
    public GameObject player;

    // one last move
    private bool notDone;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        distanceFromPlayer = 5.0f;
        entityPos = player.transform.position - new Vector3(0.0f, 0.0f, distanceFromPlayer);
        notDone = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<FieldOfView>().playerState != PlayerState.gameOver || notDone)
        {
            UpdatePosition();
        }
        else if (notDone)
        {
            notDone = false;
        }
    }

    /// <summary>
    /// Tracks player pos and tracks it
    /// </summary>
    public void UpdatePosition()
    {
        entityPos = player.transform.position - new Vector3(0.0f, 0.0f, distanceFromPlayer);
        transform.position = entityPos;
    }
}
