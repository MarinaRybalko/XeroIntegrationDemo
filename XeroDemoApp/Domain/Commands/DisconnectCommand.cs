using MediatR;
using XeroDemoApp.Domain.Commands.Results;

namespace XeroDemoApp.Domain.Commands
{
    public class DisconnectCommand : IRequest<CommandResult>
    {
        public static DisconnectCommand Create() => new DisconnectCommand();
    }
}
