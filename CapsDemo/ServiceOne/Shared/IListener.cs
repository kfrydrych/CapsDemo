using System.Threading.Tasks;
using DotNetCore.CAP;

namespace CapsDemo.ServiceOne.Shared
{
    public interface IListener<in TEvent> : ICapSubscribe
    {
        Task Handle(TEvent @event);
    }
}