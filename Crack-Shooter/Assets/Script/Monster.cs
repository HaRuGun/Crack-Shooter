using UnityEngine;

public class MonsterStatus
{
    public int iHp;
    public int iMaxHp;
    public int iAtk;
    public float fSpeed;
    public float fStrikeRange;
}

public class Monster : MonoBehaviour
{
    private MonsterStatus cStatus;
    private GameObject[] gCharacter;

    private float fDistance;
    private bool[] bCanAttack;

    public void Init()
    {
        cStatus = new MonsterStatus();

        cStatus.iMaxHp = 100;
        cStatus.iHp = cStatus.iMaxHp;
        cStatus.iAtk = 10;
        cStatus.fSpeed = 1;
        cStatus.fStrikeRange = 2.0f;

        bCanAttack = new bool[Dungeon.MaxCharacter];
    }

    private void Update()
    {
        DropBall();
        AttackFunc();

        if (cStatus.iHp <= 0)
            this.gameObject.SetActive(false);
    }
    
    // Method
    
    private void AttackFunc()
    {
        for (int i = 0; i < Dungeon.MaxCharacter; i++)
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

                gCharacter[i].GetComponent<Character>().GetStatus().iHp -= cStatus.iAtk;
                Debug.Log("Character(" + i.ToString() + ") : " + gCharacter[i].GetComponent<Character>().GetStatus().iHp.ToString());
                bCanAttack[i] = false;
            }
        }
    }

    private void DropBall()
    {
        if (this.transform.position.y <= -8.0f)
        {
            for (int i = 0; i < Dungeon.MaxCharacter; i++)
            {
                int iCurrentMaxHp = gCharacter[i].GetComponent<Character>().GetStatus().iMaxHp;
                int iCurrentCount = gCharacter[i].GetComponent<Character>().GetStatus().iDeathCount;

                iCurrentMaxHp -= iCurrentMaxHp / iCurrentCount;

                gCharacter[i].GetComponent<Character>().GetStatus().iMaxHp = iCurrentMaxHp;
                gCharacter[i].GetComponent<Character>().GetStatus().iDeathCount -= 1;

                Debug.Log("Character(" + i.ToString() + ") : " + gCharacter[i].GetComponent<Character>().GetStatus().iHp.ToString());
            }

            this.transform.position = new Vector2(0.0f, 8.0f);
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    // -- Get --

    public MonsterStatus GetStatus()
    {
        return cStatus;
    }

    // -- Set --

    public void SetCharacter(GameObject[] gCharacter)
    {
        this.gCharacter = gCharacter;
    }
}
