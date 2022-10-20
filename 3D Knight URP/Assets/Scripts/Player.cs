using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Player : Character
{
    private Camera mainCamera;
    [SerializeField] private LayerMask movableMask;
    [SerializeField] private Interactable focus;

    public static Player Instance;   //синглтон предмета(паттерн) - должен быть единственным

    [SerializeField] private GameObject swordObject;
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private int coinsCount;
    float multipliedMaxHealth = 2f;    //максимальное здоровье умноженое на 2
  

    private void Awake()
    {
        mainCamera = Camera.main;
        Instance = this;  //сылается на игрока плауер.this-ссылка
        canAttack = false;
        transform.position = SaveSystem.GetPlayerPosition();  
    }


    protected override void Update()
    {
        base.Update();    //все данные с Interactable с Update теперь будут и тут
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, movableMask))
            {
                motor.MoveToPoint(hit.point);
                RemoveFocus();
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                var interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
            SaveSystem.SetPlayerPosition(transform.position);
    }

    private void SetFocus(Interactable newFocus)  //правая клавиша мышки, идем к объекту
    {
        if(newFocus != focus)  //если мы взаимодействуем с одним объектом и начали взаимодействовать с другим,то с прежним 
                                // мы уже не взаимодействуем
        {
            if (focus != null)
                focus.OnDeFocused();

            focus = newFocus;    
            motor.FollowTarget(newFocus);

        }
        newFocus.OnFocused(gameObject);
    }

    private void RemoveFocus()  //левая клавиша мышки, идем по указанной точке
    {
        if (focus != null)
            focus.OnDeFocused();

        focus = null;
        motor.StopFollowingTarget();  //перестаем следовать за нашей целью
    }

    public void Heal()
    {
        currentHealth = maxHealth;
    }

    public void Replenishment() //восполнение
    {
        motor.StartReplenishment();
    }

    public void ActivateSword()  // тут происходит активация меча
    {
            canAttack = true;  // тут булевая переменная, есть атака или нет
            swordObject.SetActive(true);   //Активация меча
    }

    public void ActivateShield()
    {
        maxHealth *= multipliedMaxHealth;
        shieldObject.SetActive(true);
    }

    protected override void Die()
    {
        Time.timeScale = 0;   //таким способ мы останавливаем время
        animator.SetAnimState(AgentAnimator.AnimStates.Death);
    }

    //public void ActivateSpear()
    // {
    // if (hand_freeLeft == true)
    //     spearObject.SetActive(true);
    // else
    //   spearObject.SetActive(false);
    // }

    public void AddCoins(int coins)
    {
        coinsCount += coins;
        print($"Вы получили {coinsCount} монеты");
    }
}
