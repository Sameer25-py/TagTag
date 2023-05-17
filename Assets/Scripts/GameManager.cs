using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TagTag
{
    public class GameManager : MonoBehaviour
    {
        public List<Round>        Rounds;
        public BrainManager       BrainManager;
        public InteractionManager InteractionManager;

        public int   CurrentRound    = 1;
        public int   MaxAllowedRound = 3;
        public Timer Timer;

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
            StartRound(Rounds[0]);
        }

        private void StartRound(Round round)
        {
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