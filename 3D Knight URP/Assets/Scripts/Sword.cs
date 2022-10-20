using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Interactable
{
    public override void Interact(GameObject subject)
    {
        Player.Instance.Replenishment();  //подбор меча и подход к огню тоже самое.Восполнение
        Player.Instance.ActivateSword();  //активация меча
        Destroy(gameObject);
    }
}
