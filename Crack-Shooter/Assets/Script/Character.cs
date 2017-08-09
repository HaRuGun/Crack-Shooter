using UnityEngine;

public class CharacterStatus
{
    public int iHp;
    public int iAtk;
    public float fRange;
    public float fPower;
    public float fDelay;
    public float fCritical;
}

public class Character : MonoBehaviour
{
    private CharacterStatus cStatus;
    private GameObject gMonster;
    private GameObject[] gCharacter;

    private float fDistance;
    private Vector2 vAngle;

    private float fCharging;
    private bool bIsCharge;

    public void Init()
    {
        cStatus = null;
    }

    private void OnMouseDown()
    {
        fDistance = Vector2.Distance(this.transform.position, gMonster.transform.position);

        vAngle = gMonster.transform.position - this.transform.position;
        vAngle.Normalize();

        if (fDistance <= 2)
            ShortAttack();
        else if (2 < fDistance && fDistance <= 4)
            MiddleAttack();
        else if (4 < fDistance && fDistance <= 6)
            LongAttack();
    }

    private void OnMouseDrag()
    {
        bIsCharge = true;
        fCharging += 0.1f;
        Debug.Log(fCharging.ToString());
    }

    private void OnMouseUp()
    {
        bIsCharge = false;
        fCharging = 0.0f;
    }

    // -- Method --

    public virtual void ShortAttack()
    {
        gMonster.GetComponent<Rigidbody2D>().AddForce(vAngle * 1000);
    }

    public virtual void MiddleAttack()
    {
        //gMonster.GetComponent<Rigidbody2D>().AddForce(vAngle * 750);
    }

    public virtual void LongAttack()
    {
        //gMonster.GetComponent<Rigidbody2D>().AddForce(vAngle * 10);
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
