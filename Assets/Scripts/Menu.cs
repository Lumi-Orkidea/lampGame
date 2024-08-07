using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    bool playActive = true;
    Dictionary<int, bool> menut = new Dictionary<int, bool>();
    int activeMenu = 0;

    // Start is called before the first frame update
    void Start()
    {
        menut.Add(0, true);
        int i = 1;
        while (i < transform.childCount)
        {
            menut.Add(i, false);
            i++;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        ToggleCurrentMenu();
        ToggleMenu(0);
        activeMenu = 0;
    }

    public void Play()
    {
        ToggleCurrentMenu();
        ToggleMenu(1);
        activeMenu = 1;
    }
    public void Shop()
    {
        ToggleCurrentMenu();
        ToggleMenu(2);
        activeMenu = 2;
    }

    public void Instructions()
    {
        ToggleCurrentMenu();
        ToggleMenu(3);
        activeMenu = 3;
    }

    private void ToggleMenu(int menu)
    {
        bool newValue = menut[menu] = !menut[menu];
        transform.GetChild(menu).gameObject.SetActive(newValue);
        
    }

    private void ToggleCurrentMenu()
    {
        transform.GetChild(activeMenu).gameObject.SetActive(!playActive);
        menut[activeMenu] = !menut[activeMenu];
    }
}
