using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public const int MaxCharacter = 12;

    private Character[] gCharacter;

    public override void Init()
    {
        gCharacter = new Character[MaxCharacter];

    }
}
