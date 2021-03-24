using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Card[] playerDeck = new Card[8];
    [SerializeField]
    Card[] oppDeck = new Card[8];

    [SerializeField]
    MenuManager mm;

    [SerializeField]
    Card activeCardPlayer;
    
    [SerializeField]
    Card activeCardOpponent;

    [SerializeField]
    Card Temp;

    [SerializeField]
    Transform[] handCards = new Transform[4];
    
    [SerializeField]
    Transform[] OppHandPos = new Transform[4];

    [SerializeField]
    public Transform deckPos, deckOppPos;


    [SerializeField]
    public Transform playerPlayedPos;
    [SerializeField]
    public Transform oppPlayedPos;

    bool played = false;
    
    [SerializeField]
    Player player;
    [SerializeField]
    Player opp;

    [SerializeField]
    float critMod = 0.5f;
    
    [SerializeField]
    float atkTypeMod = 0.5f;
    
    [SerializeField]
    float counterMod = 0.5f;

    public SpriteRenderer oppWins;
    public SpriteRenderer youWin;

    public float maxBaseMultiple = 15.0f;
    public float minBaseMultiple = 10.0f;
    int index;
    int oppIndex;
    Card[] oppHand = new Card[4];
    bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        youWin.enabled = false;
        oppWins.enabled = false;
        UpdatePlayerHand();
        HidePlayerDeck();
        UpdateOppHand();
        HideOppDeck();
        activeCardOpponent.HideCard();
        activeCardPlayer.HideCard();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == "Card Place Holder 1" && Input.GetMouseButtonDown(0) && played == false && finished == false)
            {
                index = 0;
                activeCardPlayer.Change(playerDeck[index]);
                activeCardPlayer.ShowCard();
                played = true;
                //Debug.Log("hit");
            }

            if (hit.transform.name == "Card Place Holder 2" && Input.GetMouseButtonDown(0) && played == false && finished == false)
            {
                index = 1;
                activeCardPlayer.Change(playerDeck[index]);
                activeCardPlayer.ShowCard();
                played = true;
                //Debug.Log("hit");
            }

            if (hit.transform.name == "Card Place Holder 3" && Input.GetMouseButtonDown(0) && played == false && finished == false)
            {
                index = 2;
                activeCardPlayer.Change(playerDeck[index]);
                activeCardPlayer.ShowCard();
                played = true;
                //Debug.Log("hit");
            }

            if (hit.transform.name == "Card Place Holder 4" && Input.GetMouseButtonDown(0) && played == false && finished == false)
            {
                index = 3;
                activeCardPlayer.Change(playerDeck[index]);
                activeCardPlayer.ShowCard();
                played = true;
                //Debug.Log("hit");
            }

        }
        if(played == true && finished == false)
        {

            StartCoroutine(OpponentPlayDelay());
            StartCoroutine(DamageDelay());
            played = false;
            
        }

        if(player.health <= 0)
        {
            youWin.enabled = false;
            oppWins.enabled = true;
            finished = true;
            StartCoroutine(ResetDelay());
            

        }
        if (opp.health <= 0)
        {
            youWin.enabled = true;
            oppWins.enabled = false;
            finished = true;
            StartCoroutine(ResetDelay());

        }

    }

    void UpdatePlayerHand()
    {
        for(int loop = 0; loop < handCards.Length; loop++)
        {
            playerDeck[loop].ShowCard();
            playerDeck[loop].transform.position = handCards[loop].position;
        }
    }

    void HidePlayerDeck()
    {
        for(int loop = playerDeck.Length - handCards.Length; loop < playerDeck.Length; loop++)
        {
            playerDeck[loop].HideCard();
            playerDeck[loop].transform.position = deckPos.position;
            //Debug.Log(loop);
        }
    }
    void PlayCardPlayer(Card c)
    {
        activeCardPlayer.Change(c);
        activeCardPlayer.ShowCard();
    }
    void PlayCardOpp(Card c)
    {
        //c.transform.position = oppPlayedPos.position;
        activeCardOpponent.Change(c);
    }
    void UpdateOppHand()
    {
        for(int loop = 0; loop < oppHand.Length; loop++)
        {
            oppDeck[loop].HideCard();
            oppDeck[loop].transform.position = OppHandPos[loop].position;
        }
    }
    void HideOppDeck()
    {
        for (int loop = oppDeck.Length - handCards.Length; loop < oppDeck.Length; loop++)
        {
            oppDeck[loop].HideCard();
            oppDeck[loop].transform.position = deckOppPos.position;
            //Debug.Log(loop);
        }
    }


    void ReturnPlayerCard(Card card)
    {
        int num = playerDeck.Length - handCards.Length;
        //Debug.Log("index is " + index);
        int size = playerDeck.Length - 1;
        Temp.SetEqual(playerDeck[size]);
        playerDeck[size].Change(playerDeck[index]);
        playerDeck[index].Change(Temp);
        for (int loop = size; loop > num + 1; loop--)
        {
            Temp.SetEqual(playerDeck[loop]);
            playerDeck[loop].Change(playerDeck[loop - 1]);
            playerDeck[loop - 1].Change(Temp);
        }
        
        UpdatePlayerHand();
        HidePlayerDeck();
        
    }

    void ReturnOppCard(Card card)
    {
        int num = oppDeck.Length - handCards.Length;
        //Debug.Log("index is " + index);
        int size = oppDeck.Length - 1;
        Temp.SetEqual(oppDeck[size]);
        oppDeck[size].Change(oppDeck[oppIndex]);
        oppDeck[oppIndex].Change(Temp);
        for (int loop = size; loop > num + 1; loop--)
        {
            Temp.SetEqual(oppDeck[loop]);
            oppDeck[loop].Change(oppDeck[loop - 1]);
            oppDeck[loop - 1].Change(Temp);
        }
        
        UpdateOppHand();
        HideOppDeck();

    }

    public float CalculateDmg(Card cAtk, Card cDef, Player att, Player df)
    {
        float dmg;
        float counterBonus = 0;
        float ratio = ((float)att.GetAtk() / (float)df.GetDef());
        float maxBase = ratio * maxBaseMultiple;
        float minBase = ratio * minBaseMultiple;
        float baseDmg = Random.Range(minBase, maxBase);
        float atkTypeBonus = 0;
        if (cAtk.CounterSecondary(cDef))
        {
            counterBonus = counterMod * baseDmg;
        }
        float crit = 0;
        int critRoll = Random.Range(0, 100);

        if(critRoll <= att.GetAcc())
        {
            crit = critMod * baseDmg;
            
        }
        if (att.MatchingType(cAtk))
        {
            atkTypeBonus = atkTypeMod * baseDmg;

        }
        dmg = baseDmg + crit + atkTypeBonus + counterBonus;
        Debug.Log(dmg);
        return dmg;
    }
   
    void ChooseOppCard()
    {
        for(int loop = 0; loop < 4; loop++)
        {
            if (opp.MatchingType(oppDeck[loop]))
            {
                PlayCardOpp(oppDeck[loop]);
                oppIndex = loop;
                return;
            }
        }
        int card = Random.Range(0, 3);
        oppIndex = card;
        activeCardOpponent.Change(oppDeck[index]);
        //Debug.Log(oppIndex);
    }
    IEnumerator OpponentPlayDelay()
    {
        yield return new WaitForSeconds(1);
        ChooseOppCard();
        activeCardOpponent.ShowCard();

    }

    void CommenceTurn()
    {
        if (activeCardPlayer.CounterPrimary(activeCardOpponent))
        {
            Debug.Log("player win");
            float dmg = CalculateDmg(activeCardPlayer, activeCardOpponent, player, opp);
            opp.TakeDamage(dmg);
            opp.UpdateHealthBar();
            ReturnOppCard(oppDeck[oppIndex]);
            ReturnPlayerCard(playerDeck[index]);

            played = false;
        }
        else if (activeCardOpponent.CounterPrimary(activeCardPlayer))
        {
            Debug.Log("opponent win");
            float dmg = CalculateDmg(activeCardOpponent, activeCardPlayer, opp, player);
            player.TakeDamage(dmg);
            player.UpdateHealthBar();
            ReturnOppCard(oppDeck[oppIndex]);
            ReturnPlayerCard(playerDeck[index]);
            played = false;
        }
        else if (activeCardPlayer.SameCard(activeCardOpponent))
        {
            Debug.Log("same card");
            ReturnOppCard(oppDeck[oppIndex]);
            ReturnPlayerCard(playerDeck[index]);
            played = false;
        }
        
    }
    IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(2);
        CommenceTurn();

    }
    IEnumerator HideDelay()
    {
        yield return new WaitForSeconds(2.5f);
        activeCardOpponent.HideCard();
        activeCardPlayer.HideCard();
    }
    IEnumerator ResetDelay()
    {
        yield return new WaitForSeconds(2);
        mm.LoadGame();
    }

}
