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

    private Vector3 spawn;

    public GameObject entity;

    [Header("Sound Sources")]
    [SerializeField] GameObject whisper1;
    [SerializeField] GameObject whisper2;
    [SerializeField] GameObject whisper3;
    [SerializeField] GameObject jumpScare;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        changer = false;
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFromOutsideClass();
        CheckChanges();
        prevGameState = gameState;

        Debug.Log(transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
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

            // add in those whispers
            if (lives == 2)
            {
                whisper2.GetComponent<AudioSource>().Play();
            }
            else if (lives == 1)
            {
                whisper3.GetComponent<AudioSource>().Play();
            }

            // if dead
            if (lives == 0)
            {
                transform.position = spawn;
                gameObject.GetComponent<FieldOfView>().playerPos = spawn;
                entity.GetComponent<EntityFollow>().UpdatePosition();

                jumpScare.GetComponent<AudioSource>().Play();
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
