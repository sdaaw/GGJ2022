using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            FindObjectOfType<GameManager>().GameWon();
        }
    }
}
