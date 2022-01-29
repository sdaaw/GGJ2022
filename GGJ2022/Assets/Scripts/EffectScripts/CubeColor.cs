using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColor : MonoBehaviour
{

    [Range(0f, 1f)]
    public float randColorVar;

    [Range(0f, 1f)]
    public float glowStrength;

    private Renderer rend;

    public Color baseColor;

    public Material objectMaterial;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        Color color = new Color(glowStrength, glowStrength, glowStrength);
        color = new Color(glowStrength + baseColor.r + Random.Range(0, randColorVar), glowStrength + baseColor.g + Random.Range(0, randColorVar), glowStrength + baseColor.b + Random.Range(0, randColorVar));
        if (rend != null)
        {
            rend.material.color = color;
        }
        else
        {
            objectMaterial.color = color;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rend.material.color != baseColor)
        {
            Color color = new Color(glowStrength, glowStrength, glowStrength);
            color = new Color(glowStrength + baseColor.r, glowStrength + baseColor.g, glowStrength + baseColor.b);
            rend.material.color = color;
        }
    }
}
