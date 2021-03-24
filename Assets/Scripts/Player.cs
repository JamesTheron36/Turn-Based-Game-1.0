using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    fire,
    water,
    earth,
    dragon,
    ice
}
public class Player : MonoBehaviour
{

    [SerializeField]
    int atk = 3;
    [SerializeField]
    int def = 5;
    [SerializeField]
    int acc = 4;
    [SerializeField]
    Type type;
    [SerializeField]
    GameObject healthBar;

    public float health = 100;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetAtk()
    {
        return atk;
    }
    public int GetDef()
    {
        return def;
    }
    public int GetAcc()
    {
        return acc;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }

    public bool MatchingType(Card c)
    {
        bool b = false;
        if(c.secondary == Card.Secondary.dragon && type == Type.dragon)
        {
            b = true;
        }
        else if(c.secondary == Card.Secondary.fire && type == Type.fire)
        {
            b = true;
        }
        else if (c.secondary == Card.Secondary.water && type == Type.water)
        {
            b = true;
        }
        else if (c.secondary == Card.Secondary.ice && type == Type.ice)
        {
            b = true;
        }
        else if (c.secondary == Card.Secondary.earth && type == Type.earth)
        {
            b = true;
        }
        return b;
    }
    public void UpdateHealthBar() {
        float maxHealth = 100f;
        float ratio = health / maxHealth;
        Vector3 vec = new Vector3(ratio, 1, 1);
        healthBar.transform.localScale = vec;
    }


    
}
