using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TP2 - Geronimo Gorriarena
public class FallingRockGeneratorController : MonoBehaviour
{
    List<Transform> rockGenerators;
    public GameObject myPrefab;
    private bool isGeneratingRock;
    int generator=0 ;
    bool isSumming;
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
            StartCoroutine(generateRock(rockGenerators[generator]));
            
            if(generator==3)
            {
                isSumming =false;
            }
            if (generator==0) {
                isSumming = true;
            }
            if (isSumming)
            {
                generator++;
            }
            else
            {
                generator--;
            }
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
