using System.Net;
using System.Text;

namespace PortfolioSite.Web.Tests;

// Answers each path with the JSON the test gives it, and remembers the requests.
public class StubHttpMessageHandler : HttpMessageHandler
{
    private readonly Dictionary<string, string> _responses = new();
    public List<HttpRequestMessage> Requests { get; } = [];

    public void Respond(string path, string json) => _responses[path] = json;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Requests.Add(request);
        var response = _responses.TryGetValue(request.RequestUri!.AbsolutePath, out var json)
            ? new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json, Encoding.UTF8, "application/json") }
            : new HttpResponseMessage(HttpStatusCode.NotFound);
        return Task.FromResult(response);
    }
}
