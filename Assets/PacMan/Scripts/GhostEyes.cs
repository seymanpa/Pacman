using UnityEngine;

namespace PacMan
{
    public class GhostEyes : MonoBehaviour
    {
        [SerializeField]
        private Sprite up;
        [SerializeField]
        private Sprite down;
        [SerializeField]
        private Sprite left;
        [SerializeField]
        private Sprite right;

        public SpriteRenderer spriteRenderer { get; private set; }
        public MovementController movement { get; private set; }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            movement = GetComponentInParent<MovementController>();
        }

        private void Update()
        {
            if (movement.direction == Vector2.up)
            {
                spriteRenderer.sprite = this.up;
            }
            else if (movement.direction == Vector2.down)
            {
                spriteRenderer.sprite = this.down;
            }
            else if (movement.direction == Vector2.left)
            {
                spriteRenderer.sprite = this.left;
            }
            else if (movement.direction == Vector2.right)
            {
                spriteRenderer.sprite = this.right;
            }
        }
    }

}
