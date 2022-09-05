using Microsoft.AspNetCore.SignalR;
internal class SmsHubServer : Hub, ISmsHubServer
{
    private readonly ISmsService smsService;
    public SmsHubServer(ISmsService smsService)
    {
        this.smsService = smsService;
    }
    public Task SendSms(string phoneNumber, string text) => smsService.SendSms(phoneNumber, text);
    public Task SendCode(string phoneNumber, string code) => smsService.SendCode(phoneNumber, code);
    public Task<Sms> Load(int id) => smsService.Load(id);
    public Task<Sms> Reload(int id) => smsService.Reload(id);
    public Task<List<Sms>> LoadList(int? skips = null, int? takes = null) => smsService.Load(skips, takes);
    public Task<int> Count() => smsService.Count();
}
