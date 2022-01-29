using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObjectBehaviour : MonoBehaviour
{

    public float rotationSpeed;
    public float movementSpeed;

    public float objectScale;

    public Color glowColor;
    public Color baseColor;

    public GameObject baseMaterialObject;
    public GameObject glowMaterialObject;

    private Material gm0, bm0;

    public Vector3 direction;

    public float lifeTime;
    private float timer;

    private Vector3 randDir;
    // Start is called before the first frame update
    void Start()
    {
        randDir = Random.insideUnitCircle.normalized;
        if(baseMaterialObject != null)
        {
            if (baseMaterialObject.GetComponent<Renderer>().material != null)
            {
                bm0 = new Material(baseMaterialObject.GetComponent<Renderer>().material);
                baseMaterialObject.GetComponent<Renderer>().material = bm0;
            }
        }
        if(glowMaterialObject != null)
        {
            if (glowMaterialObject.GetComponent<Renderer>().material != null)
            {
                gm0 = new Material(glowMaterialObject.GetComponent<Renderer>().material);
                glowMaterialObject.GetComponent<Renderer>().material = gm0;
                glowMaterialObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", GetRandomBrightColor());
            }
        }

    }

    private Color GetRandomBrightColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1 * Time.deltaTime;
        if(timer > lifeTime)
        {
            Destroy(gameObject);
        }
        transform.position += direction * movementSpeed * Time.deltaTime;
        transform.Rotate(randDir, rotationSpeed / 4);
    }
}
