using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public const int MaxCharacter = 4;
    public const int MaxMonster = 1;

    private GameObject[] gMonster;
    private GameObject[] gCharacter;

	public void Awake()
    {
        gCharacter = new GameObject[MaxCharacter];
        gMonster = new GameObject[MaxMonster];

        CreateWall();

        InitObject();
        SetObject();
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
        for (int i = 0; i < MaxCharacter; i++)
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

        for (int i = 0; i < MaxMonster; i++)
        {
            gMonster[i] = Resources.Load("Object/Monster") as GameObject;
            gMonster[i] = Instantiate(gMonster[i], this.gameObject.transform);
            gMonster[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
            gMonster[i].GetComponent<Monster>().Init();
        }
        return;
    }

    private void SetObject()
    {
        for (int i = 0; i < MaxMonster; i++)
        {
            gMonster[i].GetComponent<Monster>().SetCharacter(gCharacter);
        }
        for (int i = 0; i < MaxCharacter; i++)
        {
            gCharacter[i].GetComponent<Character>().SetMonster(gMonster[0]);
            gCharacter[i].GetComponent<Character>().SetCharacter(gCharacter);
        }
        return;
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