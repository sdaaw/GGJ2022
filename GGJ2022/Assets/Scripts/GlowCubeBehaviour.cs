using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowCubeBehaviour : MonoBehaviour
{
    public enum EffectType // your custom enumeration
    {
        Pulsing,
        Wandering,
        Circling
    };
    public bool doRotation;
    public float rotationSpeed;

    public float pulseSpeed;

    [Range(0f, 1f)]
    public float randColorVar;

    [Range(0f, 1f)]
    public float glowStrength;

    private Renderer rend;

    public Color baseColor;

    public Material objectMaterial;

    public EffectType effectType;

    private delegate void EffectDelegate();
    private EffectDelegate effectMethod;

    public bool doChildren;
    public int childrenAmount;
    public float childrenSpread;


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

        //delegates pog!!
        if(effectType == EffectType.Pulsing)
        {
            effectMethod = PulseCube;
        }

        if (doChildren)
        {
            doChildren = false;
            SpawnChildren();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //saves a few cpu cycles maybe from not checking an extra IF-statement hahahasufhdsfjgfhdf

        effectMethod();


        if(doRotation)
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }
        if(rend.material.color != baseColor)
        {
            Color color = new Color(glowStrength, glowStrength, glowStrength);
            color = new Color(glowStrength + baseColor.r + Random.Range(0, randColorVar), glowStrength + baseColor.g + Random.Range(0, randColorVar), glowStrength + baseColor.b + Random.Range(0, randColorVar));
            rend.material.color = color;
        }

    }

    private void SpawnChildren()
    {
        for(int i = 0; i < childrenAmount; i++)
        {
            GameObject a = Instantiate(gameObject, new Vector3(transform.position.x + Random.Range(-childrenSpread, childrenSpread), transform.position.y + Random.Range(-childrenSpread, childrenSpread), transform.position.z + Random.Range(-childrenSpread, childrenSpread)), Quaternion.identity);
            a.transform.localScale = new Vector3(transform.localScale.x / 4, transform.localScale.y / 4, transform.localScale.z / 4);
            a.GetComponent<GlowCubeBehaviour>().pulseSpeed = pulseSpeed / 2;
            a.GetComponent<GlowCubeBehaviour>().rotationSpeed = rotationSpeed / 2;
        }
    }

    private void PulseCube()
    {
        float sinVal = Mathf.Sin(Time.time * pulseSpeed) * 0.02f;
        transform.localScale = new Vector3(transform.localScale.x + sinVal, transform.localScale.y + sinVal, transform.localScale.z + sinVal);
    }

}
