using UnityEngine;

public class CharacterStatus
{
    public int iHp;
    public int iMaxHp;
    public int iAtk;
    public int iRange;
    public float fPower;
    public float fDelay;
    public float fCritical;

    public int iDeathCount;
}

public class Character : MonoBehaviour
{
    private CharacterStatus cStatus;
    private GameObject gMonster;
    private GameObject[] gCharacter;

    private float fDistance;
    private Vector2 vAngle;

    private float fCharging = 0.0f;
    private bool bIsCharge = false;
    private float fCurrentDelay = 0.0f;
    private bool bCanAttack = true;

    public void Init()
    {
        cStatus = new CharacterStatus();

        cStatus.iMaxHp = 100;
        cStatus.iHp = cStatus.iMaxHp;
        cStatus.iAtk = 10;
        cStatus.iRange = 1;
        cStatus.fPower = 1.0f;
        cStatus.fDelay = 2.0f;
        cStatus.fCritical = 0.05f;

        cStatus.iDeathCount = 4;
    }

    private void Update()
    {
        AttackCheck();

        if (cStatus.iMaxHp <= 0 || cStatus.iHp <= 0)
            this.gameObject.SetActive(false);
        if (cStatus.iHp > cStatus.iMaxHp)
            cStatus.iHp = cStatus.iMaxHp;
    }

    private void OnMouseDown()
    {
        AttackFunc(cStatus.iRange);
    }

    private void OnMouseDrag()
    {
        bIsCharge = true;
        fCharging += 0.1f;
    }

    private void OnMouseUp()
    {
        bIsCharge = false;
        fCharging = 0.0f;
    }

    // -- Method --

    public virtual void ShortAttack()
    {
        if (fDistance <= 2)
            gMonster.GetComponent<Rigidbody2D>().AddForce(vAngle * 1000 * cStatus.fPower);
    }

    public virtual void MiddleAttack()
    {
        if (fDistance <= 4)
            gMonster.GetComponent<Rigidbody2D>().AddForce(vAngle * 750 * cStatus.fPower);
    }

    public virtual void LongAttack()
    {
        if (fDistance <= 6)
            gMonster.GetComponent<Rigidbody2D>().AddForce(vAngle * 10 * cStatus.fPower);
    }
    
    private void AttackCheck()
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

    private void AttackFunc(int iRange)
    {
        if (!bCanAttack)
            return;

        fDistance = Vector2.Distance(this.transform.position, gMonster.transform.position);
        vAngle = gMonster.transform.position - this.transform.position;
        vAngle.Normalize();

        switch (iRange)
        {
            case 1: ShortAttack(); break;
            case 2: MiddleAttack(); break;
            case 3: LongAttack(); break;
        }

        gMonster.GetComponent<Monster>().GetStatus().iHp -= cStatus.iAtk;
        Debug.Log("Monster : " + gMonster.GetComponent<Monster>().GetStatus().iHp.ToString());
        bCanAttack = false;
    }

    // -- Get --

    public CharacterStatus GetStatus()
    {
        return cStatus;
    }

    // -- Set --

    public void SetMonster(GameObject gMonster)
    {
        this.gMonster = gMonster;
    }

    public void SetCharacter(GameObject[] gCharacter)
    {
        this.gCharacter = gCharacter;
    }
}
