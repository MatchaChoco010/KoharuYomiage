﻿using System;
using System.Reflection;
using System.Windows;
using KoharuYomiageApp.Application.LoadTalker.Interfaces;
using KoharuYomiageApp.Application.LoadTalker.UseCases;
using KoharuYomiageApp.Infrastructures;
using KoharuYomiageApp.Infrastructures.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Mvvm;

namespace KoharuYomiageApp
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // DialogWindow
            containerRegistry.RegisterDialogWindow<DialogWindow>();

            // Views
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
            containerRegistry.RegisterForNavigation<Start>();

            // LoadTalker UseCase
            containerRegistry.RegisterSingleton<ILoadTalkerInputBoundary, LoadTalkerInteractor>();
            containerRegistry.RegisterManySingleton<LoadTalkerPresenter>(typeof(ILoadTalkerOutputBoundary), typeof(LoadTalkerPresenter));
            containerRegistry.RegisterSingleton<LoadTalkerController>();
            containerRegistry.RegisterDialog<LoadTalkerErrorDialogContent>();
            containerRegistry.RegisterDialog<LoadTalkerLinkDialogContent>();

            // Infrastructures
            containerRegistry.RegisterSingleton<CeVIOAIService>();
            Container.Resolve<CeVIOAIService>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Container.GetContainer().Dispose();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewName = viewType.Name;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = $"KoharuYomiageApp.Application.ViewModels.{viewName}ViewModel, {viewAssemblyName}";
                return Type.GetType(viewModelName);
            });
        }
    }
}
