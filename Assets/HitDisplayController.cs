using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.Port;

public class HitDisplayController : MonoBehaviour
{
    private Image image;
    private float opacity;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        var color = image.color;
        color.a = 0f;
        opacity = 0f;
        image.color = color;
        EventManager.OnHurt += OnHit;
        EventManager.OnDie += OnDie;
        image.enabled = false;

    }

    void OnDie()
    {
        image.enabled = false;

    }
    // Update is called once per frame
    void OnHit()
    {
        image.enabled = true;
        StopAllCoroutines();
        Cursor.lockState = CursorLockMode.None;
        var color = image.color;
        color.a = 0f;
        opacity = 0f;
        image.color = color;
        StartCoroutine(MoreOpacity(opacity));
    }
    private IEnumerator MoreOpacity(float opacity)
    {
        opacity += 0.2f;
        var color = image.color;
        color.a = opacity;
        this.image.color = color;
        yield return new WaitForSeconds(0.05f);
        if (opacity < 0.8)
        {
            StartCoroutine(MoreOpacity(opacity));
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(LessOpacity(opacity));
        }
    }
    private IEnumerator LessOpacity(float opacity)
    {
        opacity -= 0.1f;
        var color = image.color;
        color.a = opacity;
        this.image.color = color;
        yield return new WaitForSeconds(0.1f);

        if (opacity >= 0)
        {
            yield return LessOpacity(opacity);

        }
        if (opacity <= 0)
        {
            image.enabled = false;
            yield return null;

        }
    }


}
