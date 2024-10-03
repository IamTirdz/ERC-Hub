using ERC.Hub.Business.Common.Models;
using ERC.Hub.Business.Employees.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace ERC.Hub.Function
{
    public class EmployeesFunction(IMediator mediator, ILogger<EmployeesFunction> logger)
    {
        [Function("NewEmployee")]
        [OpenApiOperation(operationId: "NewEmployee", tags: new[] { "Employees" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateEmployeeDto), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(long), Description = "Created")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ErrorResponse), Description = "Bad Request")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.UnprocessableContent, contentType: "application/json", bodyType: typeof(ErrorResponse), Description = "Unprocessable Content")]
        public async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Function, "post", Route = "employees")] HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var model = JsonConvert.DeserializeObject<CreateEmployeeDto>(requestBody)!;

            var response = await mediator.Send(new CreateEmployeeCommand(model));
            return new CreatedResult($"employees/{response}", response);
        }
    }
}
