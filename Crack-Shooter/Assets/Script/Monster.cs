using UnityEngine;

public class MonsterStatus
{
    int iHp;
    int iAtk;
    float fSpeed;
    float fStrikeRange;
}

public class Monster : MonoBehaviour
{
    private MonsterStatus cStatus;
    private GameObject[] gCharacter;

    public void Init()
    {
        cStatus = null;
    }

    private void Update()
    {
        if (this.transform.position.y <= -8.0f)
        {
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
