using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
	public HandPosition[] pos;
	public Text score;
    public Text betText;
	private int handvalue = 0;
    private int posIndex = 0;
	private bool hasSoftAce = false;

    private int bet = 0;
    private bool bust = false;
    private bool stand = false;
    private bool is30 = false;

    public void Deal(Card card, bool isDealer = false) 
	{
        IncrementHandValue(card);
        pos[posIndex].gameObject.SetActive(true);
		if (isDealer && posIndex == 1)
		{
			pos[posIndex].SetCard(card, false);
		}
		else
		{
			pos[posIndex++].SetCard(card);
			score.text = handvalue.ToString();
			if (handvalue != 21 && hasSoftAce && handvalue - 10 > 0)
			{
				score.text += "/" + (handvalue - 10).ToString();
			}
		}

        //check for 30 in  3
        if (posIndex == 3 && handvalue == 30)
        {
            is30 = true;
        }

        //check for bust
        if (handvalue > 21)
        {
            bust = true;
        }
	}

    public void IncrementHandValue(Card card)
    {
        if (card.IsAce() && handvalue + 11 < 22)
        {
            handvalue += 11;
            hasSoftAce = true;
        }
        else
        {
            handvalue += card.GetValue();
            if (hasSoftAce && handvalue > 21)
            {
                handvalue -= 10;
                hasSoftAce = false;
            }
        }
    }

	public void Flip()
	{
		score.text = handvalue.ToString();
		pos[posIndex++].Flip();
	}

	public void Reset() 
	{
        bet = 0;
		posIndex = 0;
		handvalue = 0;
		hasSoftAce = false;
        bust = false;
        is30 = false;
		for (int i = 0; i < pos.Length; i++)
		{
			pos[i].Reset();
		}
	}

	public int GetHandValue() 
	{
		return handvalue;
	}

    public bool GetStand()
    {
        return stand;
    }

    public bool GetBust()
    {
        return bust;
    }

    public void SetStand(bool enable)
    {
        stand = enable;
    }

    public int GetBet()
    {
        return bet;
    }

    public void SetBet(int newBet)
    {
        bet = newBet;
        betText.text = "Bet: $" + bet.ToString();
    }

    public void SetResult(string result)
    {
        score.text += result;
    }

    public Card GrabCard()
    {
        Card grabbed = pos[--posIndex].GrabCard();
        handvalue -= grabbed.GetValue();
        if (handvalue == 11) // if double aces
        {
            hasSoftAce = true;
            score.text = "11/1";
        }
        else
        {
            score.text = handvalue.ToString();
        }
        return grabbed;
    }

    public bool Is30()
    {
        return is30;
    }
}
