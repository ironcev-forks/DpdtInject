﻿using DpdtInject.Injector.Compilation;
using DpdtInject.Injector.Excp;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DpdtInject.Tests
{
    internal class FakeDiagnosticReporter : IDiagnosticReporter
    {
        public int ErrorCount
        {
            get;
            private set;
        }

        public int WarningCount
        {
            get;
            private set;
        }

        public int InfoCount
        {
            get;
            private set;
        }

        public List<Exception> Exceptions
        {
            get;
        } = new List<Exception>();

        public FakeDiagnosticReporter()
        {
            ErrorCount = 0;
            WarningCount = 0;
            InfoCount = 0;
        }

        public void ReportError(string title, string message)
        {
            Debug.WriteLine(title);
            Debug.WriteLine(message);
            Debug.WriteLine(string.Empty);
            ErrorCount++;
        }

        public void ReportWarning(string title, string message)
        {
            Debug.WriteLine(title);
            Debug.WriteLine(message);
            Debug.WriteLine(string.Empty);
            WarningCount++;
        }

        public void ReportInfo(string title, string message)
        {
            Debug.WriteLine(title);
            Debug.WriteLine(message);
            Debug.WriteLine(string.Empty);
            InfoCount++;
        }

        internal void ReportException(Exception excp)
        {
            if (excp is null)
            {
                throw new ArgumentNullException(nameof(excp));
            }

            ErrorCount++;
            Exceptions.Add(excp);

            var prefix = "";
            while (excp != null)
            {
                Debug.WriteLine(prefix + excp.Message);
                Debug.WriteLine(prefix + excp.StackTrace);

                excp = excp.InnerException;
                prefix += "        ";
            }
            Debug.WriteLine(string.Empty);
        }

        public DpdtException GetDpdtException(int index = 0)
        {
            return GetException<DpdtException>(index);
        }

        public T GetException<T>(int index = 0)
            where T : Exception
        {
            return (T)Exceptions[index];
        }

    }
}