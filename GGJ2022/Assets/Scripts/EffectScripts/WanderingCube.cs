using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingCube : MonoBehaviour
{

    private List<GameObject> wanderingCubeList = new List<GameObject>();

    public float lifeTime;
    public float shrinkSpeed;
    private float timer;

    public float movementSpeed;

    private Renderer m_renderer;
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1 * Time.deltaTime;
        if(timer > lifeTime)
        {
            transform.localScale = new Vector3(transform.localScale.x - shrinkSpeed, transform.localScale.y - shrinkSpeed, transform.localScale.z - shrinkSpeed);
            if(transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 20) / 1.5f, transform.position.z);
    }
    
}
