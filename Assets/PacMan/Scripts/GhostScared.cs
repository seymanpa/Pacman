using UnityEngine;

namespace PacMan
{
    public class GhostScared : GhostState
    {
        [SerializeField]
        private SpriteRenderer body;
        [SerializeField]
        private SpriteRenderer eyes;
        [SerializeField]
        private SpriteRenderer blue;
        [SerializeField]
        private SpriteRenderer white;

        private bool _eaten { get; set; }

        public override void Enable(float duration)
        {
            base.Enable(duration);

            this.body.enabled = false;
            this.eyes.enabled = false;
            this.blue.enabled = true;
            this.white.enabled = false;
            Invoke(nameof(Flash), duration / 2.0f);
        }

        public override void Disable()
        {
            base.Disable();

            body.enabled = true;
            eyes.enabled = true;
            blue.enabled = false;
            white.enabled = false;
        }

        private void Eaten()
        {
            _eaten = true;
            ghost.SetPosition(ghost.ghostsBase.inside.position);
            ghost.ghostsBase.Enable(duration);

            body.enabled = false;
            eyes.enabled = true;
            blue.enabled = false;
            white.enabled = false;
        }
        private void Flash()
        {
            if(!this._eaten)
            {
                this.blue.enabled=false;
                this.white.enabled=true;
                this.white.GetComponent<AnimationController>().Restart();
            }
        }

        private void OnEnable()
        {
            this.blue.GetComponent<AnimationController>().Restart();
            this.ghost.movement.speedMultiplier = 0.5f;
            this._eaten = false;
        }

        private void OnDisable()
        {
            this.ghost.movement.speedMultiplier = 1.0f;
            this._eaten = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Node node = collision.GetComponent<Node>();
            if (node != null && enabled)
            {
                Vector2 direction = Vector2.zero;
                float maxDistance = float.MinValue;

                foreach( Vector2 possibleDirection in node.possibleDirections)
                {
                    Vector3 newPosition = transform.position + new Vector3(possibleDirection.x, possibleDirection.y);
                    float distance = (this.ghost.pacman.position - newPosition).sqrMagnitude;
                    if (distance > maxDistance)
                    {
                        direction = possibleDirection;
                        maxDistance = distance;
                    }
                }

                this.ghost.movement.SetDirection(direction);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            {
                if (enabled)
                    Eaten();
            }
        }
    }
}

