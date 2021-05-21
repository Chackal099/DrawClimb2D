using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOfTheLine : MonoBehaviour
{
    public GameObject LinePref;
    public GameObject LineAxis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            LinePref.transform.Rotate(new Vector3(0, 0, 100 * Time.deltaTime));
            LineAxis.transform.Rotate(new Vector3(0, 0, 100 * Time.deltaTime));
    }
}
