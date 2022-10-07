using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StickmanPortal
{
    public class AutoScrollLevelSelect : MonoBehaviour
    {
        [SerializeField] private ScrollRect levelSelectScroll;
        [SerializeField] private float durationScrolling = 1f;
        [SerializeField] private int startScrollLevelevel = 10;

        private int currentLevel;
        private LevelsUpdater levelsUpdater;

        private void Start()
        {
            levelsUpdater = GetComponentInParent<LevelsUpdater>();
            currentLevel = IndexFirstClosedLevel();

            if (currentLevel > startScrollLevelevel)
            {
                StartCoroutine(AutoScroll(levelSelectScroll, 1f, 1f - currentLevel / (float)levelsUpdater.levels.Count, durationScrolling));
            }           
        }

        private IEnumerator AutoScroll(ScrollRect _scrollRect, float _startPosition, float _endPosition, float _duration)
        {
           // yield return new WaitForSeconds(0.5f);
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / _duration;
                _scrollRect.verticalNormalizedPosition = Mathf.Lerp(_startPosition, _endPosition, t);
                yield return null;
            }
        }

        private int IndexFirstClosedLevel()
        {
            int index;

            for (int i = 0; i < levelsUpdater.levels.Count; i++)
            {
                if (levelsUpdater.levels[i].GetClosedLevel().activeSelf)
                {
                    index = i;
                    return index;
                }              
            }
            return 0;
        }
    }
}
