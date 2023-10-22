using UnityEngine;

namespace PacMan
{
    public abstract class GhostState : MonoBehaviour
    {
        public GhostController ghost { get; private set; }
        public float duration;

        private void Awake()
        {
            this.ghost = GetComponent<GhostController>();
        }

        public void Enable()
        {
            Enable(this.duration);
        }

        public virtual void Enable(float duration)
        {
            this.enabled = true;
            CancelInvoke();
            Invoke(nameof(Disable), duration);
        }

        public virtual void Disable()
        {
            this.enabled = false;
            CancelInvoke();
        }
    }
}
