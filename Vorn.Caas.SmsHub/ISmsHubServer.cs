internal interface ISmsHubServer
{
    Task SendSms(string phoneNumber, string text);
    Task SendCode(string phoneNumber, string code);
    Task<List<Sms>> LoadList(int? skips = null, int? takes = null);
    Task<Sms> Load(int id);
    Task<Sms> Reload(int id);
    Task<int> Count();
}
