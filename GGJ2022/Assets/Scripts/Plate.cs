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

    public GameObject baseMaterialObject;
    private Material bm0;
    private void Start()
    {
        int rand = Random.Range(1, 20);
        int rand2 = Random.Range(1, 20);
        if (rand > 17)
        {
            plateType = PlateType.Red;
        } else
        {
            if (rand2 > 17)
            {
                plateType = PlateType.Blue;
            }
        }

        if (baseMaterialObject != null)
        {
            if (baseMaterialObject.GetComponent<Renderer>().material != null)
            {
                bm0 = new Material(baseMaterialObject.GetComponent<Renderer>().material);
                baseMaterialObject.GetComponent<Renderer>().material = bm0;
            }
        }
    }

    private void FixedUpdate()
    {
        if(plateType == PlateType.Red)
        {
            PulseColor(Color.red);
        }
        if (plateType == PlateType.Blue)
        {
            PulseColor(Color.blue);
        }
    }

    public void PulseColor(Color color)
    {
        bm0.color = new Color(color.r + Mathf.Max(0.6f, Mathf.Sin(Time.time * 4) * 2), color.g + Mathf.Max(0.6f, Mathf.Sin(Time.time * 4) * 2), color.b + Mathf.Max(0.6f, Mathf.Sin(Time.time * 4) * 2));
    }
}
