using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Controllers
{
    public interface IController<T>
    {
        void Create(T value);
        void Update(T value);
        void Delete(T valie);
    }
}
