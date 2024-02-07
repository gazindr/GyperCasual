namespace Ilumisoft.BubblePop
{
    using System.Collections;
    using UnityEngine;

    public class MoveBehaviour : MonoBehaviour
    {
        /// <summary>
        /// The velocity used when the behaviour moves
        /// </summary>
        [SerializeField]
        public float velocity = 4.0f;

        /// <summary>
        /// Returns true if the beahviour is still moving, false otherwise
        /// </summary>
        public bool IsMoving { get; private set; }

        private void Awake()
        {
            IsMoving = false;
        }

        public void MoveTo(Vector3 target)
        {
            if (velocity > 0)
            {
                StartCoroutine(MoveCoroutine(target, velocity));
            }
            else
            {
                Debug.LogWarning("Cannot move object, because velocity is set to zero", this);
            }
        }

        IEnumerator MoveCoroutine(Vector3 target, float velocity)
        {
            IsMoving = true;

            Vector3 origin = transform.position;

            float duration = Vector3.Distance(origin, target) / velocity;

            float timeElapsed = 0.0f;

            while (timeElapsed <= duration)
            {
                timeElapsed += Time.deltaTime;

                float t = Mathf.Clamp01(timeElapsed / duration);

                transform.position = Vector3.Lerp(origin, target, t);

                yield return null;
            }

            IsMoving = false;
        }
    }
}