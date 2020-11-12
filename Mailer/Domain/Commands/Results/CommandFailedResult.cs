using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Commands
{
    public class CommandFailedResult : CommandResult
    {
        public CommandFailedResult()
        {
            IsSuccessful = false;
        }

        public CommandFailedResult(IEnumerable<ValidationFailure> messages) : this()
        {
            Message = string.Join(Environment.NewLine, messages.Select(e => e.ErrorMessage));
        }

        public CommandFailedResult(string message) : this()
        {
            Message = message;
        }
    }
}