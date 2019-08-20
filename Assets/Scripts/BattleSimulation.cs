using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BattleSimulation : MonoBehaviour
{
    private static BattleSimulation Instance;
    public static BattleSimulation GetInstance => Instance;

    public enum GameState { StandbyPhase, DrawCardPhase, PlayerDecisionPhase, PlayerTurnPhase, EnemyTurnPhase }
    public GameState gameState = GameState.StandbyPhase;

    public List<GameObject> players;
    public List<GameObject> enemies;

    public delegate void BattleSequence();
    public List<BattleSequence> BattleSequence_Event;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //StartCoroutine(AttackMovement());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchGameState(int _state)
    {
        gameState = (GameState)_state;
    }

    public void UseSpecialAttack()
    {
        StartCoroutine(AttackMovement());
    }

    IEnumerator AttackMovement()//insert target number
    {
        int unit = 0;
        while(unit < players.Count)
        {
            Vector3 vec3 = players[unit].transform.position;
            players[unit].transform.DOMove(new Vector3(enemies[unit].transform.position.x + 2, 0.5f, enemies[unit].transform.position.z), 0.5f).SetEase(Ease.OutSine);
            //players[unit].transform.DOJump(new Vector3(enemies[unit].transform.position.x + 1, 0.5f, enemies[unit].transform.position.z), 5, 0, 0.5f, false).SetEase(Ease.InOutQuint);
            yield return new WaitForSeconds(1);
            players[unit].transform.DOMove(vec3, 0.5f).SetEase(Ease.OutSine);
            yield return new WaitForSeconds(2);
            unit++;
        }

    }
}
