using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Banding : MonoBehaviour, IPointerClickHandler//우클릭 마우스 이벤트 쓰기위해서 IPointerClickHandler
{
    [SerializeField] private GameObject Band;
    [SerializeField] private Player_Move player;
    public bool isBanding = false;



    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right&& !isBanding)
        {
            player.animator.SetBool("isMaking", true);
            StartCoroutine(player_anim_co());
        }
        else if(eventData.button == PointerEventData.InputButton.Right && isBanding)
        {
            Band.SetActive(false);
            isBanding = false;
        }
    }

    private IEnumerator player_anim_co()
    {
        yield return new WaitForSeconds(1.5f);
        player.animator.SetBool("isMaking", false);
        Band.SetActive(true);
        isBanding = true;
    }
}
