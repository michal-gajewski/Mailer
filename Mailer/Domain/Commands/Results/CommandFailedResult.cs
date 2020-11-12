using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Commands
{
    public class CommandFailedResult : CommandResult
    {
        public CommandFailedResult(IEnumerable<ValidationFailure> messages)
        {
            IsSuccessful = false;
            Message = string.Join(Environment.NewLine, messages.Select(e => e.ErrorMessage));
        }
    }
}