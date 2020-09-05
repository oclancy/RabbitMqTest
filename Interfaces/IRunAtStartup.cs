using System;

namespace Interfaces
{
    public interface IRunAtStartup
    {
        void OnStart(IServiceProvider serviceProvider);
    }
}
