using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public PlateType plateType;

    public enum PlateType
    {
        Empty,
        Normal,
        Red,
        Blue
    }

    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        PulseColor(Color.red);
    }

    public void PulseColor(Color color)
    {
        rend.material.color = new Color(color.r + Mathf.Sin(Time.time) * 10, color.g, color.b);
    }
}
