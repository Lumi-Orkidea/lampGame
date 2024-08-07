using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LampBox : MonoBehaviour {

    bool lightStatus = false; //is the light on or off?
    MeshRenderer renderer;
    [SerializeField] List<GameObject> Neighbours; 
    [SerializeField] Material darkMat; //material displayed when the lamp is off
    [SerializeField] Material lampMat; //material displayed when the lamp is on
    [SerializeField] bool allGen;

    private Collider[] overlappingHitboxes;

    
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        FindNeighbours();
        if(Difficulty.customLamp) lampMat = Difficulty.lampMat; //set the custom material of the lamp if used
    }

    /*
    void CleanInstantiateCheck() //not sure this is in use anymore
    {
        overlappingHitboxes = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2);
        if (overlappingHitboxes.Length > 0)
        {
            transform.position += new Vector3(Mathf.Round(Random.Range(-1.4f, 1.4f)), Mathf.Round(Random.Range(-1.4f, 1.4f)));
            overlappingHitboxes = new Collider[0];   
        }

    }*/

 

    void OnMouseDown() //light this lamp and the neighbours when this lamp is clicked
    {
        toggle();
        foreach (var naapuri in Neighbours)
        {
            naapuri.GetComponent<LampBox>().toggle();
        }
        
    }
    //toggle the lamp on or off
    void toggle()
    {
        if (lightStatus)
        {
            renderer.material = darkMat;
        }
        else renderer.material = lampMat;
        lightStatus = !lightStatus;
    }

    //Find the closest neighbour of a lamp
    void FindNeighbours()
    {
        Vector3[] units = { new Vector3(-1, 0, 0),
                            new Vector3(1, 0, 0),
                            new Vector3(0, -1, 0),
                            new Vector3(0, 1, 0)};
        RaycastHit hit;
        foreach (var unit in units)
        {
            if (Physics.Raycast(transform.position, unit, out hit))
            {
                Neighbours.Add(hit.collider.gameObject); //add the found neighbour to the list of neighbours
            }
        }
    }

    public int voittoCheck()
    {
        if (lightStatus) return 1;
        return -1;
    }

    //set the lamp off
    public void reset()
    {
        renderer.material = darkMat;
        lightStatus = false;
        
    }

    //Testing to draw the self generated overlap boxes
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
    public bool getStatus()
    {
        return lightStatus;
    }
}
