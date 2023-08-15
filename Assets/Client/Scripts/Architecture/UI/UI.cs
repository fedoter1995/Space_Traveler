namespace Architecture
{
    public class UI<T> where T : IUIController
    {
        public ComponentsBase<IUIController> controllers { get; private set; }

        public UI(string[] references)
        {
            controllers = new ComponentsBase<IUIController>(references);
        }

        public bool HaveComponent(T controller)
        {
            return controllers.HaveComponent<T>();
        }
    }
}