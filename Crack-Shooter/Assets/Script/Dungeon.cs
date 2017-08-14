using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public const int BattleCharacter = 4;
    public const int BattleMonster = 3;

    private GameObject[] gMonster;
    private GameObject[] gCharacter;

    private int iCurrentMonster;

	public void Awake()
    {
        gCharacter = new GameObject[BattleCharacter];
        gMonster = new GameObject[BattleMonster];

        CreateWall();

        InitObject();
        SetObject();

        iCurrentMonster = 0;
        CallCurrentMonster();
    }

    // Method

    private void CreateWall()
    {
        GameObject wall = new GameObject("Wall");

        wall.transform.SetParent(this.gameObject.transform);
        wall.transform.localScale = new Vector2(0.5f, 100.0f);
        wall.transform.Translate(new Vector2(-3.0f, 0.0f));
        wall.AddComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        wall.AddComponent<BoxCollider2D>();

        Instantiate(wall, this.gameObject.transform).GetComponent<Transform>().Translate(new Vector2(6.0f, 0.0f));
        return;
    }

    private void InitObject()
    {
        for (int i = 0; i < BattleCharacter; i++)
        {
            gCharacter[i] = Resources.Load("Object/Character") as GameObject;
            gCharacter[i] = Instantiate(gCharacter[i], this.gameObject.transform);
            gCharacter[i].GetComponent<Character>().Init();

            Vector2 vCharPos = new Vector2(0.0f, 0.0f);
            bool bCharFlip = false;
            switch (i)
            {
                case 0: vCharPos = new Vector2(-2.0f, -2.5f); bCharFlip = true; break;
                case 1: vCharPos = new Vector2(-0.7f, -4.0f); bCharFlip = true; break;
                case 2: vCharPos = new Vector2(0.7f, -4.0f); bCharFlip = false; break;
                case 3: vCharPos = new Vector2(2.0f, -2.5f); bCharFlip = false; break;
            }
            gCharacter[i].GetComponent<Transform>().Translate(vCharPos);
            gCharacter[i].GetComponent<SpriteRenderer>().flipX = bCharFlip;
        }

        for (int i = 0; i < BattleMonster; i++)
        {
            gMonster[i] = Resources.Load("Object/Monster") as GameObject;
            gMonster[i] = Instantiate(gMonster[i], this.gameObject.transform);
            gMonster[i].gameObject.SetActive(false);
        }
        return;
    }

    private void SetObject()
    {
        for (int i = 0; i < BattleMonster; i++)
        {
            gMonster[i].GetComponent<Monster>().SetCharacter(gCharacter);
            gMonster[i].GetComponent<Monster>().SetDungeon(this);
        }
        for (int i = 0; i < BattleCharacter; i++)
        {
            gCharacter[i].GetComponent<Character>().SetMonster(gMonster[iCurrentMonster]);
            gCharacter[i].GetComponent<Character>().SetCharacter(gCharacter);
        }
        return;
    }

    public void CallCurrentMonster()
    {
        gMonster[iCurrentMonster].GetComponent<Monster>().Init();
        gMonster[iCurrentMonster].gameObject.SetActive(true);
    }

    public void CallNextMonster()
    {
        iCurrentMonster++;
        if (iCurrentMonster >= BattleMonster)
            return;

        CallCurrentMonster();

        for (int i = 0; i < BattleCharacter; i++)
            gCharacter[i].GetComponent<Character>().SetMonster(gMonster[iCurrentMonster]);
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