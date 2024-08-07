using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Simple slider display and difficulty setter
public class SliderDisplay : MonoBehaviour
{
    TMPro.TextMeshProUGUI difficulty;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void ChangeValue()
    {
        int sliderValue = (int)GetComponent<Slider>().value;
        difficulty.text =""+ sliderValue;
        Difficulty.lampCount = sliderValue;
    }
}
