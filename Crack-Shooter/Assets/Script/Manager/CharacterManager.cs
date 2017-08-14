using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public const int MaxCharacter = 12;

    private GameObject[] gCharacter;

    public override void Init()
    {
        gCharacter = new GameObject[MaxCharacter];

    }
}
