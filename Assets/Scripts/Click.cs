using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public GameObject person;

    //Просто кликаем на землю
    public void Clicking() {
        person.GetComponent<PersonMoving>().clicking = 1;
    }

    public void Exit() {
        Application.Quit();
    }
}
