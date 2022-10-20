using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour  //взаимодействие
{
    public float interactRadius = 2f;
    protected bool isFocus = false;
    private GameObject subject;

    bool hasInteracted = false;   //взаимодействовал, изначально нет

    public abstract void Interact(GameObject subject);

    protected virtual void Update()
    {
        if (isFocus == true && !hasInteracted)
        {
            float distance = Vector3.Distance(transform.position, subject.transform.position);
            if (distance <= interactRadius)
            {
                Interact(subject);
                hasInteracted = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }

    public void OnFocused(GameObject newSubject)
    {
        isFocus = true;
        subject = newSubject;
        hasInteracted = false;
    }

    public void OnDeFocused()  //пропадаем из его вида,фокусировки
    {
        isFocus = false;
        subject = null;
        hasInteracted = false;
    }
}
