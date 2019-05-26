using System.Threading.Tasks;

namespace BeepBong.Application.Commands
{
    public interface ICommand<T>
    {
        void SendCommand(T viewModel);
        Task SendCommandAsync(T viewModel);
    }
}