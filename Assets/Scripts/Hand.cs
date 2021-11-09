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
    private bool is30 = false;


    /// <summary>
    /// Sets player card at current internal index and increase handvalue by value of card
    /// </summary>
    /// <param name="card">Card to set and inrease handvalue by</param>
    /// <param name="isDealer">Flag to determine if current hand is dealers hand</param>
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

    /// <summary>
    /// Increments handvalue by card value
    /// </summary>
    /// <param name="card">Card given to hand</param>
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

    /// <summary>
    /// Flips Dealer's unflipped card in hand
    /// </summary>
	public void Flip()
	{
		score.text = handvalue.ToString();
		pos[posIndex++].Flip();
	}

    /// <summary>
    /// Resets hand back to defaults
    /// </summary>
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

    /// <summary>
    /// Function to get handvalue
    /// </summary>
    /// <returns>current handvalue of hand</returns>
	public int GetHandValue() 
	{
		return handvalue;
	}

    /// <summary>
    /// Function to check if hand bust
    /// </summary>
    /// <returns>bust flag</returns>
    public bool GetBust()
    {
        return bust;
    }

    /// <summary>
    /// Function to get current hand bet
    /// </summary>
    /// <returns>current hand bet</returns>
    public int GetBet()
    {
        return bet;
    }

    /// <summary>
    /// Function to set bet
    /// </summary>
    /// <param name="newBet">new bet to set</param>
    public void SetBet(int newBet)
    {
        bet = newBet;
        betText.text = "Bet: $" + bet.ToString();
    }

    /// <summary>
    /// Function to add to score hand text
    /// </summary>
    /// <param name="result">string to append</param>
    public void SetResult(string result)
    {
        score.text += result;
    }

    /// <summary>
    /// Function to grab last card on hand and reduce posIndex by 1
    /// </summary>
    /// <returns>Last hand on card</returns>
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

    /// <summary>
    /// Check if hand is 30 in 3
    /// </summary>
    /// <returns>is30 flag</returns>
    public bool Is30()
    {
        return is30;
    }
}
