using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isPlaying = false;
    public static float highScore = 0;
    public static float currentScore = 0;
    public static bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime;
        }
        else
        {
            if ( highScore> currentScore )
            {
                highScore = currentScore;
            }
        }
        if (! isPlaying && reset )
        {
            currentScore= 0;
            reset = false;
        }
    }

    public void GameStart()
    {
        reset = true;
        isPlaying= true;
    }
}
