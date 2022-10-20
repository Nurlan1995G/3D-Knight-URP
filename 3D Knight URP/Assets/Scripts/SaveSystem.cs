using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static void DeleteAllSavings()  //������� ��� ����������
    {
        PlayerPrefs.DeleteAll();  //������� ��� ����������
        //PlayerPrefs.DeleteKey("Gim");  //������� ������������� ����,������� ��������
        //PlayerPrefs.SetInt("Health Gim", 20);  // ��� ������ ���������� ��������,�������� ������

    }

    public static void SetPlayerPosition(Vector3 position)   //��������� ������� ������ �� ���� ���� xyz
    {
        PlayerPrefs.SetFloat("PlayerPositionX", position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", position.z);

    }

    public static Vector3 GetPlayerPosition()  //�������� �������     //����� ������ � Player Awake
    {
        if (PlayerPrefs.HasKey("PlayerPositionX"))   //�������� ���� ���� ����
        {
            return new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"),
                PlayerPrefs.GetFloat("PlayerPositionY"),
                PlayerPrefs.GetFloat("PlayerPosi tionZ"));
        }
        else
        {
            return new Vector3(0f, 0.43f, 15f); 
        }
    }

    public static void SetDoneQuests(List<Quest> quests)  //��������� �����,����������
    {
        foreach (var quest in quests)
            PlayerPrefs.SetInt($"Quest {quest.id}", 1);
    }
}
