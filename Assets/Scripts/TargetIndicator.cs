using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public GameObject target;
    public enum SelectState {  SelectEnemy, SelectPlayer }
    public SelectState st = SelectState.SelectEnemy;

    public BattleSimulation bs;

    public int curTargetEnemyNumber = 0;
    public int curTargetPlayerNumber = 0;


    void Start()
    {
        bs = BattleSimulation.GetInstance;
        target.SetActive(true);
        StartSelectTarget(st == SelectState.SelectEnemy ? true : false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SwitchEnemyTarget(false, st == SelectState.SelectEnemy ? true : false);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SwitchEnemyTarget(true, st == SelectState.SelectEnemy ? true : false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectTarget();
        }
    }

    public void StartSelectTarget(bool _PlayerOrEnemy)
    {
        SetIndicatorPosition(_PlayerOrEnemy);
    }
    
    public void SwitchEnemyTarget(bool _UpOrDown, bool _PlayerOrEnemy)
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
    void SetIndicatorPosition(bool _PlayerOrEnemy)
    {
        transform.position = _PlayerOrEnemy ? bs.enemies[curTargetEnemyNumber].transform.position : bs.players[curTargetPlayerNumber].transform.position;
    }
    void SelectTarget()
    {
        float hori;
        float vert;
        Vector3 vec3 = new Vector3(hori, 0, vert);
        transform.Translate(vec3 * speed * Time.deltaTime);

    }
}
