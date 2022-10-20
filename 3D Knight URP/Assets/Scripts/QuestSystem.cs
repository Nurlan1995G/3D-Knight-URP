using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public List<Quest> doneQuest;   //список выполненых квестов
    public List<Quest> activeQuest; //список активных квестов

    public static QuestSystem Instance;   //синглтон     (переход в Quest)

    private void Awake()
    {
        Instance = this;
    }
}
