using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//TP2 - Matias Sueldo
public class DieImageScript : MonoBehaviour
{
    private Image image;
    private float opacity;
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnEnable()
    {


        Cursor.lockState = CursorLockMode.None;
        image = GetComponent<Image>();
        text = GetComponent<TMP_Text>();
        
        if (image != null)
        {
            var color = image.color;
            color.a = 0f;
            opacity = 0f;
            image.color = color;
            StartCoroutine(Opacity(opacity));

        }
        else
        {
            var color = text.color;
            color.a = 0f;
            opacity = 0f;
            text.color = color;
            StartCoroutine(OpacityText(opacity));

        }

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Opacity(float opacity)
    {
        opacity += 0.1f;
        var color = image.color;
        color.a = opacity;
        this.image.color = color;
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(Opacity(opacity));
    }
    private IEnumerator OpacityText(float opacity)
    {
        opacity += 0.5f;
        var color = text.color;
        color.a = opacity;
        this.text.color = color;
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(Opacity(opacity));
    }

}
