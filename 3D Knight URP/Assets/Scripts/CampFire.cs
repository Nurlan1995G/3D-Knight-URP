using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : Interactable
{
    public override void Interact(GameObject subject)    //реализовали абстрактный класс из Interactable
    {
        Player.Instance.Heal();
        Player.Instance.Replenishment();
    }
}
