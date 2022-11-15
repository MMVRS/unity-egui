#if UNITY_EDITOR

namespace Build1.UnityEGUI.Renderer
{
    public abstract class Renderer<T>
    {
        protected T Data { get; private set; }

        public void Render(T data)
        {
            Data = data;
            
            OnEGUI(data);
        }

        protected abstract void OnEGUI(T param);
    }
}

#endif