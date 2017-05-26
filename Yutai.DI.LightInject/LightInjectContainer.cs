﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using LightInject;
using Yutai.Plugins.Mvp;


namespace Yutai.DI.LightInject
{
    public class LightInjectContainer : IApplicationContainer
    {
        private readonly ServiceContainer _container = new ServiceContainer();

        public LightInjectContainer()
        {
            _container.RegisterInstance<IApplicationContainer>(this);
        }

        public IApplicationContainer RegisterView<TView, TImplementation>()
            where TView : class, IView
            where TImplementation : class, TView
        {
            _container.Register<TView, TImplementation>();
            return this;
        }

        public IApplicationContainer RegisterInstance(Type type, object instance)
        {
            _container.RegisterInstance(type, instance);
            return this;
        }

        public IApplicationContainer RegisterInstance<TService>(object instance)
                where TService : class
        {
            _container.RegisterInstance<TService>(instance as TService);
            return this;
        }

        public IApplicationContainer RegisterService<TService>() where TService : class
        {
            _container.Register<TService>();
            return this;
        }

        public IApplicationContainer RegisterService<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>();
            return this;
        }

        public TService GetInstance<TService>() where TService : class
        {
            if (!IsRegistered<TService>())
            {
                _container.Register<TService>();
            }
            return _container.GetInstance<TService>();
        }

        /// <summary>
        /// Gets an instance of particular type. Registers this type with transient life time if needed.
        /// </summary>
        public object GetInstance(Type type)
        {
            if (!_container.CanGetInstance(type, string.Empty))
            {
                _container.Register(type);
            }

            return _container.GetInstance(type);
        }

        public IApplicationContainer RegisterSingleton<TService>() where TService : class
        {
            _container.Register<TService>(new PerContainerLifetime());
            return this;
        }

        public IApplicationContainer RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>(new PerContainerLifetime());
            return this;
        }

        public bool Run<TPresenter, TArgs>(TArgs arg, IWin32Window parent = null) where TPresenter : class, IPresenter<TArgs>
        {
            var presenter = GetInstance<TPresenter>();
            return presenter.Run(arg, parent);
        }

        public bool Run<TPresenter>(IWin32Window parent = null) where TPresenter : class, IPresenter
        {
            var presenter = GetInstance<TPresenter>();
            return presenter.Run(parent);
        }

        public TService Resolve<TService>() where TService : class
        {
            return _container.GetInstance<TService>();
        }

        public TService GetSingleton<TService>() where TService : class
        {
            if (!IsRegistered<TService>())
            {
                _container.Register<TService>(new PerContainerLifetime());
            }
            return _container.GetInstance<TService>();
        }

        private bool IsRegistered<TService>()
        {
            return _container.CanGetInstance(typeof(TService), string.Empty);
        }
    }
}