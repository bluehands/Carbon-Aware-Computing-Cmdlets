using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Management.Automation;

namespace CarbonAwareComputing.Cmdlets
{
    public class CmdletBase : PSCmdlet
    {
        protected void HandleError(string message, Exception ex)
        {
            WriteError(new ErrorRecord(new Exception(message), "ProcessingCommandError",
                ErrorCategory.InvalidOperation, null));
            if (ex == null) return;
            WriteError(new ErrorRecord(ex, "ProcessingCommandError",
                ErrorCategory.InvalidOperation, null));
            if (ex.InnerException != null)
            {
                WriteError(new ErrorRecord(ex.InnerException, "ProcessingCommandError",
                    ErrorCategory.InvalidOperation, null));
            }
        }
    }
}
