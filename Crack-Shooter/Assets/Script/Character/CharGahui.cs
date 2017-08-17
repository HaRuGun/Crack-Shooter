using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharGahui : Character
{
    protected override void InitStatus()
    {
        cStatus.iMaxHp = 100;
        cStatus.fHp = cStatus.iMaxHp;
        cStatus.fAtk = 10 / 10 * 8;
        cStatus.iRangeType = 3;
        cStatus.fPower = 1.0f;
        cStatus.fDelay = 0.1f;
        cStatus.fCritical = 0.05f;
    }

    protected override void PassiveA()
    {
        // 
        return;
    }

    protected override void PassiveB()
    {
        StartCoroutine("ATKBoost");
        return;
    }

    protected override void PassiveC()
    {
        // 
        return;
    }

    // -- Original --

    IEnumerator ATKBoost()
    {
        Debug.Log("ATKBoost Active!");
        for(int i = 0; i < Dungeon.BattleCharacter; i++)
        {
            float fDestAtk = gCharacter[i].GetStatus().fAtk;
            fDestAtk += fDestAtk / 10 * 3;

            gCharacter[i].GetStatus().fAtk = fDestAtk;
        }
        yield return new WaitForSeconds(3f);

        Debug.Log("ATKBoost Disactive!");
        for (int i = 0; i < Dungeon.BattleCharacter; i++)
        {
            float fDestAtk = gCharacter[i].GetStatus().fAtk;
            fDestAtk = fDestAtk / 13 * 10;

            gCharacter[i].GetStatus().fAtk = fDestAtk;
        }
        yield return new WaitForSeconds(3f);

        yield return StartCoroutine("ATKBoost");
    }
}
