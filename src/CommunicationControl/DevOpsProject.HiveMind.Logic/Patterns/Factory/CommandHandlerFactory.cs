using DevOpsProject.HiveMind.Logic.Patterns.Command.Interfaces;
using DevOpsProject.HiveMind.Logic.Patterns.Factory.Interfaces;
using Microsoft.Extensions.Logging;

namespace DevOpsProject.HiveMind.Logic.Patterns.Factory
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CommandHandlerFactory> _logger;

        public CommandHandlerFactory(IServiceProvider serviceProvider, ILogger<CommandHandlerFactory> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task HandleCommand(object command)
        {
            var runtimeType = command.GetType();
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(runtimeType);

            // TODO: OPTIONS:
            // 1. We can use switch by runtimeType - to explicitly get implementation - for example, .GetService<ICommandHandler<MoveHiveMindCommand>>()
            // 2. We can use base type HiveMindCommand CommandType property - to use this (enum) as switch and to get implementation like in p. 1
            // 3. Current approach - fully dynamic BUT a bit messy - uses command, not type safe at compile time, etc
            var handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                _logger.LogError("No handler found for type: {type}", runtimeType.Name);
                throw new NotSupportedException($"Handler not found for {runtimeType.Name}");
            }

            _logger.LogInformation("Handling command of type {type}", runtimeType.Name);

            var handleMethod = handlerType.GetMethod("HandleAsync");
            if (handleMethod == null)
            {
                throw new InvalidOperationException($"Handler for {runtimeType.Name} does not implement HandleAsync()");
            }

            var task = (Task)handleMethod.Invoke(handler, new[] { command })!;
            await task;
        }
    }

}
