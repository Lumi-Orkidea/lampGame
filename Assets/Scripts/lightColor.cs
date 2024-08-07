using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//change the color of the lamp by pressing the different color buttons
public class lightColor : MonoBehaviour
{

    void OnMouseDown()
    {
        Difficulty.customLamp = true;
        Difficulty.lampMat = GetComponent<MeshRenderer>().material;
    }
}
