using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTween : MonoBehaviour
{

    [SerializeField]
    GameObject backPanel, colorWheel, colorWheel02, colorWheel03;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateAround(colorWheel, Vector3.forward, -360f, 10f).setLoopClamp();
        LeanTween.rotateAround(colorWheel02, Vector3.forward, 360f, 10f).setLoopClamp();
        LeanTween.rotateAround(colorWheel03, Vector3.forward, -360f, 5f).setLoopClamp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
