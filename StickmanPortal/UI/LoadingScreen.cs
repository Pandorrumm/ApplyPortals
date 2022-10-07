using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StickmanPortal
{
    public class LoadingScreen : Singleton<LoadingScreen>
    {
        [SerializeField] private Slider slider = null;
     
        [Space]
        [Header("Canvas Background Image")]
        [SerializeField] private Image backgroundLoading = null;
        [SerializeField] private Sprite backgroundGame = null;

        [Space]
        [Header("Slider Image")]
        [SerializeField] private Image sliderLoading = null;
        [SerializeField] private Sprite sliderGame = null;

        [Space]
        [SerializeField] private float newSliderMaxValue = 2f;

        [Space]
        [Header("Logo")]
        [SerializeField] private GameObject logoGame = null;

        private CanvasGroup canvasGroup;
        private Camera loadingScreenCamera;

        private void Awake()
        {
            if (HasInstance)
            {
                Destroy(gameObject);
            }
            else
            {
                InitInstance();
                DontDestroyOnLoad(gameObject);
            }

            canvasGroup = GetComponentInChildren<CanvasGroup>();
            loadingScreenCamera = GetComponent<Camera>();

            Utility.SetCanvasGroupEnabled(canvasGroup, false);
            loadingScreenCamera.enabled = false;
        }

        IEnumerator LoadingSceneProcess(string _nameScene)
        {
            Utility.SetCanvasGroupEnabled(canvasGroup, true);
            loadingScreenCamera.enabled = true;

            AsyncOperation operation = SceneManager.LoadSceneAsync(_nameScene);      

            while (!operation.isDone)
            {
                slider.value = operation.progress;
                yield return null;
            }

            Utility.SetCanvasGroupEnabled(canvasGroup, false);
            loadingScreenCamera.enabled = false;
        }

        public void LoadScene(string _nameScene)
        {
            StartCoroutine(LoadingSceneProcess(_nameScene));
        }

        public void ChangeAppearance()
        {
            backgroundLoading.sprite = backgroundGame;
            sliderLoading.sprite = sliderGame;
            logoGame.SetActive(false);
            slider.maxValue = newSliderMaxValue;
        }
    }
}
