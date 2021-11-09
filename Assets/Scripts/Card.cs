using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
	private Sprite sprite;
	private int val = 0;
	private bool ace = false;

	public Card(Sprite newSprite)
	{
		sprite = newSprite;
	}

	public int GetValue()
	{
		return val;
	}

	public void SetVal(int newVal)
	{
		ace = (newVal == 1);
		val = newVal;
	}

	public Sprite GetSprite()
	{
		return sprite;
	}

	public void SetSprite(Sprite newSprite)
	{
		sprite = newSprite;
	}

	public bool IsAce()
	{
		return ace;
	}
}