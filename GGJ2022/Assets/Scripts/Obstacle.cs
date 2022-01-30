using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Character>())
        {
            Character c = other.GetComponent<Character>();
            c.health -= damage;
            //TODO: slow down the game when taking dmg?
            SoundManager.PlayASource("Hit");
        }
    }
}
