﻿using DpdtInject.Generator.BindExtractor;
using DpdtInject.Generator.Helpers;
using DpdtInject.Generator.Producer;
using DpdtInject.Generator.Producer.Factory;
using DpdtInject.Generator.Scanner;
using DpdtInject.Generator.TypeInfo;
using DpdtInject.Injector;
using DpdtInject.Injector.Compilation;
using DpdtInject.Injector.Excp;
using DpdtInject.Injector.Helper;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;

namespace DpdtInject.Generator
{
    public class DpdtInternalGenerator
    {
        private readonly IDiagnosticReporter _diagnosticReporter;

        public DpdtInternalGenerator(
            IDiagnosticReporter diagnosticReporter
            )
        {
            if (diagnosticReporter is null)
            {
                throw new ArgumentNullException(nameof(diagnosticReporter));
            }

            _diagnosticReporter = diagnosticReporter;
        }

        public void Execute(
            ITypeInfoContainer typeInfoContainer,
            string? generatedFolder
            )
        {
            using (new DTimer(_diagnosticReporter, "Dpdt total time taken"))
            {
                ExecutePrivate(
                    typeInfoContainer,
                    generatedFolder
                );
            }
        }

        private void ExecutePrivate(
            ITypeInfoContainer typeInfoContainer,
            string? generatedFolder
            )
        {
            if (typeInfoContainer is null)
            {
                throw new ArgumentNullException(nameof(typeInfoContainer));
            }

            var scanner = new TimedTypeScanner(
                _diagnosticReporter,
                new DefaultTypeScanner(
                    )
                );

            var clusterTypes = scanner.Scan(
                typeInfoContainer
                );

            for(var clusterTypeIndex = 0; clusterTypeIndex < clusterTypes.Count; clusterTypeIndex++)
            {
                var clusterType = clusterTypes[clusterTypeIndex];

                MethodDeclarationSyntax loadMethodSyntax;
                CompilationUnitSyntax? compilationUnitSyntax;
                using (new DTimer(_diagnosticReporter, "unsorted time taken"))
                {
                    var loadMethods = clusterType.GetMembers(nameof(DefaultCluster.Load));
                    if (loadMethods.Length != 1)
                    {
                        throw new Exception($"Something wrong with type {clusterType.ToDisplayString()}");
                    }

                    var loadMethod = loadMethods[0];

                    var loadMethodRefs = loadMethod.DeclaringSyntaxReferences;
                    if (loadMethodRefs.Length != 1)
                    {
                        throw new Exception($"Something wrong with method {loadMethod.ToDisplayString()} : {loadMethodRefs.Length}");
                    }

                    var loadMethodRef = loadMethodRefs[0];

                    loadMethodSyntax = (MethodDeclarationSyntax)loadMethodRef.GetSyntax();
                    compilationUnitSyntax = loadMethodSyntax.Root<CompilationUnitSyntax>();
                }

                if (compilationUnitSyntax == null)
                {
                    throw new DpdtException(
                        DpdtExceptionTypeEnum.InternalError,
                        $"Unknown problem to access to compilation unit syntax"
                        );
                }

                var bindExtractor = new TimedBindExtractor(
                    _diagnosticReporter,
                    new DefaultBindExtractor(
                        typeInfoContainer,
                        compilationUnitSyntax
                        )
                    );

                bindExtractor.Visit(loadMethodSyntax);

                var clusterBindings = bindExtractor.GetClusterBindings(
                    clusterType
                    );

                clusterBindings.BuildFlags(
                    );

                clusterBindings.Analyze(
                    _diagnosticReporter
                    );

                var factoryProducer = new FactoryProducer(
                    typeInfoContainer,
                    clusterBindings
                    );

                foreach (var factoryProduct in factoryProducer.Produce())
                {
                    var fileName = $"{factoryProduct.FactoryType.ToDisplayString().EscapeSpecialTypeSymbols()}.Pregenerated.cs";

                    ModificationDescription factoryModificationDescription;
                    using (new DTimer(_diagnosticReporter, $"Dpdt factory {factoryProduct.FactoryType.ToDisplayString()} beautify generated code time taken"))
                    {
                        factoryModificationDescription = new ModificationDescription(
                            clusterType,
                            fileName,
                            factoryProduct.SourceCode,
                            generatedFolder is not null
                            );
                    }

                    if (generatedFolder is not null)
                    {
                        var generatedFilePath = Path.Combine(generatedFolder, fileName);

                        factoryModificationDescription.SaveToDisk(
                            generatedFilePath
                            );
                    }

                    typeInfoContainer.AddSource(factoryModificationDescription);
                }


                var clusterProducer = new ClusterProducer(
                    typeInfoContainer,
                    clusterBindings
                    );

                var moduleSourceCode = clusterProducer.Produce();

                ModificationDescription modificationDescription;
                using (new DTimer(_diagnosticReporter, "Dpdt cluster beautify generated code time taken"))
                {
                    modificationDescription = new ModificationDescription(
                        clusterType,
                        clusterType.Name + Guid.NewGuid().RemoveMinuses() + "_1.cs",
                        moduleSourceCode,
                        generatedFolder is not null
                        );
                }

                if (generatedFolder is not null)
                {
                    var generatedFilePath = Path.Combine(generatedFolder, $"{clusterType.Name}.Pregenerated{clusterTypeIndex}.cs");

                    modificationDescription.SaveToDisk(
                        generatedFilePath
                        );
                }

                typeInfoContainer.AddSource(modificationDescription);
            }
        }
    }
}
