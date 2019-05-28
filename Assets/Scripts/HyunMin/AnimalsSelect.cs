using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalsSelect : MonoBehaviour
{
    private GameObject animalsInfo;
    private Image image;
    private readonly Color initColor = new Vector4(0.6f, 0.0f, 0.0f, 0.0f);

    void Start()
    {
        animalsInfo = GameObject.FindGameObjectWithTag("animalsInfo");
        image = GetComponent<Image>();
        image.color = initColor;
    }

    private void OnMouseDown()
    {
        image.color = new Vector4(1.0f, 1f, 1f, 1f);
    }
}
