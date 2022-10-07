using UnityEngine;
using DG.Tweening;
using System;

namespace StickmanPortal
{
    public class EnemyRotator : MonoBehaviour
    {
        [SerializeField] private GameObject target = null;
        [SerializeField] private float offsetAngle = 0.05f;

        private Animator enemyAnimator;

        public static Action BulletShiftEvent;

        private void Start()
        {
            enemyAnimator = GetComponentInChildren<Animator>();

            RotateToTarget(target);                  
        }

        private void RotateToTarget(GameObject _target)
        {
            Vector2 dir = (_target.transform.position - transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (angle > 90f)
            {
                angle = 180f - angle;
            }

            enemyAnimator.SetLayerWeight(1, (angle / Mathf.Rad2Deg) + offsetAngle);

            if (_target.transform.position.x < gameObject.transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }

            DOTween.Sequence()
                    .AppendInterval(0.5f)
                    .AppendCallback(() => BulletShiftEvent?.Invoke());
        }
    }
}
