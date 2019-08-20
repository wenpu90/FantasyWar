using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    private static TargetIndicator instance;
    public static TargetIndicator GetInstance => instance;
    public GameObject target;
    public enum SelectState {  SelectEnemy, SelectPlayer }
    public SelectState st = SelectState.SelectEnemy;

    public enum SelectionPhase { SelectingCharacter, SelectingAction, SelectingEnemyTarget}
    public SelectionPhase sp = SelectionPhase.SelectingCharacter;
    public BattleSimulation bs;

    public int curTargetEnemyNumber = 0;
    public int curTargetPlayerNumber = 0;

    public GameObject battleUI;
    
    void Start()
    {
        st = SelectState.SelectPlayer;
        bs = BattleSimulation.GetInstance;
        target.SetActive(true);
        SetIndicatorPosition(st == SelectState.SelectEnemy ? true : false);
    }


    void Update()
    {
        SwitchSelectionInput();

    }

    public void SwitchSelectionInput()//偵測切換對象的輸入
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectCharacter();
        }
        if (sp != SelectionPhase.SelectingCharacter) return;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SwitchTarget(false, st == SelectState.SelectEnemy ? true : false);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SwitchTarget(true, st == SelectState.SelectEnemy ? true : false);
        }

    }
    
    public void SwitchTarget(bool _UpOrDown, bool _PlayerOrEnemy)//切換選擇中對象
    {
        switch (_PlayerOrEnemy)
        {
            case true:
                curTargetEnemyNumber = _UpOrDown ? (curTargetEnemyNumber + 1) % (bs.enemies.Count) : curTargetEnemyNumber - 1;
                if (curTargetEnemyNumber < 0)
                {
                    curTargetEnemyNumber = bs.enemies.Count - 1;
                }
                break;

            case false:
                curTargetPlayerNumber = _UpOrDown ? (curTargetPlayerNumber + 1) % (bs.players.Count) : curTargetPlayerNumber - 1;
                if (curTargetPlayerNumber < 0)
                {
                    curTargetPlayerNumber = bs.players.Count - 1;
                }
                break;
        }
        SetIndicatorPosition(_PlayerOrEnemy);

    }
    void SetIndicatorPosition(bool _PlayerOrEnemy)//設置坐標指針位置
    {
        transform.position = _PlayerOrEnemy ? bs.enemies[curTargetEnemyNumber].transform.position : bs.players[curTargetPlayerNumber].transform.position;
    }
    void SelectCharacter()//選擇角色
    {
        battleUI.SetActive(true);
        sp = SelectionPhase.SelectingAction;
    }


}
