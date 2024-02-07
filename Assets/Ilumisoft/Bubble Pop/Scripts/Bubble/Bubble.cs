namespace Ilumisoft.BubblePop
{
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(MoveBehaviour))]
    public class Bubble : MonoBehaviour
    {
        
        public SpriteRenderer spriteRenderer = null;

        [SerializeField] 
        Animator animator = null;

        [SerializeField] 
        Collider2D inputCollider = null;

        /// <summary>
        /// Reference to the move behaviour, which performs all movement
        /// </summary>
        MoveBehaviour moveBehaviour;

        /// <summary>
        /// Callback invoked when the bubble gets destroyed
        /// </summary>
        public UnityAction<Bubble> OnDestroy { get; set; } = null;

        /// <summary>
        /// Gets the color of the bubble
        /// </summary>
        public Color Color => spriteRenderer.color;

        /// <summary>
        /// Returns true if the bubble is detroyed, false otherwise
        /// </summary>
        public bool IsDestroyed { get; private set; }

        /// <summary>
        /// Returns true if the bubble is moving, false otherwise
        /// </summary>
        public bool IsMoving => moveBehaviour.IsMoving;

        private void Awake()
        {
            moveBehaviour = GetComponent<MoveBehaviour>();
        }

        /// <summary>
        /// Disables the collider of the bubble
        /// </summary>
        public void EnableCollider()
        {
            this.inputCollider.enabled = true;
        }

        /// <summary>
        /// Enables the collider of the bubble
        /// </summary>
        public void DisableCollider()
        {
            this.inputCollider.enabled = false;
        }

        /// <summary>
        /// Triggers the select animation
        /// </summary>
        public void PlaySelectAnimation()
        {
            animator.SetTrigger("Select");
        }

        /// <summary>
        /// Triggers the deselect animation
        /// </summary>
        public void PlayDeselectAnimation()
        {
            animator.SetTrigger("Deselect");
        }

        /// <summary>
        /// Triggers the pop animation
        /// </summary>
        public void PlayPopAnimation()
        {
            animator.SetTrigger("Despawn");
        }

        /// <summary>
        /// Pops the bubble
        /// </summary>
        public void Pop()
        {
            PlayPopAnimation();
            
            // Mark the bubble as destroyed
            IsDestroyed = true;

            // Disable collider, so that the bubble cannot be selected with raycasts anymore
            DisableCollider();

            // Trigger OnDestroy event
            OnDestroy?.Invoke(this);

            // Destroy the game object after a second
            Destroy(this.gameObject, 1.0f);
        }

        /// <summary>
        /// Moves the bubble to the given position
        /// </summary>
        /// <param name="position"></param>
        public void MoveTo(Vector3 position)
        {
            moveBehaviour.MoveTo(position);
        }
    }
}