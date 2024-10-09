using Microsoft.AspNetCore.Mvc;
using ASP_NET_MVC.Models;
using ASP_NET_MVC.Data;
using System.Collections.Generic;
using System.Linq; 

public class UserController : Controller
{
    private static List<User> users = new List<User>
    {
        new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "3801233422", RegistrationDate = DateTime.Now.AddYears(-1) },
        new User { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "3802345422", RegistrationDate = DateTime.Now.AddYears(-2) }
    };

    private readonly ApplicationDbContext _context;
    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Users()
    {
        var users = _context.Users.ToList();
        return View(users);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            user.RegistrationDate = DateTime.Now; 
            _context.Users.Add(user);  
            _context.SaveChanges();    
            return RedirectToAction("Users");  
        }

        return View(user);  
    }
    public IActionResult Delete(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Users)); 
    }
    public IActionResult Details(int id)
    {
        var user = _context.Users.Find(id); 
        if (user == null)
        {
            return NotFound(); 
        }
        return View(user);
    }
}
