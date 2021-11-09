using UnityEngine;

public class HandPosition : MonoBehaviour {
    public Sprite backSprite;
    private Card card;

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

    public void Reset()
    {
        card = null;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }


    public void Flip() 
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = card.GetSprite();
    }

    public int GetValue()
    {
        return card.GetValue();
    }

    public Card GrabCard()
    {
        Card cardGrabbed = card;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        return cardGrabbed ;
    }
 
}
