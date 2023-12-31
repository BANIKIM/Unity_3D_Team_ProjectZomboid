using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp; //바위체력
    [SerializeField]
    private float destroyTime; //파면 제거 시간
    [SerializeField]
    private SphereCollider COL; //구체 콜라이더 

    //필요한 게임 오브젝트 
    [SerializeField]
    private GameObject go_rock; //일반 바위
    [SerializeField]
    private GameObject go_debris; //깨진 바위 
    [SerializeField]
    private GameObject go_effect_Prefabs; //채굴 이펙트 
    [SerializeField]
    private GameObject go_item_Prefabs;

    [SerializeField]
    private int count;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip effectClip1;
    [SerializeField]
    private AudioClip effectClip2;

    public void Mining()
    {
        audioSource.clip = effectClip1;
        // audioSource.Play();
        MusicController.instance.PlaySFXSound("Rock_Hit");
        var clone = Instantiate(go_effect_Prefabs, COL.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);

        hp--;
        if (hp<=0)
        {
            Destruction();
        }
    }
    private void Destruction()
    {
        audioSource.clip = effectClip2;
        // audioSource.Play();
        MusicController.instance.PlaySFXSound("Rock_Broken");
        COL.enabled = false;
        for (int i = 0; i < count; i++)
        {
            Instantiate(go_item_Prefabs, go_rock.transform.position, Quaternion.identity);
        }
      /*  Instantiate(go_item_Prefabs, go_rock.transform.position, Quaternion.identity);
        Instantiate(go_item_Prefabs, go_rock.transform.position, Quaternion.identity);
        Instantiate(go_item_Prefabs, go_rock.transform.position, Quaternion.identity);
        Instantiate(go_item_Prefabs, go_rock.transform.position, Quaternion.identity);*/

        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            Mining();
        }
    }
}
