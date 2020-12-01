using MediatR;
using XeroDemoApp.Domain.Commands.Results;

namespace XeroDemoApp.Domain.Commands
{
    public class CreateAccessTokenCommand : IRequest<CommandResult>
    {
        public string Code { get; protected set; }
        public string State { get; protected set; }

        public CreateAccessTokenCommand(string code, string state) => (State, Code) = (state, code);

        public static CreateAccessTokenCommand Create(string code, string state) =>
            new CreateAccessTokenCommand(code, state);
    }
}
