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

    /// <summary>
    /// Command ran when insert clicked. Adds $100
    /// </summary>
    private void InsertClicked()
    {
        UpdateCash(100);
        increaseBtn.interactable = true;
    }


    /// <summary>
    /// Increase bet by one
    /// </summary>
    private void IncreaseClicked()
    {
        if (bet < cash)
        {
            UpdateBet(1);
            decreaseBtn.interactable = true;
            increaseBtn.interactable = cash > bet;
        }
    }

    /// <summary>
    /// Decreases bet by one.
    /// </summary>
    private void DecreaseClicked()
    {
        if (bet > 0)
        {
            UpdateBet(1 * -1);
            decreaseBtn.interactable = bet > 0;
            increaseBtn.interactable = cash > bet;
        }
    }


    /// <summary>
    /// Checks if the bet is less than 0
    /// </summary>
    /// <returns></returns>
    public bool CanPlay() {
        if (bet > 0)
        {
            UpdateCash(bet * -1);
            return true;
        }
        return false;
    }


    /// <summary>
    /// Returns player initial bet
    /// </summary>
    /// <returns>intial player bet</returns>
    public int GetBet()
    {
        return bet;
    }


    /// <summary>
    /// Sets all button in object to whatever the enable flag is
    /// </summary>
    /// <param name="enabled">boolean flag</param>
    public void ButtonsEnable(bool enabled = true)
    {
        insertBtn.interactable = enabled;
        increaseBtn.interactable = enabled;
        decreaseBtn.interactable = enabled;
    }

    /// <summary>
    /// Increases or decreases cash by amount
    /// </summary>
    /// <param name="amount">amount to increase or decrease by</param>
    public void UpdateCash(int amount)
    {
        cash += amount;
        cashText.text = "cash: $" + cash;
    }

    /// <summary>
    /// Increase or decreases bet by amount
    /// </summary>
    /// <param name="amount"></param>
    public void UpdateBet(int amount)
    {
        bet += amount;
        betText.text = "Bet: $" + bet.ToString();
    }

    /// <summary>
    /// Make sure bet is not higher than cash
    /// </summary>
    public void CheckBet()
    {
        bet = bet > cash ? cash : bet;
        betText.text = "Bet: $" + bet.ToString();
    }

    /// <summary>
    /// Get internal cash amount
    /// </summary>
    /// <returns></returns>
    public int GetCash()
    {
        return cash;
    }
}
