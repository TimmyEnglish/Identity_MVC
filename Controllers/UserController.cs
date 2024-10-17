using Microsoft.AspNetCore.Mvc;
using ASP_NET_MVC.Models;
using ASP_NET_MVC.Data;
using System.Collections.Generic;
using System.Linq; 

public class UserController : Controller
{
    private static List<User> users = new List<User>
    {
        new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", RegistrationDate = DateTime.Now.AddYears(-1) },
        new User { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", RegistrationDate = DateTime.Now.AddYears(-2) }
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
    public IActionResult Login()
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
            user.Role = "User";
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
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Check if user exists
            var user = _context.Users
                .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user != null)
            {
                // Set session or cookie, depending on how you manage login
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetString("Role", user.Role);


                return RedirectToAction("Profile", new { id = user.Id });
            }

            ModelState.AddModelError("", "Invalid email or password");
        }
        return View(model);
    }

    // GET: Profile page (after login)
    public IActionResult Profile(int id)
    {
        // Get the logged-in user's ID from the session
        var loggedInUserId = HttpContext.Session.GetString("UserId");

        if (loggedInUserId == null)
        {
            // If no user is logged in, redirect to the login page
            return RedirectToAction("Login", "User");
        }

        // If the logged-in user is trying to access a profile that isn't theirs
        if (int.Parse(loggedInUserId) != id)
        {
            // Redirect to their own profile
            return RedirectToAction("Profile", new { id = loggedInUserId });
        }

        // Fetch the user's details from the database
        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            // If the user is not found, return a Not Found page
            return NotFound();
        }

        return View(user); // Pass the user details to the profile view
    }

}
