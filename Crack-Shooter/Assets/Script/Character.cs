using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus
{
    public int iHp;
    public int iAtk;
    public float fRange;
    public float fPower;
    public float fDelay;
    public float fCritical;

    CharacterStatus()
    {
        iHp = 0;
        iAtk = 0;
        fRange = 0;
        fPower = 0;
        fDelay = 0;
        fCritical = 0;
    }
}

public class Character : MonoBehaviour
{
    private CharacterStatus cStatus;

    public void Init()
    {
        
    }

    // -- Get --

    public CharacterStatus GetStatus()
    {
        return cStatus;
    }
}
