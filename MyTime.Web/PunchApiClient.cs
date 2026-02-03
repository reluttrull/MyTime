using MyTime.Shifts.Contracts.Responses;

namespace MyTime.Web;

public class PunchApiClient(HttpClient httpClient)
{
    public async Task<PunchResponse[]> GetAllPunchesAsync(CancellationToken cancellationToken = default)
    {
        List<PunchResponse>? punches = null;

        await foreach (var punch in httpClient.GetFromJsonAsAsyncEnumerable<PunchResponse>("/punches/me", cancellationToken))
        {
            if (punch is not null)
            {
                punches ??= [];
                punches.Add(punch);
            }
        }

        await Task.Delay(500, cancellationToken);
        return punches?.ToArray() ?? [];
    }
}