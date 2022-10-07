using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StickmanPortal
{
    public class PauseTimeBooster : MonoBehaviour
    {
        [SerializeField] private float stoppingTime = 10f;
        [SerializeField] private Timer timer = null;

        [Space]
        public float timeCounter;

        [Header("Booster Timer")]
        [SerializeField] private GameObject boosterTimer = null;
        [SerializeField] private Image uiFill = null;
        [SerializeField] private TMP_Text boosterTimerText = null;

        private Button pauseTimeButton;

        private void Start()
        {
            pauseTimeButton = GetComponent<Button>();
            pauseTimeButton.onClick.AddListener(StopTimer);

            boosterTimer.SetActive(false);
        }

        private void StopTimer()
        {           
            PauseTimer(stoppingTime);
        }

        public void PauseTimer(float _pauseTime)
        {
            timer.StopTimer();
            pauseTimeButton.interactable = false;

            float currentTime = timer.timerSlider.value;
            StartCoroutine(PauseTimer(_pauseTime, currentTime));           
        }

        private IEnumerator PauseTimer(float _pauseTime, float _currentTime)
        {
            timeCounter = _pauseTime;
            boosterTimer.SetActive(true);

            while (timeCounter > 0)
            {
                timeCounter -= Time.deltaTime;

                boosterTimerText.text = timeCounter.ToString("00");
                uiFill.fillAmount = Mathf.InverseLerp(0, stoppingTime, timeCounter);

                if (timeCounter <= 0)
                {
                    timeCounter = 0;
                    timer.startTime = _currentTime;
                    timer.StartTimer();

                    pauseTimeButton.interactable = true;
                    boosterTimer.SetActive(false);
                }

                yield return null;
            }
        }
    }
}
