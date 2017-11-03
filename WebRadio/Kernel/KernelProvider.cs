using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRadio.Kernel
{
    public class KernelProvider
    {
        private static KernelProvider _instance = new KernelProvider();
        private StandardKernel _kernel;

        public KernelProvider()
        {
            _kernel = new StandardKernel(new MainModule());
        }

        public static KernelProvider Instance
        {
            get
            {
                return _instance;
            }
        }

        public T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _kernel.GetAll<T>();
        }
    }
}
