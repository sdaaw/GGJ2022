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
    public float orbitRadius;
    public float orbitSpeed;

    private float currOrbitAngle;

    private GameObject dadObject;

    private List<GameObject> orbitingObjects = new List<GameObject>();


    public bool orbitChildren;

    private float sinVal;
    // Start is called before the first frame update
    void Start()
    {
        if(dadObject == null)
        {
            dadObject = gameObject;
        }
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
        if (orbitChildren)
        {
            OrbitAround(orbitRadius, orbitSpeed);
        } else
        {
            effectMethod();
        }


    }

    private void SpawnChildren()
    {
        for(int i = 0; i < childrenAmount; i++)
        {
            GlowCubeBehaviour gcb;
            GameObject a = Instantiate(gameObject, new Vector3(transform.position.x + Random.Range(-childrenSpread, childrenSpread), transform.position.y + Random.Range(-childrenSpread, childrenSpread), transform.position.z + Random.Range(-childrenSpread, childrenSpread)), Quaternion.identity);
            a.transform.localScale = new Vector3(transform.localScale.x / 4, transform.localScale.y / 4, transform.localScale.z / 4);
            gcb = a.GetComponent<GlowCubeBehaviour>();
            gcb.pulseSpeed = pulseSpeed / 2;
            gcb.rotationSpeed = rotationSpeed / 2;
            gcb.pulseSpeed = 0;
            gcb.glowStrength = glowStrength / 1.5f;
            a.GetComponent<Renderer>().material.color = new Color(glowStrength + baseColor.r + Random.Range(0, randColorVar), glowStrength + baseColor.g + Random.Range(0, randColorVar), glowStrength + baseColor.b + Random.Range(0, randColorVar));
            AddOrbitingObject(a);
        }
    }

    private void PulseCube()
    {
        sinVal = Mathf.Sin(Time.time * pulseSpeed) * 0.02f;
        transform.localScale = new Vector3(transform.localScale.x + sinVal, transform.localScale.y + sinVal, transform.localScale.z + sinVal);
    }


    public void AddOrbitingObject(GameObject o)
    {
        orbitingObjects.Add(o);
        float theta = (Mathf.PI * 2) / orbitingObjects.Count;
        float angle;
        for (int i = 0; i < orbitingObjects.Count; i++)
        {
            angle = theta * i;
            orbitingObjects[i].GetComponent<GlowCubeBehaviour>().currOrbitAngle = angle;
        }
    }

    public void OrbitAround(float radius, float speed)
    {
        if (orbitingObjects.Count != 0)
        {
            foreach (GameObject o in orbitingObjects)
            {
                o.GetComponent<GlowCubeBehaviour>().currOrbitAngle += speed * Time.deltaTime;
                Vector3 offset = new Vector3(Mathf.Sin(o.GetComponent<GlowCubeBehaviour>().currOrbitAngle), Mathf.Sin(o.GetComponent<GlowCubeBehaviour>().currOrbitAngle * (Time.deltaTime * 10)), Mathf.Cos(o.GetComponent<GlowCubeBehaviour>().currOrbitAngle)) * radius;
                
                Vector3 pos = new Vector3(-Mathf.Sin(Time.time), Mathf.Sin(Time.time), -Mathf.Cos(Time.time));
                o.transform.Rotate(Vector3.right, 5f);
                o.transform.position = dadObject.transform.position + offset + pos;
            }
        }
    }

}
