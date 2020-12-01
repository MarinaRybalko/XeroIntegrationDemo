﻿namespace XeroDemoApp.Domain.Commands.Results
{
    public class CommandResult
    {
        private CommandResult() { }
        private CommandResult(string failureReason) => FailureReason = failureReason;
       
        public string FailureReason { get; }
        public bool IsSuccess => string.IsNullOrEmpty(FailureReason);

        public static CommandResult Success { get; } = new CommandResult();
        public static CommandResult Fail(string reason) => new CommandResult(reason);

        public static implicit operator bool(CommandResult result) => result.IsSuccess;
    }
}
