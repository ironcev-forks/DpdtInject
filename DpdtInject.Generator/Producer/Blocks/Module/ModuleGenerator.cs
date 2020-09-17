﻿using DpdtInject.Generator.Helpers;
using DpdtInject.Generator.Producer.Blocks.Binding;
using DpdtInject.Generator.Producer.Blocks.Exception;
using DpdtInject.Generator.Producer.Blocks.Provider;
using DpdtInject.Injector;
using DpdtInject.Injector.Beautify;
using DpdtInject.Injector.Compilation;
using DpdtInject.Injector.Excp;
using DpdtInject.Injector.Helper;
using DpdtInject.Injector.Module;
using DpdtInject.Injector.Module.Bind;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using System.Threading;

namespace DpdtInject.Generator.Producer.Blocks.Module
{
    internal class ModuleGenerator
    {
        private readonly IDiagnosticReporter _diagnosticReporter;

        public INamedTypeSymbol ModuleSymbol
        {
            get;
        }

        public string ModuleTypeNamespace => ModuleSymbol.ContainingNamespace.GetFullName();

        public string ModuleTypeName => ModuleSymbol.Name;

        public ModuleGenerator(
            IDiagnosticReporter diagnosticReporter,
            INamedTypeSymbol moduleSymbol
            )
        {
            if (diagnosticReporter is null)
            {
                throw new ArgumentNullException(nameof(diagnosticReporter));
            }

            if (moduleSymbol is null)
            {
                throw new ArgumentNullException(nameof(moduleSymbol));
            }
            _diagnosticReporter = diagnosticReporter;
            ModuleSymbol = moduleSymbol;
        }

        public string GetClassBody(
            InstanceContainerGeneratorsContainer container
            )
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.BindingsContainer.AnalyzeForCircularDependencies(_diagnosticReporter);
            container.BindingsContainer.AnalyzeForSingletonTakesTransient(_diagnosticReporter);

            var providerGenerator = new ProviderGenerator(
                container
                );

            var result = @$"
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DpdtInject.Injector;
using DpdtInject.Injector.Excp;
using DpdtInject.Injector.Module;
using DpdtInject.Injector.Module.Bind;

{container.InstanceContainerGenerators.Join(sc => sc.Usings.Join(c => c))}

namespace {ModuleTypeNamespace}
{{
#nullable enable
    public partial class {ModuleTypeName} : {nameof(DpdtModule)}
    {{
        private static long _instanceCount = 0L;

        private static readonly Provider _provider;
        private static readonly {typeof(ReinventedContainer).FullName} _typeContainerGet;
        private static readonly {typeof(ReinventedContainer).FullName} _typeContainerGetAll;
        //private static readonly {typeof(Beautifier).FullName} _beautifier;

        //public static {typeof(Beautifier).FullName} Beautifier => _beautifier;

        static {ModuleTypeName}()
        {{
            _provider = new Provider(
                );

            _typeContainerGet = new {typeof(ReinventedContainer).FullName}(
                {container.GetReinventedContainerArgument("Get")}
                );
            _typeContainerGetAll = new {typeof(ReinventedContainer).FullName}(
                {container.GetReinventedContainerArgument("GetAll")}
                );
            //_beautifier = new {typeof(Beautifier).FullName}(
            //    this
            //    );
        }}

        public {ModuleTypeName}()
        {{
            if(Interlocked.Increment(ref _instanceCount) > 1L)
            {{
                throw new DpdtException(DpdtExceptionTypeEnum.GeneralError, ""Module should not be instanciated more that once. This is a Dpdt's design axiom."");
            }}
        }}


        public override void Dispose()
        {{
            {container.InstanceContainerGenerators.Where(icg => icg.BindingContainer.Scope.In(BindScopeEnum.Singleton)).Join(sc => sc.DisposeClause + ";")}
        }}

        public T Get<T>()
        {{
            return ((IBaseProvider<T>)_provider).Get();
        }}
        public List<T> GetAll<T>()
        {{
            return ((IBaseProvider<T>)_provider).GetAll();
        }}

        public object Get({typeof(Type).FullName} requestedType)
        {{
            var result = _typeContainerGet.{nameof(ReinventedContainer.GetGetObject)}(requestedType);

            return result;
        }}
        public IEnumerable<object> GetAll({typeof(Type).FullName} requestedType)
        {{
            var result = _typeContainerGetAll.{nameof(ReinventedContainer.GetGetObject)}(requestedType);

            return (IEnumerable<object>)result;
        }}

        private class Provider
            {providerGenerator.CombinedInterfaces}
        {{
            //public Provider()
            //{{
            //}}

            {providerGenerator.CombinedImplementationSection}
        }}
#nullable disable

    {container.InstanceContainerGenerators.Join(sc => sc.GetClassBody(container))}

    }}

}}
";
            return result;
        }

    }
}