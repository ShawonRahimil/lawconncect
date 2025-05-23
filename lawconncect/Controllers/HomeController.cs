using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using lawconncect.Models;
using lawconncect.Data;

namespace lawconncect.Controllers;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context; 

    public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
    {
        _logger = logger;
        this. _context = context;
    }

    public IActionResult Index()
    {
       var Clienttable= ViewBag.Clients = _context.Clients.ToList();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
