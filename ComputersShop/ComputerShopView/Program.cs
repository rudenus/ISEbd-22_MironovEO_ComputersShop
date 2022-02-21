﻿using ComputerShopBusinessLogic.BusinessLogics;
using ComputerShopContracts.BusinessLogicContracts;
using ComputerShopContracts.StoragesContracts;
using ComputerShopFileImplement;
using ComputerShopFileImplement.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace ComputersShopView
{
    static class Program
    {
        private static IUnityContainer container = null;
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ApplicationExit += ApplicationExit;
            AppDomain.CurrentDomain.UnhandledException += (o, e) => { if (e.IsTerminating) ApplicationExit(null, null); };
            Application.ThreadException += (o, e) => { Application.Exit(); };

            Application.Run(Container.Resolve<FormMain>());
        }
        private static void ApplicationExit(object sender, EventArgs e)
        {
            FileDataListSingleton.SaveAll();//need to cut
        }
        private static IUnityContainer BuildUnityContainer()
    {
        var currentContainer = new UnityContainer();
        currentContainer.RegisterType<IComponentStorage,
        ComponentStorage>(new HierarchicalLifetimeManager());
        currentContainer.RegisterType<IOrderStorage, OrderStorage>(new
        HierarchicalLifetimeManager());
        currentContainer.RegisterType<IComputerStorage, ComputerStorage>(new
        HierarchicalLifetimeManager());
        currentContainer.RegisterType<IComponentLogic, ComponentLogic>(new
        HierarchicalLifetimeManager());
        currentContainer.RegisterType<IOrderLogic, OrderLogic>(new
        HierarchicalLifetimeManager());
        currentContainer.RegisterType<IComputerLogic, ComputerLogic>(new
        HierarchicalLifetimeManager());
        return currentContainer;
    }
}

}