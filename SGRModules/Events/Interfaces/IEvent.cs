using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SGRModules.Events.Interfaces
{
    public interface IEvent
    {
        IEnumerable<_Exception> Exceptions { get; }

        void AddException( _Exception Ex );
    }
}
