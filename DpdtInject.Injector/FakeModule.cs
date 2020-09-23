﻿using DpdtInject.Injector.Beautify;
using DpdtInject.Injector.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpdtInject.Injector
{
    public class FakeCluster
    {
        public string DeclaredClusterType => null!;

        public bool IsRootCluster => true;

        public IBeautifier Beautifier = null!;

        public bool IsRegisteredFrom<TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }


        public TRequestedType Get<TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }


        public List<TRequestedType> GetAll<TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }


        public object Get(Type requestedType)
        {
            throw new NotImplementedException("This method should not be executed");
        }

        public IEnumerable<object> GetAll(Type requestedType)
        {
            throw new NotImplementedException("This method should not be executed");
        }
    }

    public class FakeModule  : IDisposable
    {
        public bool IsRegisteredFrom<TCluster, TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }

        public bool IsRegisteredFrom<TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }


        public TRequestedType Get<TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }
        public TRequestedType Get<TCluster, TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }


        public List<TRequestedType> GetAll<TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }
        public List<TRequestedType> GetAll<TCluster, TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }



        public object Get(Type requestedType)
        {
            throw new NotImplementedException("This method should not be executed");
        }
        public object Get<TCluster>(Type requestedType)
        {
            throw new NotImplementedException("This method should not be executed");
        }

        public IEnumerable<object> GetAll(Type requestedType)
        {
            throw new NotImplementedException("This method should not be executed");
        }
        public IEnumerable<object> GetAll<TCluster>(Type requestedType)
        {
            throw new NotImplementedException("This method should not be executed");
        }



        public void Dispose()
        {
            throw new NotImplementedException("This method should not be executed");
        }
    }

    public class FakeModule<T>  : IDisposable
        where T : DpdtModule
    {
        public bool IsRegisteredFrom<TCluster, TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }

        public bool IsRegisteredFrom<TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }


        public TRequestedType Get<TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }
        public TRequestedType Get<TCluster, TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }


        public List<TRequestedType> GetAll<TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }
        public List<TRequestedType> GetAll<TCluster, TRequestedType>()
        {
            throw new NotImplementedException("This method should not be executed");
        }



        public object Get(Type requestedType)
        {
            throw new NotImplementedException("This method should not be executed");
        }
        public object Get<TCluster>(Type requestedType)
        {
            throw new NotImplementedException("This method should not be executed");
        }

        public IEnumerable<object> GetAll(Type requestedType)
        {
            throw new NotImplementedException("This method should not be executed");
        }
        public IEnumerable<object> GetAll<TCluster>(Type requestedType)
        {
            throw new NotImplementedException("This method should not be executed");
        }


        public void Dispose()
        {
            throw new NotImplementedException("This method should not be executed");
        }
    }
}
