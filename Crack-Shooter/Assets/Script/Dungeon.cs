using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    private GameObject[] gMonster;
    private GameObject[] gCharacter;

	public void Init()
    {

    }

    // -- Set --

    public void SetMonster(GameObject[] gMonster)
    {
        this.gMonster = gMonster;
    }

    public void SetCharacter(GameObject[] gCharacter)
    {
        this.gCharacter = gCharacter;
    }
}