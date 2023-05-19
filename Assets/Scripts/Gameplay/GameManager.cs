using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public List<Round>        Rounds;
        public BrainManager       BrainManager;
        public InteractionManager InteractionManager;

        public int       CurrentRound    = 1;
        public int       MaxAllowedRound = 3;
        public Timer     Timer;
        public RoundText RoundText;

        public static Action RoundStarted;

        private void OnEnable()
        {
            Timer.TimerEnd += EndRound;
        }

        private void OnDisable()
        {
            Timer.TimerEnd -= EndRound;
        }

        private void Start()
        {
            CurrentRound = 1;
            StartRound(Rounds[1]);
        }

        private void StartRound(Round round)
        {
            RoundText.SetRound(round.Index);
            InteractionManager.SetRound(round);
            Timer.StartTimer(round.Time);
            BrainManager.InitializeBrains();
            BrainManager.ChangeBrainsMovementStatus(true);
        }

        private void StartRoundWithDelay()
        {
            CurrentRound += 1;
            foreach (Round round in Rounds)
            {
                if (round.Index == CurrentRound)
                {
                    StartRound(round);
                }
            }
        }

        private void EndRound()
        {
            BrainManager.ChangeBrainsMovementStatus(false);
            Invoke(nameof(StartRoundWithDelay), 1f);
        }
    }
}