using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentAnimator))]
public class AgentMotor : MonoBehaviour
{
    private NavMeshAgent agent;
    private AgentAnimator animator;
    private Transform target;
    private bool isReplenish = false;   //восполнение здоровья
    private bool isAttacking = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<AgentAnimator>();   
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            LookAtTarget();
        }
        if (isReplenish == false && isAttacking == false)  //если у нас не проигрывается анимация восполнения здоровья,
                                                           //то проигрывается другая аним
        {
            if (agent.velocity.magnitude < agent.speed * 0.2f)
                animator.SetAnimState(AgentAnimator.AnimStates.Idle);
            else
                animator.SetAnimState(AgentAnimator.AnimStates.Walk);
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.interactRadius;
        agent.updateRotation = false;

        target = newTarget.transform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        target = null;
    }

    private void LookAtTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }

    public void StartReplenishment()
    {
        StartCoroutine(Replenishment());
    }

    private IEnumerator Replenishment()
    {
        isReplenish = true;
        animator.SetAnimState(AgentAnimator.AnimStates.Replenishment);
        yield return new WaitForSeconds(5f);
        isReplenish = false;
    }

    public void StartAttack(float attackSpped)
    {
        StartCoroutine(Attack(attackSpped));
    }

    private IEnumerator Attack(float attackSpeed)
    {
        isAttacking = true;
        animator.SetAnimState(AgentAnimator.AnimStates.Attack);
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false; 
    }
}
