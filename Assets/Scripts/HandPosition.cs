using UnityEngine;

public class HandPosition : MonoBehaviour {
    public Sprite backSprite;
    private Card card;

    /// <summary>
    /// Sets card in hand position
    /// </summary>
    /// <param name="newCard">card to set</param>
    /// <param name="flip">whether card should initially be flipped</param>
    public void SetCard(Card newCard, bool flip = true)
    {
        card = newCard;
        gameObject.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        if (flip)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = card.GetSprite();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = backSprite;
        }
    }

    /// <summary>
    /// Resets hand position
    /// </summary>
    public void Reset()
    {
        card = null;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }


    /// <summary>
    /// Flips card at hand position
    /// </summary>
    public void Flip() 
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = card.GetSprite();
    }

    /// <summary>
    /// Get current face value of card
    /// </summary>
    /// <returns>face value of card</returns>
    public int GetValue()
    {
        return card.GetValue();
    }

    /// <summary>
    /// Give card away
    /// </summary>
    /// <returns></returns>
    public Card GrabCard()
    {
        Card cardGrabbed = card;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        return cardGrabbed ;
    }
 
}
