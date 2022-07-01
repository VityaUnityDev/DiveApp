using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{ 
    [SerializeField] private Transform _target;


    private int _result;
    private int _countEnter;
    private int _first;
    private int _max;
    private int _same;
    private float _timer;

    private CountPlayerInGameCommand _countPlayerInGameCommand;

    private CountBetCommand _countBetCommand;
    private MakeBetCommand _makeBetCommand;
    private EndGameCommand _endGameCommand;
    private CountMoneyCommand _countMoneyCommand;
    private HandOutMoneyCommand _moneyCommand;

    private void Awake()
    {
        _countPlayerInGameCommand = new CountPlayerInGameCommand();
        _countBetCommand = new CountBetCommand();
        _makeBetCommand = new MakeBetCommand();
        _endGameCommand = new EndGameCommand();
        _countMoneyCommand = new CountMoneyCommand();
        _moneyCommand = new HandOutMoneyCommand();
    }

    public void CountPlayerInGame()
    {
        _countPlayerInGameCommand.Execute();

        MakeBet();
        CountBetInGame();
        RollDices();
    }

    private void CountBetInGame() => _countBetCommand.Execute();

    private void MakeBet() => _makeBetCommand.Execute();


    private async void RollDices()
    {
        for (int i = 0; i < GameInfo.PlayersInCurrentGame.Count; i++)
        {
            int diceResult = 0;


            StartCoroutine(MoveDices(GameInfo.PlayersInCurrentGame.ElementAt(i).Value.playerView));
            foreach (var dice in GameInfo.PlayersInCurrentGame.ElementAt(i).Value.playerView.dicesInfo.dices)
            {
                var count = await dice.RollTheDice();
                diceResult += count;
            }

            var player = GameInfo.PlayersInCurrentGame.ElementAt(i);
            GameInfo.PlayerResult(player.Value, diceResult);
            await Task.Delay(1000);
            FindMaxDiceNumber(diceResult);
        }
    }


    private IEnumerator MoveDices(PlayerView playerView)
    {
        var startPos = playerView.dicesInfo.transfromDices.transform.position;
        var startLocalPos = playerView.dicesInfo.transfromDices.localPosition;
        var target = _target.GetComponent<RectTransform>();
        Vector3 newPos;
        float time = 0;
        while (time < 3f)
        {
            Debug.Log(startPos);  // в игре локальные,
            Debug.Log(target.transform.localPosition); // cовпадает
           // newPos = Vector3.Lerp(startLocalPos, target.transform.localPosition, time * 10);
            newPos = Vector3.Lerp(startLocalPos, target.localPosition, time * 10);

            playerView.dicesInfo.transfromDices.transform.position = newPos.normalized;
            time += Time.deltaTime;
            yield return null;
        }

        playerView.dicesInfo.transfromDices.localPosition = startLocalPos;
        yield return null;
    }


    private void FindMaxDiceNumber(int number)
    {
        _countEnter++;
        if (_countEnter == 1)
        {
            _first = number;
            _max = _first;
        }
        else
        {
            if (_first > number)
            {
                _first = _max;
            }
            else if (_first == number)
            {
                _same = number;
                _max = _same;
                _first = _max;
            }
            else if (_first < number)
            {
                _first = number;
                _max = number;
            }
        }

        StopCountMaxNumber();
    }

    private void StopCountMaxNumber()
    {
        if (GameInfo.PlayersInCurrentGame.Count == _countEnter)
        {
            GameInfo.winnerNumber = _max;
            FinishGame();
            _countEnter = 0;
        }
    }


    private void FinishGame()
    {
        _endGameCommand.Execute();
        CountMoney();
    }

    private void CountMoney()
    {
        _countMoneyCommand.Execute();
        HandOutMoney();
    }

    private void HandOutMoney() => _moneyCommand.Execute();
}


/*public class Email1
{
    public void Send()
    { }
}
public class Notification1
{
    private Email1 email;
    public Notification1()
    {
        email = new Email1();
    }

    public void EmailDistribution ()
    {
        email.Send();
    }
}

public interface IMessenger
{
    void Send();
}

public class Email : IMessenger
{
    public void Send()
    {
        // код для отправки email-письма
    }
}

public class SMS : IMessenger
{
    public void Send()
    {
        // код для отправки SMS
    }
}

// Уведомление
public class Notification
{
    private IMessenger _messenger;
    public Notification()
    {
        _messenger = new Email();
    }

    public void DoNotify()
    {
        _messenger.Send();
    }
}*/