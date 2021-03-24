using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    
    public enum Primary
    {
        rock,
        paper,
        scissors
    }
    public enum Secondary
    {
        fire,
        water,
        ice,
        earth,
        dragon
    }
    [SerializeField]
    SpriteRenderer background;
    [SerializeField]
    SpriteRenderer image;
    [SerializeField]
    SpriteRenderer flipped;

    public Primary primary;
    public Secondary secondary;
    public int cardID;
    [SerializeField]
    Sprite earthBack;
    [SerializeField]
    Sprite fireBack;
    [SerializeField]
    Sprite waterBack;
    [SerializeField]
    Sprite dragonBack;
    [SerializeField]
    Sprite iceBack;
    


    [SerializeField]
    Sprite rockImage;
    [SerializeField]
    Sprite sciImage;
    [SerializeField]
    Sprite paperImage;

    [SerializeField]
    bool OppCard;

    // Start is called before the first frame update
    void Start()
    {
        PrimaryImageIni();
        BackgroundIni();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Card()
    {
        
    }
    void PrimaryImageIni()
    {
        if(primary == Primary.rock)
        {
            image.sprite = rockImage;
        }
        else if(primary == Primary.paper)
        {
            image.sprite = paperImage;
        }
        else if(primary == Primary.scissors)
        {
            image.sprite = sciImage;
        }       
    }

    void BackgroundIni()
    {
        if(secondary == Secondary.fire)
        {
            background.sprite = fireBack;
        }
        else if (secondary == Secondary.water)
        {
            background.sprite = waterBack;
        }
        else if (secondary == Secondary.ice)
        {
            background.sprite = iceBack;
        }
        else if (secondary == Secondary.earth)
        {
            background.sprite = earthBack;
        }
        else if (secondary == Secondary.dragon)
        {
            background.sprite = dragonBack;
        }
    }
    public void ShowCard()
    {
        background.enabled = true;
        image.enabled = true;
        flipped.enabled = false;
    }
    public void HideCard()
    {
        background.enabled = false;
        image.enabled = false;
        flipped.enabled = true;
    }

    public bool CounterSecondary(Card def)
    {
        bool c = false;
        if(secondary == Secondary.water && def.secondary == Secondary.fire)
        {
            c = true;
        }
        else if(secondary == Secondary.water && def.secondary == Secondary.dragon)
        {
            c = true;
        }
        else if (secondary == Secondary.fire && def.secondary == Secondary.ice)
        {
            c = true;
        }
        else if (secondary == Secondary.fire && def.secondary == Secondary.earth)
        {
            c = true;
        }
        else if (secondary == Secondary.ice && def.secondary == Secondary.dragon)
        {
            c = true;
        }
        else if (secondary == Secondary.ice && def.secondary == Secondary.water)
        {
            c = true;
        }
        else if (secondary == Secondary.dragon && def.secondary == Secondary.fire)
        {
            c = true;
        }
        else if (secondary == Secondary.dragon && def.secondary == Secondary.earth)
        {
            c = true;
        }
        else if (secondary == Secondary.earth && def.secondary == Secondary.water)
        {
            c = true;
        }
        else if (secondary == Secondary.earth && def.secondary == Secondary.ice)
        {
            c = true;
        }
        return c;

    }
    public bool CounterPrimary(Card c)
    {
        bool b = false;
        if(primary == Primary.rock && c.primary == Primary.scissors)
        {
            b = true;
        }
        else if (primary == Primary.paper && c.primary == Primary.rock)
        {
            b = true;
        }
        else if (primary == Primary.scissors && c.primary == Primary.paper)
        {
            b = true;
        }

        else if(primary == c.primary)
        {
            if (CounterSecondary(c))
            {
                b = true;
            }
        }
        return b;
    }
    public bool SameCard(Card c)
    {
        if(primary == c.primary && secondary == c.secondary)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Change(Card c)
    {
        primary = c.primary;
        secondary = c.secondary;
        BackgroundIni();
        PrimaryImageIni();

    }
    public void SetEqual(Card c)
    {
        primary = c.primary;
        secondary = c.secondary;

    }

    public bool Equals(Card c)
    {
        if(primary == c.primary && secondary == c.secondary)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
