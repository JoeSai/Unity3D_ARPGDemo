using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BearInitial : MonoBehaviour
{

    private Material material;

    private Renderer[] renderers;
    public Material[] PhaseInAndOut = new Material[18];
    private int materialNum = 0;

    void Start()
    {
        renderers = this.GetComponentsInChildren<Renderer>();

        foreach (var item in renderers)
        {
            //Debug.Log(item.material.name);
            if (item.material.name == "PhaseInAndOut2 (Instance)")
            {
                PhaseInAndOut[materialNum] = item.material;
                materialNum++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            //renderers[1].material.SetFloat("_AlphaValue", 1);

            foreach (var item in PhaseInAndOut)
            {
                item.DOFloat(-1, "_AlphaValue", 2.3f);
            }

          
        }

    }
}
