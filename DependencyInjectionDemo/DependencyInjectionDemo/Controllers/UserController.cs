using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjectionDemo.Interface;
using DependencyInjectionDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionDemo.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;

        /// <summary>
        /// 通过构造函数进行注入
        /// </summary>
        /// <param name="userRepository"></param>
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            ViewBag.Title = _userRepository.GetUserTitle();
            return View(_userRepository.ListAll());
        }

        public IActionResult Register()
        {
            ViewBag.Title = _userRepository.GetRegisterTitle();
            return View();
        }

        [HttpPost]
        public IActionResult Register(User registerUser)
        {
            if (ModelState.IsValid)
            {
                _userRepository.Register(registerUser);
                // 注册成功，跳转到Index方法
                return RedirectToAction("Index");
            }
            return View(registerUser);
        }
    }
}