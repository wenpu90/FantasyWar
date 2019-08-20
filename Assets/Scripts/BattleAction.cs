using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAction : MonoBehaviour
{
    public GameObject[] buttons;
    public TargetIndicator ti;

    void Start()
    {
        ti = TargetIndicator.GetInstance;
    }
    public void AttackTarget()//選擇目標
    {

    }
    public void SpellTarget()
    {

    }
    public void ItemTarget()
    {

    }
    public void GuardMode()
    {

    }
    public void DodgeMode()
    {

    }
}
