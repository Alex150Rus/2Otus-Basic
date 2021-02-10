using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

internal sealed class GameController : MonoBehaviour
{
    public Button attackButton;
    public CanvasGroup buttonPanel;
    public CanvasGroup wonPanel;
    public CanvasGroup lostPanel;

    public Character[] playerCharacter;
    public Character[] enemyCharacter;

    public string WinSound = "Win";
    public string LostSound = "Lose";

    private Character currentTarget;
    private bool waitingForInput;
    private PlaySound _playSound;

    Character FirstAliveCharacter(Character[] characters)
    {
        return characters.FirstOrDefault(character => !character.IsDead());
    }

    void PlayerWon()
    {
        Utility.SetCanvasGroupEnabled(wonPanel, true);
        _playSound.Play(WinSound);
    }

    void PlayerLost()
    {
        Utility.SetCanvasGroupEnabled(lostPanel, true);
        _playSound.Play(LostSound);
    }

    bool CheckEndGame()
    {
        if (FirstAliveCharacter(playerCharacter) == null) {
            PlayerLost();
            return true;
        }

        if (FirstAliveCharacter(enemyCharacter) == null) {
            PlayerWon();
            return true;
        }

        return false;
    }

    //[ContextMenu("Player Attack")]
    public void PlayerAttack()
    {
        waitingForInput = false;
    }

    //[ContextMenu("Next Target")]
    public void NextTarget()
    {
        int index = Array.IndexOf(enemyCharacter, currentTarget);
        for (int i = 1; i < enemyCharacter.Length; i++) {
            int next = (index + i) % enemyCharacter.Length;
            if (!enemyCharacter[next].IsDead()) {
                currentTarget.targetIndicator.gameObject.SetActive(false);
                currentTarget = enemyCharacter[next];
                currentTarget.targetIndicator.gameObject.SetActive(true);
                return;
            }
        }
    }

    IEnumerator GameLoop()
    {
        yield return null;
        while (!CheckEndGame()) {
            foreach (var player in playerCharacter)
            {
                if (player.IsDead()) continue;
                currentTarget = FirstAliveCharacter(enemyCharacter);
                if (currentTarget == null)
                    break;

                currentTarget.targetIndicator.gameObject.SetActive(true);
                Utility.SetCanvasGroupEnabled(buttonPanel, true);

                waitingForInput = true;
                while (waitingForInput)
                    yield return null;

                Utility.SetCanvasGroupEnabled(buttonPanel, false);
                currentTarget.targetIndicator.gameObject.SetActive(false);

                player.target = currentTarget.transform;
                player.AttackEnemy();

                while (!player.IsIdle())
                    yield return null;

                break;
            }

            foreach (var enemy in enemyCharacter)
            {
                if (enemy.IsDead()) continue;
                Character target = FirstAliveCharacter(playerCharacter);
                if (target == null)
                    break;

                enemy.target = target.transform;
                enemy.AttackEnemy();

                while (!enemy.IsIdle())
                    yield return null;

                break;
            }
        }
    }
    
    void Start()
    {
        attackButton.onClick.AddListener(PlayerAttack);
        //выключаем в начале
        Utility.SetCanvasGroupEnabled(buttonPanel, false);
        Utility.SetCanvasGroupEnabled(wonPanel, false);
        Utility.SetCanvasGroupEnabled(lostPanel, false);
        StartCoroutine(GameLoop());
        _playSound = GetComponent<PlaySound>();
    }
}