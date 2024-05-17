using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FallingRockGeneratorController : MonoBehaviour
{
    List<Transform> rockGenerators;
    public GameObject myPrefab;
    private bool isGeneratingRock;
    // Start is called before the first frame update
    void Start()
    {
        rockGenerators = GetComponentsInChildren<Transform>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGeneratingRock)
        {
            StartCoroutine(generateRock(rockGenerators[Random.Range(1, 4)]));
        }
    }

    IEnumerator generateRock(Transform rockGenerator)
    {
        isGeneratingRock = true;
        yield return new WaitForSeconds(3f);
        Instantiate(myPrefab, rockGenerator);

        isGeneratingRock = false;

    }
}
