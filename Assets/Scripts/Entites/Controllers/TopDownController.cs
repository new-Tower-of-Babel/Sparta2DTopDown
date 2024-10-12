using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent; // Action은 무조건 void만 반환해야 아니면 Func
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;

    protected bool IsAttacking {  get; set; }

    private float timeSinceLastAttack = float.MaxValue;
    
    //protected 프로퍼티를 한 이유 : 나만 바꾸고싶지만 가져가는건 내 상속받는 클래스들도 볼수있게!
    protected CharacterStatsHandler stats { get;private set; }
    protected virtual void Awake()
    {
        stats = GetComponent<CharacterStatsHandler>();
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (timeSinceLastAttack<=stats.Currentstat.attackSO.delay)
        {
            timeSinceLastAttack+=Time.deltaTime;
        }
        else if(IsAttacking && timeSinceLastAttack >= stats.Currentstat.attackSO.delay)
        {
            timeSinceLastAttack = 0f;
            CallAttackEvent(stats.Currentstat.attackSO);
        }
    }


    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction); // ?. 없으면 말고 있으면 실행
    }
    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    } 
    private void CallAttackEvent(AttackSO attackSO)
    {
        OnAttackEvent?.Invoke(attackSO);
    }
}
