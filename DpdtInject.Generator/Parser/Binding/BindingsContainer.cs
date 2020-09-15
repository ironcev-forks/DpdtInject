﻿using DpdtInject.Generator.Parser.Binding.Graph;
using DpdtInject.Generator.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DpdtInject.Generator.Parser.Binding
{
    public class BindingsContainer
    {
        private readonly List<BindingContainer> _bindingContainers;

        public IReadOnlyList<BindingContainer> BindingContainers => _bindingContainers;

        public BindingsContainer(
            List<BindingContainer> bindingContainers
            )
        {
            if (bindingContainers is null)
            {
                throw new ArgumentNullException(nameof(bindingContainers));
            }
            _bindingContainers = bindingContainers;
        }

        public IReadOnlyList<BindingContainer> GetBindWith(
            string bindFromTypeFullName
            )
        {
            if (bindFromTypeFullName is null)
            {
                throw new ArgumentNullException(nameof(bindFromTypeFullName));
            }

            return
                _bindingContainers.FindAll(bc => bc.FromTypeFullNames.Contains(bindFromTypeFullName));
        }

        public BindingContainerGroups ConvertToGroups()
        {
            return new BindingContainerGroups(_bindingContainers);
        }

        internal void AnalyzeForCircularDependencies(
            IDiagnosticReporter diagnosticReporter
            )
        {
            if (diagnosticReporter is null)
            {
                throw new ArgumentNullException(nameof(diagnosticReporter));
            }

            new CycleChecker(ConvertToGroups())
                .CheckForCycles(diagnosticReporter)
                ;
        }
    }
}
