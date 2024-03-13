using System.Collections.Immutable;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HHGlobal.Features.Tax.Calculate;

[ApiController]
[Route("api/[controller]")]
public class CalculateTaxForJobsController
{
    [HttpPost]
    public async Task<CalculateTaxForJobs.Response> CalculateTaxForJobs([FromServices] ISender sender,
        [FromBody] CalculateTaxForJobs.RequestBody requestBody)
    {
        return await sender.Send(new CalculateTaxForJobs.Request(requestBody
        ));
    }
}

public static class CalculateTaxForJobs
{
    public record Request(RequestBody RequestBody) : IRequest<Response>;

    public record Response(IEnumerable<ResponseJob> ResponseJobs);

    public record RequestBody(List<IncomingJob> IncomingJobs);

    internal sealed class CalculateTaxForJobsHandler : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var responseJobs = new List<ResponseJob>();

            foreach (var incomingJob in request.RequestBody.IncomingJobs)
            {
                var marginPercentage = incomingJob.ExtraMargin ? 16 : 11;
                var resultItems = incomingJob.Items.Select(x =>
                    (Name: x.Name, PriceAfterTax: x.Exempt ? x.Price : x.Price * (decimal)(1 + 7 / 100.00),
                        Margin: x.Price * marginPercentage / 100)).ToImmutableArray();

                var total = resultItems.Select(x => decimal.Round(x.PriceAfterTax + x.Margin,2, MidpointRounding.ToZero)).Sum();

                responseJobs.Add(new ResponseJob(total,
                    resultItems.Select(x => new ResponseItem(x.Name, decimal.Round(x.PriceAfterTax, 2, MidpointRounding.AwayFromZero)))));
                
            }

            return new Response(responseJobs);
        }
    }
}