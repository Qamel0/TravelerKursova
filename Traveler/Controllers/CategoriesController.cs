using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Traveler.Models;

namespace Traveler.Controllers;

public class CategoriesController : Controller
{
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ILogger<CategoriesController> logger)
    {
        _logger = logger;
    }

    public IActionResult Stays()
    {
        return View("Stays");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
