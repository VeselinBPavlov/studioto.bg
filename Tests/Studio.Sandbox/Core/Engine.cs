namespace Studio.Sandbox.Core
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Contracts;

    public class Engine : IEngine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            //var initializeService = serviceProvider.GetService<IDatabaseInitializerService>();
            //initializeService.InitializeDatabase();

            var commandInterpreter = serviceProvider.GetService<ICommandInterpreter>();

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter command:");
                    string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string result = commandInterpreter.Read(input);
                    Console.WriteLine(result);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}