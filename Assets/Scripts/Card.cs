using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class representing card
/// </summary>
public class Card
{
	private Sprite sprite;
	private int val = 0;
	private bool ace = false;

    /// <summary>
    /// Card constructor
    /// </summary>
    /// <param name="newSprite">sprite to initialize card with</param>
	public Card(Sprite newSprite)
	{
		sprite = newSprite;
	}

    /// <summary>
    /// Function to get value of card
    /// </summary>
    /// <returns>value of card</returns>
	public int GetValue()
	{
		return val;
	}

    /// <summary>
    /// Sets card value
    /// </summary>
    /// <param name="newVal"> new value to set card to</param>
	public void SetVal(int newVal)
	{
		ace = (newVal == 1);
		val = newVal;
	}

    /// <summary>
    /// Get card sprite
    /// </summary>
    /// <returns>card sprite</returns>
	public Sprite GetSprite()
	{
		return sprite;
	}

    /// <summary>
    /// Sets card sprite
    /// </summary>
    /// <param name="newSprite"></param>
	public void SetSprite(Sprite newSprite)
	{
		sprite = newSprite;
	}

    /// <summary>
    /// Check if card is ace
    /// </summary>
    /// <returns>returns whether card is ace</returns>
	public bool IsAce()
	{
		return ace;
	}
}