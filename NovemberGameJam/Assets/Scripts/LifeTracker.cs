using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTracker : MonoBehaviour
{
    #region Variables

    // Life fields
    private int lives;
    private bool changer;

    private PlayerState gameState;
    private PlayerState prevGameState;

    public GameObject entity;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        changer = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFromOutsideClass();
        CheckChanges();
        prevGameState = gameState;

        Debug.Log(lives);
    }

    /// <summary>
    /// Pulls variables from out of this class
    /// </summary>
    public void UpdateFromOutsideClass()
    {
        gameState = gameObject.GetComponent<FieldOfView>().playerState;

        if (changer)
        {
            // update entity position
            entity.GetComponent<EntityFollow>().distanceFromPlayer -= 1.0f;

            // don't update it again
            changer = false;

            // if dead
            if (lives == 0)
            {
                gameObject.GetComponent<FieldOfView>().playerState = PlayerState.gameOver;
            }
        }
    }

    /// <summary>
    /// Update lives if the game state changed to death
    /// </summary>
    public void CheckChanges()
    {
        if (gameState != prevGameState && gameState == PlayerState.death)
        {
            lives -= 1;
            changer = true;
        }
    }


}
