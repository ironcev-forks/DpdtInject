﻿using DpdtInject.Injector;
using DpdtInject.Injector.Module;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DpdtInject.Binding.Example.Zero
{
    //public partial class D2pdt2Kernel0 : D2pdt2Kernel
    //{

    //}

    public partial class D2pdt2Module0 : DpdtModule
    {
        public const string SomeString = "hello guys!";

        private readonly Provider _provider = new Provider();

        public override void Dispose()
        {
            IC_C_Singleton_Container_31B71770_8AF4_4D0B_9F1F_21DB4BF17C96.DoDisposeIfApplicable();
            IB_B_Singleton_Container_E0C46AC9_14EA_4142_A7D5_17460AA7AB90.DoDisposeIfApplicable();
            IA1_IA2_A_Singleton_Container_AC5CC60D_A385_4A7B_A741_4089E650CEB5.DoDisposeIfApplicable();
        }

        public T Get<T>()
        {
            return ((IBaseProvider<T>)_provider).Get();
        }


        public List<T> GetAll<T>()
        {
            return ((IBaseProvider<T>)_provider).GetAll();
        }


        public class ResolutionContext { }


        private class Provider :
            IBaseProvider<IA1>,
            IBaseProvider<IA2>,
            IBaseProvider<IB>,
            IBaseProvider<IC>
        {
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            IA1 IBaseProvider<IA1>.Get()
            {
                if(IA1_IA2_A_Singleton_Container_AC5CC60D_A385_4A7B_A741_4089E650CEB5.CheckPredicate(null))
                {
                    return IA1_IA2_A_Singleton_Container_AC5CC60D_A385_4A7B_A741_4089E650CEB5.GetInstance();
                }

                throw new InvalidOperationException();
            }
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            List<IA1> IBaseProvider<IA1>.GetAll()
            {
                var result = new List<IA1>();
                
                if (IA1_IA2_A_Singleton_Container_AC5CC60D_A385_4A7B_A741_4089E650CEB5.CheckPredicate(null))
                {
                    result.Add(IA1_IA2_A_Singleton_Container_AC5CC60D_A385_4A7B_A741_4089E650CEB5.GetInstance());
                }

                return result;
            }



            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            IA2 IBaseProvider<IA2>.Get()
            {
                if (IA1_IA2_A_Singleton_Container_AC5CC60D_A385_4A7B_A741_4089E650CEB5.CheckPredicate(null))
                {
                    return IA1_IA2_A_Singleton_Container_AC5CC60D_A385_4A7B_A741_4089E650CEB5.GetInstance();
                }

                throw new InvalidOperationException();
            }
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            List<IA2> IBaseProvider<IA2>.GetAll()
            {
                var result = new List<IA2>();

                if (IA1_IA2_A_Singleton_Container_AC5CC60D_A385_4A7B_A741_4089E650CEB5.CheckPredicate(null))
                {
                    result.Add(IA1_IA2_A_Singleton_Container_AC5CC60D_A385_4A7B_A741_4089E650CEB5.GetInstance());
                }

                return result;
            }


            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            IB IBaseProvider<IB>.Get()
            {
                if (IB_B_Singleton_Container_E0C46AC9_14EA_4142_A7D5_17460AA7AB90.CheckPredicate(null))
                {
                    return IB_B_Singleton_Container_E0C46AC9_14EA_4142_A7D5_17460AA7AB90.GetInstance();
                }

                throw new InvalidOperationException();
            }
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            List<IB> IBaseProvider<IB>.GetAll()
            {
                var result = new List<IB>();

                if (IB_B_Singleton_Container_E0C46AC9_14EA_4142_A7D5_17460AA7AB90.CheckPredicate(null))
                {
                    result.Add(IB_B_Singleton_Container_E0C46AC9_14EA_4142_A7D5_17460AA7AB90.GetInstance());
                }

                return result;
            }


            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            IC IBaseProvider<IC>.Get()
            {
                if (IC_C_Singleton_Container_31B71770_8AF4_4D0B_9F1F_21DB4BF17C96.CheckPredicate(null))
                {
                    return IC_C_Singleton_Container_31B71770_8AF4_4D0B_9F1F_21DB4BF17C96.GetInstance();
                }

                throw new InvalidOperationException();
            }
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            List<IC> IBaseProvider<IC>.GetAll()
            {
                var result = new List<IC>();

                if (IC_C_Singleton_Container_31B71770_8AF4_4D0B_9F1F_21DB4BF17C96.CheckPredicate(null))
                {
                    result.Add(IC_C_Singleton_Container_31B71770_8AF4_4D0B_9F1F_21DB4BF17C96.GetInstance());
                }

                return result;
            }

        }



        public sealed class IC_C_Singleton_Container_31B71770_8AF4_4D0B_9F1F_21DB4BF17C96
        {
            private volatile static Action _currentDisposeAction = null;
            private volatile static Action _realDisposeAction =
                () =>
                {
                    if (Nested.Instance is IDisposable disposableInstance)
                    {
                        disposableInstance.Dispose();
                    }
                };

            private IC_C_Singleton_Container_31B71770_8AF4_4D0B_9F1F_21DB4BF17C96()
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool CheckPredicate(ResolutionContext resolutionContext)
            {
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static C GetInstance()
            {
                Interlocked.Exchange(ref _currentDisposeAction, _realDisposeAction);

                return Nested.Instance;
            }

            public static void DoDisposeIfApplicable()
            {
                Interlocked.Exchange(ref _currentDisposeAction, null)?.Invoke();
            }

            private class Nested
            {
                // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
                static Nested()
                {
                }

                internal static readonly C Instance = new C(
                    IB_B_Singleton_Container_E0C46AC9_14EA_4142_A7D5_17460AA7AB90.GetInstance()
                    );
            }
        }

        public sealed class IB_B_Singleton_Container_E0C46AC9_14EA_4142_A7D5_17460AA7AB90
        {
            private volatile static Action _currentDisposeAction = null;
            private volatile static Action _realDisposeAction =
                () =>
                {
                    if (Nested.Instance is IDisposable disposableInstance)
                    {
                        disposableInstance.Dispose();
                    }
                };

            private IB_B_Singleton_Container_E0C46AC9_14EA_4142_A7D5_17460AA7AB90()
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool CheckPredicate(ResolutionContext resolutionContext)
            {
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static B GetInstance()
            {
                Interlocked.Exchange(ref _currentDisposeAction, _realDisposeAction);

                return Nested.Instance;
            }

            public static void DoDisposeIfApplicable()
            {
                Interlocked.Exchange(ref _currentDisposeAction, null)?.Invoke();
            }

            private class Nested
            {
                // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
                static Nested()
                {
                }

                internal static readonly B Instance = new B(
                    IA1_IA2_A_Singleton_Container_AC5CC60D_A385_4A7B_A741_4089E650CEB5.GetInstance()
                    );
            }
        }

        public sealed class IA1_IA2_A_Singleton_Container_AC5CC60D_A385_4A7B_A741_4089E650CEB5
        {
            private volatile static Action _currentDisposeAction = null;
            private volatile static Action _realDisposeAction =
                () =>
                {
                    if (Nested.Instance is IDisposable disposableInstance)
                    {
                        disposableInstance.Dispose();
                    }
                };

            private IA1_IA2_A_Singleton_Container_AC5CC60D_A385_4A7B_A741_4089E650CEB5()
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool CheckPredicate(ResolutionContext resolutionContext)
            {
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static A GetInstance()
            {
                Interlocked.Exchange(ref _currentDisposeAction, _realDisposeAction);

                return Nested.Instance;
            }

            public static void DoDisposeIfApplicable()
            {
                Interlocked.Exchange(ref _currentDisposeAction, null)?.Invoke();
            }

            private class Nested
            {
                // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
                static Nested()
                {
                }

                internal static readonly A Instance = new A(
                    //SomeString
                    );
            }
        }


    }


}

