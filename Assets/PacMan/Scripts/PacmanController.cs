using UnityEngine;

namespace PacMan
{
    public class PacmanController : MonoBehaviour
    {
        public AnimationController deathSequence;
        public SpriteRenderer spriteRenderer { get; private set; }
        public new Collider2D collider { get; private set; }
        private MovementController _movement { get; set; }


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<Collider2D>();
            this._movement = GetComponent<MovementController>();
        }

        private void Update()
        {
            

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                _movement.SetDirection(Vector2.up);
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                _movement.SetDirection(Vector2.down);
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                _movement.SetDirection(Vector2.left);
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                _movement.SetDirection(Vector2.right);

            float angle = Mathf.Atan2(_movement.direction.y, _movement.direction.x);
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        } 

        public void ResetState()
        {
            this.gameObject.SetActive(true);
            this._movement.ResetState();
            enabled = true;
            spriteRenderer.enabled = true;
            collider.enabled = true;
            deathSequence.enabled = false;
            deathSequence.spriteRenderer.enabled = false;
        }

        public void DeathSequence()
        {
            enabled = false;
            spriteRenderer.enabled = false;
            collider.enabled = false;
            _movement.enabled = false;
            deathSequence.enabled = true;
            deathSequence.spriteRenderer.enabled = true;
            deathSequence.Restart();
        }
    }
}
