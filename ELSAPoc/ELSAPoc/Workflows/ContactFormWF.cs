using AutoMapper;
using Elsa.Activities.Http;
using Elsa.Activities.Http.Models;
using Elsa.Activities.Primitives;
using Elsa.Builders;
using ELSAPoc.Classes;

namespace ELSAPoc.Workflows
{
    /// <summary>
    /// This WF expose an API that accepts a POST request with a predefined format, rapresenting a contact form request.
    /// The request will be shipped via mail to a predefined mail address. 
    /// </summary>
    public class ContactFormWF : IWorkflow
    {
        private readonly IMapper _mapper;

        public ContactFormWF(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WithDisplayName("Generic contact form")
                // definition of the endpoint
                .HttpEndpoint(e => e
                    .WithPath("/api/contactform")
                    .WithMethod("POST")
                    .WithReadContent())
                .SetVariable(
                    "request",
                    ctx => ctx.GetInput<HttpRequestModel>()?.Body)
                .WriteHttpResponse(p => p
                .WithContent(ctx => _mapper.Map<ContactFormRequest>(ctx.GetVariable("request")))
                    ); ;
        }
    }
}