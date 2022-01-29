using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSwitchEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Material> materialList = new List<Material>();
    private List<Material> runtimeMaterialList = new List<Material>();

    public GameObject cylinder;
    public GameObject cylinder2;



    private int idx;

    public float switchSpeed;


    public float timeBetweenSwitches;

    private Color previousColor;

    void Start()
    {
        Color color = new Color(1, 1, 1, 1);
        foreach (Material m in materialList)
        {
            Material m0 = new Material(m);
            runtimeMaterialList.Add(m0);
            m0.SetColor("_BaseColor", color);
        }
        cylinder.GetComponent<Renderer>().material = materialList[0];
        cylinder2.GetComponent<Renderer>().material = materialList[1];
        materialList[idx].SetColor("_BaseColor", new Color(materialList[idx].color.r, materialList[idx].color.g, materialList[idx].color.b, 1));
        materialList[idx + 1].SetColor("_BaseColor", new Color(materialList[idx].color.r, materialList[idx].color.g, materialList[idx].color.b, 0));
        StartCoroutine(switchCylinder());
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        float offset = Time.time * 0.1f;
        runtimeMaterialList[idx].mainTextureOffset = new Vector2(offset, offset);
        //runtimeMaterialList[idx].SetColor("_EmissionColor", Color.Lerp(GetRandomColor(), GetRandomColor(), 1f));
    }

    private Color GetRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    IEnumerator switchCylinder()
    {
        //materialList[idx].SetColor("_BaseColor", new Color(materialList[idx].color.r, materialList[idx].color.g, materialList[idx].color.b, 1));
        for (float i = 0; i < 1; i += 0.01f)
        {
            Color color = new Color(1, 1, 1, 1 - i);
            Color color2 = new Color(1, 1, 1, 1 + i);
            runtimeMaterialList[idx].SetColor("_BaseColor", color);
            if(idx + 1 == materialList.Count - 1)
            {
                runtimeMaterialList[0].SetColor("_BaseColor", color2);
            } 
            else
            {
                runtimeMaterialList[idx + 1].SetColor("_BaseColor", color2);
            }
            yield return new WaitForSeconds(switchSpeed);
        }
        yield return new WaitForSeconds(timeBetweenSwitches);
        StartCoroutine(switchCylinder());
    }
}
