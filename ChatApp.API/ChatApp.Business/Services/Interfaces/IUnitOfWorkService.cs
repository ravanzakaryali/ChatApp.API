namespace ChatApp.Business.Services.Interfaces
{
    public interface IUnitOfWorkService
    {
        IMessageService MessageService { get; }
        IUserService UserService { get; }
    }
}
