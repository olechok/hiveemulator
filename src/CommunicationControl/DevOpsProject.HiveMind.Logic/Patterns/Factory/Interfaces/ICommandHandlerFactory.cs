using DevOpsProject.Shared.Models.HiveMindCommands;

namespace DevOpsProject.HiveMind.Logic.Patterns.Factory.Interfaces
{
    public interface ICommandHandlerFactory
    {
        Task HandleCommand(object command);
    }
}
