using Code.Services.Input;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure
{
    public class GlobalScope : LifetimeScope
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterInput(builder);
        }

        private void RegisterInput(IContainerBuilder builder)
        {
            builder.Register<InputActions>(Lifetime.Singleton);
            builder.RegisterEntryPoint<InputService>();
        }
    }
}