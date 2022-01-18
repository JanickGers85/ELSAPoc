using Elsa.Activities.Console;
using Elsa.Activities.Temporal;
using Elsa.Builders;
using NodaTime;

namespace ELSAPoc.Workflows
{
    /// <summary>
    /// This WF simply print a message on the stdout, every 5 seconds
    /// </summary>
    public class RecurringBaseConsoleWF : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            var seconds = 5;     

            builder.Timer(Duration.FromSeconds(seconds))
                .WriteLine(() => $"I'm alive! And I run every {seconds} seconds!")
                .PersistWorkflow();
        }
    }
}
