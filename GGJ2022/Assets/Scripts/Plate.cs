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
        if(baseMaterialObject == null)
        {
            baseMaterialObject = transform.GetChild(0).gameObject;
        }
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
            PulseColorRed();
        }
        if (plateType == PlateType.Blue)
        {
            PulseColorBlue();
        }
    }

    public void PulseColorRed()
    {
        bm0.color = new Color(Mathf.Max(2f, Mathf.Sin(Time.time * 4) * 10), Mathf.Max(0.8f, Mathf.Sin(Time.time * 4) * 2), Mathf.Max(0.8f, Mathf.Sin(Time.time * 4) * 2));
    }

    public void PulseColorBlue()
    {
        bm0.color = new Color(Mathf.Max(0.8f, Mathf.Sin(Time.time * 4) * 2), Mathf.Max(0.8f, Mathf.Sin(Time.time * 4) * 2), Mathf.Max(2f, Mathf.Sin(Time.time * 4) * 10));
    }
}
