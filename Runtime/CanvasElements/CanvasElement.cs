using UnityEngine;

namespace QuickEye.Utility
{
    public abstract class CanvasElement<TContext> : MonoBehaviour
    {
        protected TContext Context { get; private set; }

        public virtual void Initialize(TContext context)
        {
            Context = context;
        }
    }
}