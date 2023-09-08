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
    private readonly IDutchRepository _repository;

		public AppController(INullMailService mailService, IDutchRepository repository) 
        {
            _mailService = mailService;
            _repository = repository;
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
        var results = _repository.GetAllProducts();
        return View(results);
    }
}
