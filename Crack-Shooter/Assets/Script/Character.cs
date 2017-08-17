using UnityEngine;

public class CharacterStatus
{
    public float fHp;
    public int iMaxHp;
    public float fAtk;
    public int iRangeType;
    public float fPower;
    public float fDelay;
    public float fCritical;

    public int iDeathCount;
}

public class Character : MonoBehaviour
{
    protected CharacterStatus cStatus;
    protected Monster gMonster;
    protected Character[] gCharacter;

    protected float fDistance;
    protected Vector2 vAngle;

    protected float fCharging = 0.0f;
    protected bool bIsCharge = false;
    protected float fCurrentDelay = 0.0f;
    protected bool bCanAttack = true;

    public void Init()
    {
        cStatus = new CharacterStatus();
        InitStatus();
        cStatus.iDeathCount = 4;
    }

    public void Update()
    {
        AttackCheck();

        if (cStatus.iMaxHp <= 0 || cStatus.fHp <= 0)
            this.gameObject.SetActive(false);
        if (cStatus.fHp > cStatus.iMaxHp)
            cStatus.fHp = cStatus.iMaxHp;
    }

    protected void OnMouseDown()
    {
        AttackFunc(cStatus.iRangeType);
    }

    protected void OnMouseDrag()
    {
        bIsCharge = true;
        fCharging += 0.1f;
    }

    protected void OnMouseUp()
    {
        bIsCharge = false;
        fCharging = 0.0f;
    }

    // -- Method --

    protected virtual void InitStatus()
    {
        cStatus.iMaxHp = 100;
        cStatus.fHp = cStatus.iMaxHp;
        cStatus.fAtk = 10;
        cStatus.iRangeType = 1;
        cStatus.fPower = 1.0f;
        cStatus.fDelay = 2.0f;
        cStatus.fCritical = 0.05f;
    }

    protected virtual void ShortAttack()
    {
        if (fDistance <= 2)
            gMonster.GetComponent<Rigidbody2D>().AddForce(vAngle * 1000 * cStatus.fPower);
    }

    protected virtual void MiddleAttack()
    {
        if (fDistance <= 4)
            gMonster.GetComponent<Rigidbody2D>().AddForce(vAngle * 750 * cStatus.fPower);
    }

    protected virtual void LongAttack()
    {
        if (fDistance <= 6)
            gMonster.GetComponent<Rigidbody2D>().AddForce(vAngle * 100 * cStatus.fPower);
    }

    protected void AttackCheck()
    {
        if (!bCanAttack)
        {
            fCurrentDelay += 0.1f;
            if (fCurrentDelay >= cStatus.fDelay)
            {
                bCanAttack = true;
                fCurrentDelay = 0.0f;
            }
        }
    }

    protected void AttackFunc(int iRangeType)
    {
        if (!bCanAttack)
            return;

        fDistance = Vector2.Distance(this.transform.position, gMonster.transform.position);
        vAngle = gMonster.transform.position - this.transform.position;
        vAngle.Normalize();

        switch (iRangeType)
        {
            case 1: ShortAttack(); break;
            case 2: MiddleAttack(); break;
            case 3: LongAttack(); break;
        }

        gMonster.GetStatus().fHp -= cStatus.fAtk;
        Debug.Log("Monster : " + gMonster.GetStatus().fHp.ToString());
        bCanAttack = false;
    }

    public void Passive()
    {
        PassiveA();
        PassiveB();
        PassiveC();
    }


    // -- Skill --
    protected virtual void ActiceA() { }
    protected virtual void ActiceB() { }
    protected virtual void ActiceC() { }
    protected virtual void PassiveA() { }
    protected virtual void PassiveB() { }
    protected virtual void PassiveC() { }

    // -- Get --

    public CharacterStatus GetStatus()
    {
        return cStatus;
    }

    // -- Set --

    public void SetMonster(Monster gMonster)
    {
        this.gMonster = gMonster;
    }

    public void SetCharacter(Character[] gCharacter)
    {
        this.gCharacter = gCharacter;
    }
}
