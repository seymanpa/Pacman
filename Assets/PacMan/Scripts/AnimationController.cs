using UnityEngine;

namespace PacMan
{
    public class AnimationController : MonoBehaviour
    {

        public SpriteRenderer spriteRenderer { get; set; }
        [SerializeField]
        private Sprite[] sprites = new Sprite[0];
        [SerializeField]
        private float animationTime = 0.25f;
        [SerializeField]
        private int animationFrame { get; set; }
        [SerializeField]
        private bool loop = true;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(Advance), animationTime, animationTime);
        }

        private void Advance()
        {
            if (!spriteRenderer.enabled)
            {
                return;
            }

            animationFrame++;

            if (animationFrame >= sprites.Length && loop)
                animationFrame = 0;

            if (animationFrame >= 0 && animationFrame < sprites.Length)
                spriteRenderer.sprite = sprites[animationFrame];
        }

        public void Restart()
        {
            animationFrame = -1;
            Advance();
        }
    }
}