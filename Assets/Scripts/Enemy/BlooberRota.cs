using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlooberRota : MonoBehaviour
{
    private Image Blood;
    void Start()
    {
        Blood = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Blood.rectTransform.rotation = Camera.main.transform.rotation;
    }
}
