using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    //�⺻���Ȱ� �߰����ȵ��� ����ؼ� ���� ������ ����ϴ� ������ ����
    //�ٵ� ������ �׳� �⺻���ݸ�

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
        // todo:������ �⺻�ɷ�ġ�� ���������, �����δ� �ɷ�ġ ��ȭ����� ����ȴ�.
        Currentstat.statsChangeType = baseStat.statsChangeType;
        Currentstat.maxHealth = baseStat.maxHealth;
        Currentstat.speed = baseStat.speed;
    }
}