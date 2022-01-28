using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingCube : MonoBehaviour
{

    public bool doRotation;
    public float rotationSpeed;

    [Range(0f, 1f)]
    public float randColorVar;

    [Range(0f, 1f)]
    public float glowStrength;

    private Renderer rend;

    public Color baseColor;

    public Material objectMaterial;


    public bool doChildren;
    public int childrenAmount;
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

        if (doChildren)
        {
            doChildren = false;
            SpawnChildren();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if(doRotation)
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }
        if(rend.material.color != baseColor)
        {
            Color color = new Color(glowStrength, glowStrength, glowStrength);
            color = new Color(glowStrength + baseColor.r, glowStrength + baseColor.g, glowStrength + baseColor.b);
            rend.material.color = color;
        }
        if (orbitChildren)
        {
            OrbitAround(orbitRadius, orbitSpeed);
        }


    }

    private void SpawnChildren()
    {
        for(int i = 0; i < childrenAmount; i++)
        {
            GameObject a = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y , transform.position.z), Quaternion.identity);
            a.transform.localScale = new Vector3(transform.localScale.x / 4, transform.localScale.y / 4, transform.localScale.z / 4);
            a.GetComponent<Renderer>().material.color = new Color(glowStrength + baseColor.r + Random.Range(0, randColorVar), glowStrength + baseColor.g + Random.Range(0, randColorVar), glowStrength + baseColor.b + Random.Range(0, randColorVar));
            AddOrbitingObject(a);
        }
    }

    /*private void PulseCube()
    {
        sinVal = Mathf.Sin(Time.time * pulseSpeed) * 0.02f;
        transform.localScale = new Vector3(transform.localScale.x + sinVal, transform.localScale.y + sinVal, transform.localScale.z + sinVal);
    }*/


    public void AddOrbitingObject(GameObject o)
    {
        orbitingObjects.Add(o);
        float theta = (Mathf.PI * 2) / orbitingObjects.Count;
        float angle;
        for (int i = 0; i < orbitingObjects.Count; i++)
        {
            angle = theta * i;
            orbitingObjects[i].GetComponent<OrbitingCube>().currOrbitAngle = angle;
        }
    }

    public void OrbitAround(float radius, float speed)
    {
        if (orbitingObjects.Count != 0)
        {
            foreach (GameObject o in orbitingObjects)
            {
                o.GetComponent<OrbitingCube>().currOrbitAngle += speed * Time.deltaTime;
                Vector3 offset = new Vector3(Mathf.Sin(o.GetComponent<OrbitingCube>().currOrbitAngle), Mathf.Sin(o.GetComponent<OrbitingCube>().currOrbitAngle * (Time.deltaTime * 10)), Mathf.Cos(o.GetComponent<OrbitingCube>().currOrbitAngle)) * radius;
                
                Vector3 pos = new Vector3(-Mathf.Sin(Time.time), Mathf.Sin(Time.time), -Mathf.Cos(Time.time));
                o.transform.Rotate(Vector3.right, 5f);
                o.transform.position = dadObject.transform.position + offset + pos;
            }
        }
    }

}
