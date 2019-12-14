using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EscapeMenu : MonoBehaviour
{
#pragma warning disable CS0649 // varriable is never assigned to and will always have it's default value
    [SerializeField, Required]
    GameObject menu;
#pragma warning restore CS0649 // varriable is never assigned to and will always have it's default value

    private void Awake()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeInHierarchy == true)
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f;
                menu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                Time.fixedDeltaTime = 0;
                menu.SetActive(true);
            }
        }
    }
}
