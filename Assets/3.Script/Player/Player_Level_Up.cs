using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Level_Up : MonoBehaviour
{
    int level = 0;

    //���������
    public void Level_up(GameObject[] gameObjects)
    {

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].transform.GetChild(1).gameObject.activeSelf == true)
            {

            }
            else
            {
                gameObjects[i].transform.GetChild(1).gameObject.SetActive(true);
                break;
            }
        }         
    }

    //����üũ���
    public int Level_check(GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].transform.GetChild(1).gameObject.activeSelf == true)
            {

            }
            else
            {
                level = i;
                break;                               
            }
        }
        return level;
    }
}
