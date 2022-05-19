using ChatApp.Business.Services.Interfaces;

namespace ChatApp.Business.Services.Implementations
{
    public interface IUnitOfWorkService
    {
        IMessageService MessageService { get; }
    }
}
