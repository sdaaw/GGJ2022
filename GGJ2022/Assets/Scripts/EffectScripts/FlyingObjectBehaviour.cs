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

    public Material baseMaterial;
    public Material glowMaterial;

    private Material gm0, bm0;

    public Vector3 direction;

    public float lifeTime;
    private float timer;

    private Vector3 randDir;
    // Start is called before the first frame update
    void Start()
    {
        
        randDir = Random.insideUnitCircle.normalized;
        if (baseMaterial != null)
        {
            bm0 = new Material(baseMaterial);
        }
        if(glowMaterial != null)
        {
            gm0 = new Material(glowMaterial);

            glowMaterial.SetColor("_EmissionColor", GetRandomBrightColor());
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
