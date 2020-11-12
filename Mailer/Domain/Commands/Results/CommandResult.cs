using System.Collections;
using System.Linq;

namespace Domain.Commands
{
    public class CommandResult
    {
        public bool IsSuccessful { get; protected set; }

        public string Message { get; protected set; }
    }
}