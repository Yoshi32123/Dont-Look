using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSounds : MonoBehaviour
{
    [Header("Random Sounds")]
    [SerializeField] GameObject random1;
    [SerializeField] GameObject random2;
    [SerializeField] GameObject random3;
    [SerializeField] GameObject random4;
    [SerializeField] GameObject random5;
    [SerializeField] GameObject random6;
    [SerializeField] GameObject random7;
    [SerializeField] GameObject random8;
    [SerializeField] GameObject random9;

    private List<GameObject> mySounds;

    private int m_frameCounter = 0;
    private float m_timeCounter = 0.0f;
    private float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;

    private int secondsPast = 0;
    private int iteration = 0;

    // Start is called before the first frame update
    void Start()
    {
        mySounds = new List<GameObject>();

        mySounds.Add(random1);
        mySounds.Add(random2);
        mySounds.Add(random3);
        mySounds.Add(random4);
        mySounds.Add(random5);
        mySounds.Add(random6);
        mySounds.Add(random7);
        mySounds.Add(random8);
        mySounds.Add(random9);
    }

    // Update is called once per frame
    void Update()
    {
        // time tracking
        if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            //This code will break if you set your m_refreshTime to 0, which makes no sense.
            m_lastFramerate = (float)m_frameCounter / m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;

            // updating the seconds
            secondsPast++;
        }

        // playing a random sound every few seconds
        if (secondsPast >= 20)
        {
            secondsPast = 0;

            mySounds[iteration].GetComponent<AudioSource>().Play();

            iteration++;

            if (iteration >= 9)
            { iteration = 0; }
        }
    }
}
