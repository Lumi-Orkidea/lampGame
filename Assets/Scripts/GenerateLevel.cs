using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GenerateLevel : MonoBehaviour
{
    [SerializeField] GameObject lamp; //the gameObject we will be generating
    [SerializeField] GameObject camera;
    [SerializeField] Canvas victoryCanvas;
    public List<GameObject> lamps;
    public GameObject lampsObject;
    public bool allGen=false;
    int gridSize;
    List<float> xCoords = new List<float>(); //collect the X and ...
    List<float> yCoords = new List<float>(); //y coordinates to calculate the width and height of the level for camera centering
    
    void Start()
    {
        Generate();
    }
    //Generation for buttons
    void OnMouseDown()
    {
        Generate();
    }

    //current field width -3 to 9
    //Height: 2.5 to 8
    //Generate a puzzle
    public void Generate()
    {
        DestroyAll(); //Destroy any existing puzzle
        victoryCanvas.enabled = false; //disable the winscreen in case it's active
        if(Difficulty.lampCount == 0) Difficulty.lampCount = 10;
        gridSize = Difficulty.lampCount;
        int i = 0;
        Vector3 firstCoords = new Vector3(Mathf.Round(Random.Range(1, 5)), Mathf.Round(Random.Range(3, 6)), 1.5f); //Randomize the position of the first lamp in the constraints of the level
        while (i < gridSize)
        {
            float xNew = (Mathf.Round(Random.Range(-1.4f, 1.4f))); //randomise each X...
            float yNew = (Mathf.Round(Random.Range(-1.4f, 1.4f))); //... and y
            if (yNew == 0 && xNew == 0) xNew = 1; //Make sure we are not creating a lamp on the exact same position as the last one
            firstCoords.x += xNew;
            firstCoords.y += yNew;
            var newLamp = (Instantiate(lamp, firstCoords, Quaternion.identity, lampsObject.transform.GetChild(1)));
            newLamp.name = "lamp" + i; //this is for the editor so each lamp has different name
            CleanInstantiateCheck(newLamp); //check that the lamp didn't spawn on the same position as any currently existing lamp
            lamps.Add(newLamp);
            xCoords.Add(newLamp.transform.position.x);
            yCoords.Add(newLamp.transform.position.y);

            i++;
        }
        allGen=true;
        CameraCentering();
    }

    //Checks that the lamps don't spawn on top of one another.
    //lamp = the lamp we are checking
    void CleanInstantiateCheck(GameObject lamp)
    {
        Collider[] overlappingHitboxes = Physics.OverlapBox(lamp.transform.position, lamp.transform.localScale / 2);
        if (overlappingHitboxes.Length > 0)
        {
           // Debug.Log("Moving from:" + lamp.transform.position);
            Vector3 spawnFix = new Vector3(Roll(-1.4f, 1.4f), Roll(-1.4f, 1.4f)); //adjust the lamp position according to the randomized fix.
            bool error = true;
            while (error)
            {
                if (spawnFix.x == 0 && spawnFix.y == 0) spawnFix = new Vector3(Roll(-1.4f, 1.4f), Roll(-1.4f, 1.4f)); 
                error = false;
                
                if (Physics.Raycast(lamp.transform.position, spawnFix, 1.5f)) //raycast to see if the fixed position is already occupied
                {
                    //Debug.Log("taken");
                    spawnFix = new Vector3(Roll(-1.4f, 1.4f), Roll(-1.4f, 1.4f)); //Roll a new spawnfix if the previous one was occupied
                    error = true;
                }
                lamp.transform.position += spawnFix;
                xCoords.Add(lamp.transform.position.x);
                yCoords.Add(lamp.transform.position.y);
            }
            
            //Debug.Log("TO:" + lamp.transform.position);
            overlappingHitboxes = new Collider[0];
        }

    }
    //destroys all existing lamps
    void DestroyAll()
    {
        Destroy(lampsObject.transform.GetChild(0).gameObject);
        lampsObject.transform.GetChild(0).gameObject.SetActive(false);
        GameObject newList = new GameObject("list");
        newList.transform.parent = lampsObject.transform;
        xCoords.Clear();
        yCoords.Clear();
    }

    float Roll(float min, float max)
    {
        return Mathf.Round(Random.Range(min, max));
    }

    //Centers the camera according to the puzzle coordinations
    void CameraCentering()
    {
        float minX = xCoords.Min();
        float maxX = xCoords.Max();
        float minY = yCoords.Min();
        float maxY = yCoords.Max();

        Debug.Log(minX);
        Debug.Log(maxX);

        float averageX = (minX + maxX) / 2;
        float averageY = (minY + maxY) / 2;
        float cameraZ = camera.transform.position.z;
        camera.transform.position = new Vector3(averageX, averageY, cameraZ);
        

    }



}
