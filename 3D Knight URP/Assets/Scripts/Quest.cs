using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int id;
    new public string name = "Test Quest";
    [Multiline] public string description = "Do Something!";   //многострочневое описание "сделай что-нибудь!"
    public int coinsReward = 10;
    public List<GameObject> enemies;
    private int enemyCount;  //для чтобы мы уведили когда нашев врагов не будет в наличии
    public bool isDone = false;  // выполнен ли наш квест
    private bool isActive = false;  // активен ли наш квест

    private void Start()
    {
        enemyCount = enemies.Count;
    }

    public void StartQuest()
    {
        if(isActive == false)
        {
            QuestSystem.Instance.activeQuest.Add(this);   //переход из QuestSystem   список активных квестов
            float areaLenght = 5f;
            foreach(var enemy in enemies)
            {
                float posX = transform.position.x + (areaLenght / 2) * Random.Range(-1,1);
                float posZ = transform.position.z + (areaLenght / 2) * Random.Range(-1, 1);
                var newEnemy = Instantiate(enemy, new Vector3(posX, transform.position.y, posZ), Quaternion.identity);  //для того отслеживать умер ли враг
                newEnemy.GetComponent<Enemy>().quest = this;
            }
            isActive = true;
            print($"Начался квест {name}");
            print($"Описание: {description}");
            print($"Награда {coinsReward} монет");
        }
    }

    public void OnEnemyDead()
    {
        enemyCount--;
        if(enemyCount <= 0)
        {
            QuestDone();
        }
    }

    private void QuestDone()   //выпоненый квест
    {
        Player.Instance.AddCoins(coinsReward);
        isDone = true;
        isActive = false;
        QuestSystem.Instance.activeQuest.Remove(this);   //убирается из списка активных квестов
        QuestSystem.Instance.doneQuest.Add(this);    //добавится в список выполненых квестов
        print("Кветс выполнен");
    }
}
