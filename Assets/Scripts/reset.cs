using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reset : MonoBehaviour {

    private int victory = 100; //how many lamps need to be lighted to complete the level
    [SerializeField] GameObject victoryCanvas;
    private int points = 0; //how many lamps are currently lit
    [SerializeField] GameObject lamps;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(victoryCount(1f)); 
    }  

    //wait till the lights are generated before counting the required points
    IEnumerator victoryCount(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        victory = lamps.transform.GetChild(0).childCount;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            points = 0;
            for (int i = 0; i < lamps.transform.GetChild(0).childCount; i++)
            {
                if (lamps.transform.GetChild(0).GetChild(i).GetComponent<LampBox>().getStatus()) points += 1;
                else break;
            }
            if (points >= victory) victoryCanvas.GetComponent<Canvas>().enabled=true;
        }
	}
    void OnMouseDown()
    {
        Reset();
    }
    //reset the points and put all lights off.
    public void Reset()
    {
        points = 0;
        victoryCanvas.GetComponent<Canvas>().enabled = false;
        
        for (int i = 0; i < lamps.transform.GetChild(0).childCount; i++)
        {
            lamps.transform.GetChild(0).GetChild(i).gameObject.GetComponent<LampBox>().reset();
        }
    }

    
}
