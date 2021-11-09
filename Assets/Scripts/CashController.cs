using UnityEngine;
using UnityEngine.UI;

public class CashController : MonoBehaviour {
    public Button insertBtn;
    public Button increaseBtn;
    public Button decreaseBtn;

    public Text cashText;
    public Text betText;

    int cash = 0;
    int bet = 0;

	private void Start () {
        insertBtn.onClick.AddListener(() => InsertClicked());
        increaseBtn.onClick.AddListener(() => IncreaseClicked());
        decreaseBtn.onClick.AddListener(() => DecreaseClicked());
	}

    private void InsertClicked()
    {
        UpdateCash(100);
        increaseBtn.interactable = true;
    }

    private void IncreaseClicked()
    {
        if (bet < cash)
        {
            UpdateBet(1);
            decreaseBtn.interactable = true;
            increaseBtn.interactable = cash > bet;
        }
    }

    private void DecreaseClicked()
    {
        if (bet > 0)
        {
            UpdateBet(1 * -1);
            decreaseBtn.interactable = bet > 0;
            increaseBtn.interactable = cash > bet;
        }
    }

    public bool CanPlay() {
        if (bet > 0)
        {
            UpdateCash(bet * -1);
            return true;
        }
        return false;
    }

    public int GetBet()
    {
        return bet;
    }

    public void ButtonsEnable(bool enabled = true)
    {
        insertBtn.interactable = enabled;
        increaseBtn.interactable = enabled;
        decreaseBtn.interactable = enabled;
    }

    public void UpdateCash(int amount)
    {
        cash += amount;
        cashText.text = "cash: $" + cash;
    }

    public void UpdateBet(int amount)
    {
        bet += amount;
        betText.text = "Bet: $" + bet.ToString();
    }

    public void CheckBet()
    {
        bet = bet > cash ? cash : bet;
        betText.text = "Bet: $" + bet.ToString();
    }

    public int GetCash()
    {
        return cash;
    }
}
