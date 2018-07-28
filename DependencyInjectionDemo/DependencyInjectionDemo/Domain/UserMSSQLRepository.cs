using DependencyInjectionDemo.Interface;
using DependencyInjectionDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Domain
{
    public class UserMSSQLRepository : IUserRepository
    {
        public List<User> listUser = new List<User>()
        {
          new User(){Id=1,UserName="张三MSSQL",Password="123" },
           new User(){Id=1,UserName="李四MSSQL",Password="1234" },
            new User(){Id=1,UserName="王五MSSQL",Password="1235" },
             new User(){Id=1,UserName="赵六MSSQL",Password="1236" }
        };

        public string GetRegisterTitle()
        {
            return "通过MSSQL注册用户数据";
        }

        public string GetUserTitle()
        {
            return "通过MSSQL获取所有数据";
        }

        public IEnumerable<User> ListAll()
        {
            return listUser;
        }

        public void Register(User user)
        {
            listUser.Add(user);
        }
    }
}
