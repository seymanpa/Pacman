using UnityEngine;

namespace PacMan
{
    public class MovementController : MonoBehaviour
    {
        public new Rigidbody2D rigidbody { get; private set; }

        [SerializeField]
        private float _speed;
        public float speedMultiplier;
        [SerializeField]
        private Vector2 _initialDirection;
        [SerializeField]
        private LayerMask _wallLayer;

        public Vector2 direction { get; set; }
        private Vector2 _nextDirection { get; set; }
        private Vector2 _startingPosition { get; set; }

        private void Awake()
        {
            this.rigidbody = GetComponent<Rigidbody2D>();
            this._startingPosition = this.transform.position;
        }

        private void Start()
        {
            ResetState();
        }

        public void ResetState()
        {
            this.speedMultiplier = 1f;
            this.direction = this._initialDirection;
            this._nextDirection = Vector2.zero;
            this.transform.position = this._startingPosition;
            this.rigidbody.isKinematic = false;
            this.enabled = true;
        }

        private void Update()
        {
            if (_nextDirection != Vector2.zero)
            {
                SetDirection(_nextDirection);
            }
        }

        private void FixedUpdate()
        {
            Vector2 position = this.rigidbody.position;
            Vector2 translation = this.direction * this._speed * this.speedMultiplier * Time.fixedDeltaTime;
            this.rigidbody.MovePosition(position + translation);
        }

        public void SetDirection(Vector2 direction, bool forced = false)
        {
            if (forced || !HasWallCollision(direction))
            {
                this.direction = direction;
                this._nextDirection = Vector2.zero;
            }
            else
            {
                this._nextDirection = direction;
            }
        }

        public bool HasWallCollision(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this._wallLayer);
            return hit.collider != null;
        }
    }
}

