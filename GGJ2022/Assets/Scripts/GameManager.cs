using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gameDifficultyScaler = 1;

    private void Update()
    {
        RotateLevel.speedIncrease = gameDifficultyScaler;
    }
}
