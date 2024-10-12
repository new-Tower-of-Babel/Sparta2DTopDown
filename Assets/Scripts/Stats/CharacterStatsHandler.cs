using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    //기본스탯과 추가스탯들을 계산해서 최종 스탯을 계산하는 로직이 있음
    //근데 지금은 그냥 기본스텟만

    [SerializeField] private CharacterStat baseStat;
    public CharacterStat Currentstat { get; private set; }
    public List<CharacterStat> statModifiers = new List<CharacterStat>();
    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        AttackSO attackSO = null;
        if(baseStat.attackSO != null)
        {
            attackSO = Instantiate(baseStat.attackSO);
        }
        Currentstat = new CharacterStat { attackSO = attackSO };
        // todo:지금은 기본능력치만 적용되지만, 앞으로는 능력치 강화기능이 적용된다.
        Currentstat.statsChangeType = baseStat.statsChangeType;
        Currentstat.maxHealth = baseStat.maxHealth;
        Currentstat.speed = baseStat.speed;
    }
}