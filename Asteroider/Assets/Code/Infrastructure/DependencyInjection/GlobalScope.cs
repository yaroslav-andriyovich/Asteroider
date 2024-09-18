using Code.Services.Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.DependencyInjection
{
    public class GlobalScope : LifetimeScope
    {
        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(this);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterInput(builder);
        }

        private void RegisterInput(IContainerBuilder builder)
        {
            builder.Register<InputActions>(Lifetime.Singleton);
            builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}