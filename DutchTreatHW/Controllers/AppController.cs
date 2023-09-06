using DutchTreatHW.Data;
using DutchTreatHW.Services;
using DutchTreatHW.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DutchTreatHW.Controllers;

public class AppController : Controller
{
    private readonly INullMailService _mailService;
		private DutchContext _context;

		public DutchContext Context { get; }

		public AppController(INullMailService mailService, DutchContext context) 
    {
       _mailService = mailService;
			_context = context;
		}

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("contact")]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost("contact")]
    public IActionResult Contact(ContactViewModel model)
    {
        if(ModelState.IsValid)
        {
            // Send the email
            _mailService.SendMessage("shawn@wildermich.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
            ViewBag.UserMessage = "Mail Sent";
            ModelState.Clear();
			}

			return View();
		}

    public IActionResult About() 
    {
        return View();
    }

    public IActionResult Shop()
    {
        var results = _context.Products
            .OrderBy(p => p.Category)
            .ToList();
        return View(results);
    }
}
