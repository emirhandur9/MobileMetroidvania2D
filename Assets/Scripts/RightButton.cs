using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    PlayerController player;
    public void OnPointerDown(PointerEventData eventData)
    {
        player.xInput = 1;
        
        if(player.transform.rotation.y == 0)
        {
            //saða bakýyo bi þey yapma
        }
        else
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
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
