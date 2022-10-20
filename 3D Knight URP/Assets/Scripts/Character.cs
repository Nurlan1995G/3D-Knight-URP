using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentMotor))]
public abstract class Character : Interactable
{
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float currentHealth;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] protected bool canAttack = true;
    protected AgentAnimator animator;
    protected AgentMotor motor;


    private void Start()
    {
        motor = GetComponent<AgentMotor>();
        currentHealth = maxHealth;
    }

    public override void Interact(GameObject subject)  //через этот метод мы будем атаковать. Чтобы сделать персонажа который
                                                       //не атакует,делаем
                                                      //отделный класс AttackCharacter
    {
        StartCoroutine(OnInteracting(subject));
    }

    private IEnumerator OnInteracting(GameObject subject)
    {
        var character = subject.GetComponent<Character>();
        if (character != null)
        {
            if (character.canAttack == true)
            {
                while (isFocus == true && subject != null)
                {
                    if (Vector3.Distance(transform.position, subject.transform.position) <= interactRadius)
                    {
                        print($"{subject.name} бьет {gameObject.name}");
                        TakeDamage(character.damage);
                        character.Attack();
                        yield return new WaitForSeconds(character.attackSpeed);
                    }
                    yield return null;
                }
            }
        }
    }

    public void Attack()
    {
        motor.StartAttack(attackSpeed);
    }

    

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        print($"Здоровье: {gameObject.name}  {currentHealth}");
        if (currentHealth <= 0)
            Die();
    }

    protected abstract void Die();  //метод умирать
}
