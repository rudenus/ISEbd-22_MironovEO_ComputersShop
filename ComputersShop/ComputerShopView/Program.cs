using ComputerShopBusinessLogic.BusinessLogics;
using ComputerShopContracts.BusinessLogicContracts;
using ComputerShopContracts.StoragesContracts;
using ComputerShopDatabseImplement;
using ComputerShopDatabseImplement.Implements;
using ComputersShopBuisnessLogic.BusinessLogics;
using ComputersShopBuisnessLogic.MailWorker;
using ComputersShopBuisnessLogic.OfficePackage;
using ComputersShopBuisnessLogic.OfficePackage.Implements;
using ComputersShopContracts.BindingModels;
using ComputersShopContracts.BusinessLogicContracts;
using ComputersShopContracts.StoragesContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
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
            var mailSender = Container.Resolve<AbstractMailWorker>();
            //var MailLogin = ConfigurationManager.AppSettings["MailLogin"];
            //var MailPassword = ConfigurationManager.AppSettings["MailPassword"];
            //var SmtpClientHost = ConfigurationManager.AppSettings["SmtpClientHost"];
            //var SmtpClientPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientPort"]);
            //var PopHost = ConfigurationManager.AppSettings["PopHost"];
            //var PopPort = Convert.ToInt32(ConfigurationManager.AppSettings["PopPort"]);
            mailSender.MailConfig(new MailConfigBindingModel
            {
                MailLogin = ConfigurationManager.AppSettings["MailLogin"],
                MailPassword =
            ConfigurationManager.AppSettings["MailPassword"],
                SmtpClientHost =
            ConfigurationManager.AppSettings["SmtpClientHost"],
                SmtpClientPort =
            Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientPort"]),
                PopHost = ConfigurationManager.AppSettings["PopHost"],
                PopPort =
            Convert.ToInt32(ConfigurationManager.AppSettings["PopPort"])
            });
            
            // создаем таймер
            var timer = new System.Threading.Timer(new TimerCallback(MailCheck), null, 0,
           100000);

            Application.SetCompatibleTextRenderingDefault(false);


            Application.Run(Container.Resolve<FormMain>());
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
            currentContainer.RegisterType<IReportLogic, ReportLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ComputerSaveToExcel, SaveToExcel>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ComputerSaveToWord, SaveToWord>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ComputerSaveToPdf, SaveToPdf>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientLogic, ClientLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientStorage, ClientStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IImplementerLogic, ImplementerLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IImplementerStorage, ImplementerStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkProcess,WorkModeling > (new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkProcess, WorkModeling>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractMailWorker, MailKitWorker>(new
            SingletonLifetimeManager());
            currentContainer.RegisterType<IMessageInfoLogic, MessageInfoLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMessageInfoStorage, MessageInfoStorage>(new
            HierarchicalLifetimeManager());

            return currentContainer;
        }
        private static void MailCheck(object obj) =>
            Container.Resolve<AbstractMailWorker>().MailCheck();
    }

}
