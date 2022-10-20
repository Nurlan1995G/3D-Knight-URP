using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static void DeleteAllSavings()  //удалить все сбережения
    {
        PlayerPrefs.DeleteAll();  //удаляет все сохранения
        //PlayerPrefs.DeleteKey("Gim");  //удаляет опеределенный ключ,который записали
        //PlayerPrefs.SetInt("Health Gim", 20);  // для записи сохранения здоровья,например игроку

    }

    public static void SetPlayerPosition(Vector3 position)   //сохраняем позицию игрока по трем осям xyz
    {
        PlayerPrefs.SetFloat("PlayerPositionX", position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", position.z);

    }

    public static Vector3 GetPlayerPosition()  //получать позицию     //вызов класса в Player Awake
    {
        if (PlayerPrefs.HasKey("PlayerPositionX"))   //проверка если ключ есть
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

    public static void SetDoneQuests(List<Quest> quests)  //сохранять квест,записывать
    {
        foreach (var quest in quests)
            PlayerPrefs.SetInt($"Quest {quest.id}", 1);
    }
}
