using DutchTreatHW.Data;
using DutchTreatHW.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;

namespace DutchTreatHW.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]
public class ProductsController : Controller
{
    private readonly IDutchRepository _repository;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IDutchRepository repository, ILogger<ProductsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult Get()
    {
        try
        {
            return Ok(_repository.GetAllProducts());
        }
        catch (System.Exception ex)
        {
            _logger.LogError($"Failed to get products: {ex}");
            return BadRequest("Failed to get products");
        }
        
    }
}
