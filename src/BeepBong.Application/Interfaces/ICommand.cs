using System.Threading.Tasks;

namespace BeepBong.Application.Interfaces
{
    public interface ICommand<T>
    {
        void SendCommand(T viewModel);
        Task SendCommandAsync(T viewModel);
    }
}