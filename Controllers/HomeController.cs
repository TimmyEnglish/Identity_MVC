using Microsoft.AspNetCore.Mvc;
using ASP_NET_MVC.Models; 
using System.Collections.Generic;

public class HomeController : Controller
{
    public IActionResult Users()
    {
        var users = new List<User>
        {
            new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", RegistrationDate = DateTime.Now.AddDays(-10) },
            new User { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", RegistrationDate = DateTime.Now.AddDays(-5) }
        };

        return View(users);

    }
    public IActionResult Index()
    {
        return View();
    }
}
