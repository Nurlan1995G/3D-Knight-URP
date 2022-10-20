using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{
    [SerializeField] private Interactable focus;
    [SerializeField] private float agreRadius = 10f;

    public Quest quest;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,agreRadius);
    }


    protected override void Update()
    {
        base.Update();
        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);
        if (distance < agreRadius)
        {
            if (focus == null)
            {
                SetFocus(Player.Instance.GetComponent<Interactable>());
            }
            else if (focus != null)
            {
                RemoveFocus();
            }
        }
    }

    private void SetFocus(Interactable newFocus) // заполнение
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDeFocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.OnFocused(gameObject);
    }

    private void RemoveFocus()  //удаление фокуса
    {
        if(focus != null)
            focus.OnDeFocused();

        focus = null;
        motor.StopFollowingTarget();
    }

    protected override void Die()
    {
        if(quest != null)
            quest.OnEnemyDead();

        Destroy(gameObject);
        //animator.SetAnimState(AgentAnimator.AnimStates.Death);
    }
}
