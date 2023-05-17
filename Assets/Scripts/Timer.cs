using System;
using TMPro;
using UnityEngine;

namespace TagTag
{
    public class Timer : MonoBehaviour
    {
        public float    countDownTime = 45;
        public TMP_Text CountDownText;

        private bool  _isCountDownInProgress;
        private float _elapsedTime;
        private float _remainingTime;

        public static Action        TimerEnd;
        public static Action<float> TimerProgressed;

        private float _lastSentprogress = 0f;

        private void DecrementTimer()
        {
            _remainingTime -= 1f;
            int seconds = Mathf.FloorToInt(_remainingTime % 60f);
            int minutes = Mathf.FloorToInt(_remainingTime / 60f);
            CountDownText.text = $"{minutes:00}:{seconds:00}";

            float progress = (float)Math.Round(1f - (_remainingTime / countDownTime), 1);

            if (_remainingTime == 0f)
            {
                _isCountDownInProgress = false;
                TimerEnd();
            }
            else
            {
                if (progress != _lastSentprogress)
                {
                    TimerProgressed(progress);
                    _lastSentprogress = progress;
                }
                
            }
        }

        private void Update()
        {
            if (!_isCountDownInProgress) return;
            _elapsedTime += Time.deltaTime;
            if (!(_elapsedTime >= 1f)) return;
            _elapsedTime = 0f;
            DecrementTimer();
        }

        public void StartTimer(float duration)
        {
            countDownTime  = duration;
            _remainingTime = countDownTime;
            int seconds = Mathf.FloorToInt(_remainingTime % 60f);
            int minutes = Mathf.FloorToInt(_remainingTime / 60f);
            CountDownText.text     = $"{minutes:00}:{seconds:00}";
            _isCountDownInProgress = true;
        }

        public void PauseTimer()
        {
            _isCountDownInProgress = false;
        }

        public void ResumeTimer()
        {
            _isCountDownInProgress = true;
        }
    }
}