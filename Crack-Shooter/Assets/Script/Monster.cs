using UnityEngine;

public class MonsterStatus
{
    public float fHp;
    public int iMaxHp;
    public float fAtk;
    public float fSpeed;
    public float fStrikeRange;
}

public class Monster : MonoBehaviour
{
    private MonsterStatus cStatus;
    private Character[] gCharacter;
    private Dungeon cDungeon;

    private float fDistance;
    private bool[] bCanAttack;

    public void Init()
    {
        cStatus = new MonsterStatus();

        cStatus.iMaxHp = 50;
        cStatus.fHp = cStatus.iMaxHp;
        cStatus.fAtk = 10;
        cStatus.fSpeed = 1;
        cStatus.fStrikeRange = 2.0f;

        bCanAttack = new bool[Dungeon.BattleCharacter];
    }

    private void Update()
    {
        DropBall();
        AttackFunc();

        if (cStatus.fHp <= 0)
            Death();
    }
    
    // Method
    
    private void AttackFunc()
    {
        for (int i = 0; i < Dungeon.BattleCharacter; i++)
        {
            fDistance = Vector2.Distance(this.transform.position, gCharacter[i].transform.position);
            if (fDistance > 1.5f)
            {
                bCanAttack[i] = true;
            }
            else
            {
                if (!bCanAttack[i])
                    continue;

                gCharacter[i].GetStatus().fHp -= (int)cStatus.fAtk;
                Debug.Log("Character(" + i.ToString() + ") : " + gCharacter[i].GetStatus().fHp.ToString());
                bCanAttack[i] = false;
            }
        }
    }

    private void DropBall()
    {
        if (this.transform.position.y <= -8.0f)
        {
            for (int i = 0; i < Dungeon.BattleCharacter; i++)
            {
                int iCurrentMaxHp = gCharacter[i].GetStatus().iMaxHp;
                int iCurrentCount = gCharacter[i].GetStatus().iDeathCount;

                iCurrentMaxHp -= iCurrentMaxHp / iCurrentCount;

                gCharacter[i].GetStatus().iMaxHp = iCurrentMaxHp;
                gCharacter[i].GetStatus().iDeathCount -= 1;

                Debug.Log("Character(" + i.ToString() + ") : " + gCharacter[i].GetStatus().fHp.ToString());
            }

            this.transform.position = new Vector2(0.0f, 8.0f);
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void Death()
    {
        cDungeon.CallNextMonster();

        this.gameObject.SetActive(false);
        return;
    }

    // -- Get --

    public MonsterStatus GetStatus()
    {
        return cStatus;
    }

    // -- Set --

    public void SetCharacter(Character[] gCharacter)
    {
        this.gCharacter = gCharacter;
    }

    public void SetDungeon(Dungeon cDungeon)
    {
        this.cDungeon = cDungeon;
    }
}
