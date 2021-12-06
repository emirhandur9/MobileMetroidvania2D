using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    PlayerController player;
    public void OnPointerDown(PointerEventData eventData)
    {
        player.xInput = -1;

        if (player.transform.rotation.y == 180)
        {
            //sola bakýyo bi þey yapma
        }
        else
        {
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.xInput = 0;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

}
