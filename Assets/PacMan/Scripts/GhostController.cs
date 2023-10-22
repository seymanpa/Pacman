using UnityEngine;

namespace PacMan
{
    public class GhostController : MonoBehaviour
    {
        public MovementController movement {  get; private set; }
        public GhostBase ghostsBase { get; private set; }
        public GhostRandomMovement randomMovement { get; private set; }
        public GhostFollow follow { get; private set; }
        public GhostScared scared { get; private set; }
        [SerializeField]
        private GhostState _initialState;
        public Transform pacman;

        public int point = 200;

        private void Awake()
        {
            this.movement = GetComponent<MovementController>();
            this.ghostsBase = GetComponent<GhostBase>();
            this.randomMovement = GetComponent<GhostRandomMovement>();
            this.follow = GetComponent<GhostFollow>();
            this.scared = GetComponent<GhostScared>();
            this._initialState = GetComponent<GhostState>();
        }

        private void Start()
        {
            ResetState();
        }

        public void ResetState()
        {
            this.gameObject.SetActive(true);
            this.movement.ResetState();
            this.scared.Disable();
            this.randomMovement.Enable();
            this.follow.Disable();
            if (this.ghostsBase != this._initialState)
                this.ghostsBase.Disable();
            if(this._initialState != null)
                this._initialState.Enable();
        }

        public void SetPosition(Vector3 position)
        {
            position.z= transform.position.z;
            transform.position = position;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            {
                if (this.scared.enabled)
                    FindObjectOfType<GameManager>().GhostEaten(this);
                else
                    FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
