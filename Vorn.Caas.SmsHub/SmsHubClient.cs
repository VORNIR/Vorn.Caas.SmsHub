using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
internal class SmsHubClient : HubClient<SmsHubConfiguration>, ISmsHub
{
    public EventHandler<Sms> Received { get; set; }
    public SmsHubClient(IOptions<SmsHubConfiguration> options, IEntityHubClient<Sms> entityHub) : base(options.Value)
    {
        entityHub.NotifyChanges += (s, c) =>
        {
            foreach(var entity in c)
            {
                if(entity.State == EntityState.Added && entity.Entity.Incoming)
                    Received?.Invoke(this, entity.Entity);
            }
        };
    }
    public Task<List<Sms>> LoadList(int? skips = null, int? takes = null) => HubConnection.InvokeAsync<List<Sms>>(nameof(ISmsHubServer.LoadList), skips, takes);
    public Task<Sms> Load(int id) => HubConnection.InvokeAsync<Sms>(nameof(ISmsHubServer.Load), id);
    public Task<Sms> Reload(int id) => HubConnection.InvokeAsync<Sms>(nameof(ISmsHubServer.Reload), id);
    public Task SendSms(string phoneNumber, string text) => HubConnection.InvokeAsync(nameof(ISmsHubServer.SendSms), phoneNumber, text);
    public Task SendCode(string phoneNumber, string code) => HubConnection.InvokeAsync(nameof(ISmsHubServer.SendCode), phoneNumber, code);
    public Task<int> Count() => HubConnection.InvokeAsync<int>(nameof(ISmsHubServer.Count));
}
