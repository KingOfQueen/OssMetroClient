using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using ossClient.Framework;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition;

namespace ossClient
{
    class AppBootstrapper  : Bootstrapper<IShell>
    {
        private CompositionContainer container;

        protected override void Configure()
        {
            ViewLocator.NameTransformer.AddRule
(
@"(?<nsbefore>([A-Za-z_]\w*\.)*)?(?<nsvm>ViewModels\.)(?<nsafter>([A-Za-z_]\w*\.)*)(?<basename>[A-Za-z_]\w*)(?<suffix>ViewModel$)",
@"${nsbefore}Views.${nsafter}${basename}View",
@"(([A-Za-z_]\w*\.)*)?ViewModels\.([A-Za-z_]\w*\.)*[A-Za-z_]\w*ViewModel$"
);

            container = new CompositionContainer(new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));

            CompositionBatch batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(container);

            container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
            {
                return exports.First();
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }
    }
}
