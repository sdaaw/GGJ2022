using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSwitchEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Material> materialList = new List<Material>();
    private List<Material> runtimeMaterialList = new List<Material>();

    public GameObject cylinder;

    private Material currMaterial;

    private int idx;

    public float switchSpeed;

    private bool isSwitching;

    public float timeBetweenSwitches;

    void Start()
    {
        currMaterial = cylinder.GetComponent<Renderer>().material;
        Color color = new Color(1, 1, 1, 1);
        foreach (Material m in materialList)
        {
            Material m0 = new Material(m);
            runtimeMaterialList.Add(m0);
            m0.SetColor("_BaseColor", color);
        }
        cylinder.GetComponent<Renderer>().material = materialList[0];
        materialList[idx].SetColor("_BaseColor", new Color(materialList[idx].color.r, materialList[idx].color.g, materialList[idx].color.b, 1));
        StartCoroutine(switchCylinder());
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        float offset = Time.time * 0.5f;
        runtimeMaterialList[idx].mainTextureOffset = new Vector2(offset, offset);
    }

    IEnumerator switchCylinder()
    {
        isSwitching = true;
        //materialList[idx].SetColor("_BaseColor", new Color(materialList[idx].color.r, materialList[idx].color.g, materialList[idx].color.b, 1));
        for (float i = 0; i < 1; i += 0.01f)
        {
            Color color = new Color(1, 1, 1, 1 - i);
            runtimeMaterialList[idx].SetColor("_BaseColor", color);
            yield return new WaitForSeconds(switchSpeed);
        }
        if (idx == materialList.Count - 1)
        {
            idx = 0;
        } else
        {
            idx++;
        }
        cylinder.GetComponent<Renderer>().material = runtimeMaterialList[idx];
        currMaterial = runtimeMaterialList[idx];
        for (float i = 0; i < 1; i += 0.01f)
        {
            Color color = new Color(1, 1, 1, 0 + i);
            runtimeMaterialList[idx].SetColor("_BaseColor", color);
            yield return new WaitForSeconds(switchSpeed);
        }
        isSwitching = false;
        yield return new WaitForSeconds(timeBetweenSwitches);
        StartCoroutine(switchCylinder());
    }
}
