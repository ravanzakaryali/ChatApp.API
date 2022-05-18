namespace ChatApp.Business.Services.Interfaces
{
    public interface IRabbitMqService
    {
        void SendMessage(object obj);
        void SendMessage(string message);
    }
}
