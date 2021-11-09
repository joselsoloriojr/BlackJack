using UnityEngine;

public class Deck : MonoBehaviour 
{
	public Sprite[] cardSprites;
	private  Card[] cards = new Card[52];
	private int curIndex = 0;

	private void Start () 
	{
		CreateDeck();	
	}

	private void CreateDeck()
	{
		for (int i = 0; i < cardSprites.Length; i++)
		{
			cards[i] = new Card(cardSprites[i]);
			int cardValue = (i + 1) % 13;
			if (cardValue > 10 || cardValue == 0)
			{
				cards[i].SetVal(10);
			}
			else
			{
				cards[i].SetVal(cardValue);
			}
		}
	}

	// Fisher-Yates Shuffle
	public void Shuffle() 
	{
		for (int a = cards.Length - 1; a >= 0; a-- ) {
			int b = Random.Range(0, cards.Length);
			Card temp = cards[a];
			cards[a] = cards[b];
			cards[b] = temp;
		}
		curIndex = 0;
	}

	public Card DealCard() 
	{
		return cards[curIndex++];
	}
}
