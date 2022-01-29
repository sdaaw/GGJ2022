using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSwitchEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Material> materialList = new List<Material>();

    public GameObject cylinder;

    private Material currMaterial;

    private int idx;

    public float switchSpeed;

    private bool isSwitching;

    void Start()
    {
        cylinder.GetComponent<Renderer>().material = materialList[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (isSwitching) return;
            StartCoroutine(switchCylinder());
        }
    }

    IEnumerator switchCylinder()
    {
        isSwitching = true;
        for (float i = 1; i > 0; i -= 0.01f)
        {
            materialList[idx].color = new Color(materialList[idx].color.r, materialList[idx].color.g, materialList[idx].color.b, i);
            yield return new WaitForSeconds(switchSpeed);
        }
        idx++;

        for (float i = 0; i < 1; i += 0.01f)
        {
            materialList[idx].color = new Color(materialList[idx].color.r, materialList[idx].color.g, materialList[idx].color.b, i);
            yield return new WaitForSeconds(switchSpeed);
        }
        isSwitching = false;
    }
}
