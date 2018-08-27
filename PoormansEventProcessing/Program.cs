using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoormansEventProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
			ServiceLocator serviceLocator = new ServiceLocator();

            var impls = serviceLocator.GetService<IHandler<SomeEvent>>();

			RunParallel(impls);
            RunSequential(impls);

            Console.WriteLine("The end!");
            Console.Read();
        }

        public static void RunParallel(IList<IHandler<SomeEvent>> impls)
        {
			Console.WriteLine("Running in parallel mode.");
            Parallel.ForEach(impls, (impl) => impl.Handle(new SomeEvent("hello world!")));
        }

        public static void RunSequential(IList<IHandler<SomeEvent>> impls)
        {
			Console.WriteLine("Running in sequential mode.");
            foreach (var impl in impls)
            {
                impl.Handle(new SomeEvent("hello"));
            }
        }
    }


	class ServiceLocator
    {
        IDictionary<object, IList<object>> _services;

        public ServiceLocator()
        {
            _services = new Dictionary<object, IList<object>>();

            RegisterHandlers();
        }

        public IList<T> GetService<T>()
        {
            try
            {
                return _services[typeof(T)].Cast<T>().ToList();
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("No Such service");
            }
        }

        private void RegisterHandlers()
        {
            var assembly = typeof(Program).Assembly;

            var classTypes = assembly.ExportedTypes.Select(t => t).Where(t => t.IsClass && !t.IsAbstract);

            IList<object> values;

            foreach (var type in classTypes)
            {
                var interfaces = type.GetInterfaces();

                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<>)))
                {

                    if (_services.ContainsKey(handlerType) == false)
                    {
                        values = new List<object>();
                        _services.Add(handlerType, values);
                    }
                    else
                    {
                        _services.TryGetValue(handlerType, out values);
                    }
                    values.Add(Activator.CreateInstance(type));

                    //Console.WriteLine("Registering: {0}, {1}", handlerType, type);
                }
            }
        }
    }
}
