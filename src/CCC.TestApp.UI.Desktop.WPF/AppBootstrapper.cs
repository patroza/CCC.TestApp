using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using CCC.TestApp.Core.Application.Services;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.Core.Domain.Entities;
using CCC.TestApp.Infrastructure.DAL.Repositories;
using CCC.TestApp.UI.Desktop.ViewModels;

namespace CCC.TestApp.UI.Desktop.WPF
{
    public class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        CompositionContainer _container;

        protected override void Configure() {
            SetupViewNamespaces();
            BuildContainer();
        }

        protected override IEnumerable<Assembly> SelectAssemblies() {
            return new[] {
                Assembly.GetExecutingAssembly(), // UI.WPF - Self
                typeof (ScreenBase).Assembly, // UI
                typeof (MockUserRepository).Assembly, // DAL
                typeof (UserInteractor).Assembly, // Core App
                typeof (User).Assembly // Core Domain
                //typeof (IWindowManager).Assembly
            };
        }

        protected override object GetInstance(Type serviceType, string key) {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = _container.GetExportedValues<object>(contract);

            var enumerable = exports as object[] ?? exports.ToArray();
            if (enumerable.Any())
                return enumerable.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType) {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance) {
            _container.SatisfyImportsOnce(instance);
        }

        void BuildContainer() {
            var rb = new RegistrationBuilder();
            SetupRegistrationBuilder(rb);
            var catalog = new AggregateCatalog(SelectAssemblies().Select(x => new AssemblyCatalog(x, rb)));
            var container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
            var batch = new CompositionBatch();
            SetupBatch(batch, container);
            _container = container;
        }

        static void SetupBatch(CompositionBatch batch, CompositionContainer container) {
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventBus>(new EventBus());
            container.Compose(batch);
        }

        static void SetupRegistrationBuilder(RegistrationBuilder rb) {
            rb.ForType<SixMapper>()
                .Export<IMapper>();
            SetupUI(rb);
            SetupDAL(rb);
            SetupCore(rb);
        }

        static void SetupUI(RegistrationBuilder rb) {
            rb.ForTypesMatching(x => x.Name.EndsWith("View"))
                .Export();

            rb.ForTypesMatching(x => x.Name.EndsWith("Controller"))
                .Export()
                .ExportInterfaces();

            rb.ForTypesMatching(x => x.Name.EndsWith("ViewModel"))
                .Export()
                .ExportInterfaces();

            rb.ForTypesMatching(x => x.Name.EndsWith("ViewModelsFactory"))
                .ExportProperties(x => true);
        }

        static void SetupCore(RegistrationBuilder rb) {
            rb.ForTypesMatching(x => x.Name.EndsWith("Interactor"))
                .ExportInterfaces();
        }

        static void SetupDAL(RegistrationBuilder rb) {
            rb.ForTypesMatching(x => x.Name.EndsWith("Repository"))
                .ExportInterfaces();
        }

        static void SetupViewNamespaces() {
            ViewLocator.AddNamespaceMapping("CCC.TestApp.UI.Desktop.ViewModels", "CCC.TestApp.UI.Desktop.WPF.Views");
            ViewLocator.AddNamespaceMapping("CCC.TestApp.UI.Desktop.ViewModels.Users",
                "CCC.TestApp.UI.Desktop.WPF.Views.Users");
        }
    }
}