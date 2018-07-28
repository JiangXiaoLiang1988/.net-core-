using DependencyInjectionDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> ListAll();

        void Register(User user);

        string GetUserTitle();

        string GetRegisterTitle();
    }
}
