using System;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IRunAtStartup
    {
        Task OnStart(IServiceProvider serviceProvider);
    }
}
