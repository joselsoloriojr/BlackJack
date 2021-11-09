using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public Button dealBtn;
	public Button hitBtn;
    public Button doubleBtn;
	public Button standBtn;
    public Button splitBtn;

    public Button RulesBtn;
    public Button RulesExitBtn;

    public Canvas RulesCanvas;

    public Image[] arrows;

    public CashController cashController;

	public Deck deck;
	public Text resultText;

    public Hand[] hands;
    public Hand dealer;

    private int handIndex = 0;
    private bool blackjack = false;
    private bool got30 = false;
    private bool dealerBlackjack = false;

    private void Start () 
	{
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        doubleBtn.onClick.AddListener(() => DoubleClicked());
        standBtn.onClick.AddListener(() => StandClicked());
        splitBtn.onClick.AddListener(() => Split());
        RulesBtn.onClick.AddListener(() => Rules());
        RulesExitBtn.onClick.AddListener(() => ExitRules());

        resultText.gameObject.SetActive(false);
    
        arrows[0].enabled = false;
        arrows[1].enabled = false;

        dealer.gameObject.SetActive(false);
    }

    /// <summary>
    /// Command to bring up rules on screen
    /// </summary>
    private void Rules()
    {
        RulesCanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// Command to exit rules
    /// </summary>
    private void ExitRules()
    {
        RulesCanvas.gameObject.SetActive(false);
    }
    

    
    /// <summary>
    /// Splits pairs, and royals
    /// </summary>
    private void Split()
    {
        hands[1].gameObject.SetActive(true);

        cashController.UpdateCash(hands[0].GetBet() * -1);
        hands[1].SetBet(hands[0].GetBet());

        Card grabbedCard = hands[0].GrabCard();
        hands[1].Deal(grabbedCard);
        splitBtn.interactable = false;
        arrows[0].enabled = true;
    }


    /// <summary>
    /// Comman ran when deal clicked
    /// </summary>
    private void DealClicked()
    {
        if (cashController.CanPlay())
        {
            handIndex = 0;

            cashController.ButtonsEnable(false);
            hands[0].gameObject.SetActive(true);
            dealer.gameObject.SetActive(true);

            deck.Shuffle();
            ResetHands();

            resultText.gameObject.SetActive(false);
            dealBtn.interactable = false;

            hands[0].gameObject.SetActive(true);
            dealer.gameObject.SetActive(true);

            hands[0].SetBet(cashController.GetBet());

            for (int i = 0; i < 2; i++)
            {
                hands[0].Deal(deck.DealCard());
                dealer.Deal(deck.DealCard(), isDealer: true);
            }

            if (hands[0].GetHandValue() == 21)
            {
                blackjack = true;
            }

            if (dealer.GetHandValue() == 21 &&
                dealer.pos[1].GetValue() == 1)
            {
                dealerBlackjack = true;
            }

            if (blackjack || dealerBlackjack)
            {
                StandClicked();
            }
            else
            {
                if (hands[0].pos[0].GetValue() == hands[0].pos[1].GetValue())
                {
                    splitBtn.interactable = true;
                }
                hitBtn.interactable = true;
                doubleBtn.interactable = true;
                standBtn.interactable = true;
            }
        }
        else
        {
            resultText.gameObject.SetActive(true);
            resultText.text = "Bet must be higher than $0.";
        }
    }

    private void HitClicked()
    {
        doubleBtn.interactable = false;
        splitBtn.interactable = false;
        standBtn.interactable = true;

        hands[handIndex].Deal(deck.DealCard());

        if (hands[handIndex].Is30())
        {
            got30 = true;
            StandClicked();
        }
        else if (hands[handIndex].GetBust() || hands[handIndex].GetHandValue() == 21)
        {
            StandClicked();
        }
    }

    /// <summary>
    /// Command ran when double clicked.
    /// </summary>
    private void DoubleClicked()
    {
        cashController.UpdateCash(hands[handIndex].GetBet() * -1);
        hands[handIndex].SetBet(hands[handIndex].GetBet() * 2);
        HitClicked();
        StandClicked();
    }

    /// <summary>
    /// Command ran when stand clicked
    /// </summary>
    private void StandClicked()
    {
        if (hands[1].gameObject.activeSelf && handIndex == 0)
        {
            arrows[0].enabled = false;
            arrows[1].enabled = true;
            doubleBtn.interactable = true;
            standBtn.interactable = false;
            handIndex++;
        }
        else
        {
            arrows[1].enabled = false;
            DealersTurn();
        }
    }

    /// <summary>
    /// Yields turn to dealer letting dealer finish game
    /// </summary>
    private void DealersTurn()
    {
        standBtn.interactable = false;
        hitBtn.interactable = false;
        doubleBtn.interactable = false;

        dealer.Flip();

        while (!blackjack && !hands[0].GetBust() &&  dealer.GetHandValue() < 17 &&
            dealer.GetHandValue() <= hands[0].GetHandValue())
        {
            dealer.Deal(deck.DealCard());
        }

        if (hands[1].gameObject.activeSelf && !hands[1].GetBust())
        {
            while ( dealer.GetHandValue() <= hands[1].GetHandValue())
            {
                dealer.Deal(deck.DealCard());
            }
        }

        ResolveGame();
    }

    /// <summary>
    /// Final state of game where players hand(s) get compared with dealers hands
    /// </summary>
    private void ResolveGame()
    {
        int amountWon = 0;
        int dealerScore = dealer.GetHandValue();

        if (dealer.GetBust())
        {
            dealer.SetResult(" (BUST)");
        }

        if (got30)
        {
            resultText.gameObject.SetActive(true);
            amountWon = hands[0].GetBet() * 6;
            resultText.text = "Got 30 in 3! Player Won $" + amountWon;
            cashController.UpdateCash(amountWon);
        }

        else if (blackjack && !dealerBlackjack)
        {
            resultText.gameObject.SetActive(true);
            amountWon = hands[0].GetBet() * 3;
            resultText.text = "BlackJack! Player Won $" + amountWon;
            cashController.UpdateCash(amountWon);
        }

        else if (dealerBlackjack && !blackjack)
        {
            resultText.gameObject.SetActive(true);
            resultText.text = "Lost! Dealer BlackJack!";
        }

        else
        {
            for (int i = 0; i < handIndex + 1; i++)
            {
                int handScore = hands[i].GetHandValue();
                if (hands[i].GetBust())
                {
                    hands[i].SetResult(" (BUST)");
                }
                else if (!dealer.GetBust() && handScore < dealerScore)
                {
                    hands[i].SetResult(" (LOST)");
                }
                else if (handScore == dealerScore)
                {
                    hands[i].SetResult(" (PUSH)");
                    amountWon += hands[i].GetBet() * 1;
                }
                else
                {
                    hands[i].SetResult(" (WON!)");
                    amountWon += hands[i].GetBet() * 2;
                }
            }

            resultText.gameObject.SetActive(true);

            if (amountWon > 0)
            {
                resultText.text = "Player Won $" + amountWon;
                cashController.UpdateCash(amountWon);
            }
            else
            {
                resultText.text = "Lost! Dealer Won :(";
            }

            cashController.CheckBet();
        }
        GameReset();
      
        
    }

    /// <summary>
    /// Resets all hands on table to default
    /// </summary>
    private void ResetHands() {
        dealer.Reset();
        hands[0].Reset();
        hands[1].Reset();
        hands[1].gameObject.SetActive(false);
    }


    /// <summary>
    /// Resets game.
    /// </summary>
    private void GameReset()
    {
        blackjack = false;
        dealerBlackjack = false;
        got30 = false;

        cashController.ButtonsEnable(true);
        dealBtn.interactable = true;
        splitBtn.interactable = false;
    }

}

