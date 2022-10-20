using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public List<Quest> doneQuest;   //������ ���������� �������
    public List<Quest> activeQuest; //������ �������� �������

    public static QuestSystem Instance;   //��������     (������� � Quest)

    private void Awake()
    {
        Instance = this;
    }
}
