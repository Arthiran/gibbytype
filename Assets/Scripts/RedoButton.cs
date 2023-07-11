using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RedoButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject RedoIndicator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        RedoIndicator.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Hmmm");
        RedoIndicator.SetActive(false);
    }
}
