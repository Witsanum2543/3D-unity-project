using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 15f * Time.deltaTime, 0);
    }
}
