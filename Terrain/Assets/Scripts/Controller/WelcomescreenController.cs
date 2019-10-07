using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WelcomescreenController : MonoBehaviour, IPointerClickHandler
{
    public GameObject disappearing;
    public GameObject appearing;

    public void OnPointerClick(PointerEventData pointerEventData) {
        disappearing.SetActive(false);
        appearing.SetActive(true);
    }
}
