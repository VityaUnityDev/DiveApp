public  class PlayerMoneyState
{
    private float _currentMoney;
    public readonly MoneyState MoneyState;

    public PlayerMoneyState(float money)
    {
        _currentMoney = money;
        MoneyState = StateMoney(money);
    }


    private MoneyState StateMoney(float money)
    {
        if (money > GameInfo.minBetInGame && money < GameInfo.Bet)
            return MoneyState.DontPlayerInCurrentGame;

        else if (money >= GameInfo.Bet)
            return MoneyState.PlayCurrentGame;
        
        else if (money < GameInfo.minBetInGame)
            return MoneyState.GameOver;

        return MoneyState.None;
    }
}