using DevOpsProject.Shared.Models.HiveMindCommands;

namespace DevOpsProject.HiveMind.Logic.Patterns.Command.Interfaces
{
    public interface ICommandHandler<T>
    {
        Task HandleAsync(T command);
    }
}
