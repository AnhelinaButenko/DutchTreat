using AutoMapper;
using DutchTreatHW.Data;
using DutchTreatHW.Data.Entities;
using DutchTreatHW.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DutchTreatHW.Controllers;

[Route("api/[Controller]")]
[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
public class OrdersController : Controller
{
    private readonly IDutchRepository _repository;
    private readonly ILogger<OrdersController> _logger;
    private readonly IMapper _mapper;
	private readonly UserManager<StoreUser> _userManager;

	public OrdersController(IDutchRepository repository, 
        ILogger<OrdersController> logger,
        IMapper mapper,
        UserManager<StoreUser> userManager)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
		_userManager = userManager;
	}

    [HttpGet]
    public IActionResult Get(bool includeItems = true)
    {
        try
        {
            var username = User.Identity.Name;

            var result = _repository.GetAllOrdersByUser(username, includeItems);
            return Ok(_mapper.Map<IEnumerable<OrderViewModel>>(result));
        }
        catch (System.Exception ex)
        {
            _logger.LogError($"Failed to get orders: {ex}");
            return BadRequest("Failed to get orders");
        }
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        try
        {
            var order = _repository.GetOrderById(User.Identity.Name, id);

            if (order != null)
            {
                return Ok(_mapper.Map<Order, OrderViewModel>(order));
            }
            else
            {
                return NotFound();
            }
        }
        catch (System.Exception ex)
        {
            _logger.LogError($"Failed to get orders: {ex}");
            return BadRequest("Failed to get orders");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]OrderViewModel model)
    {
        // add it to the db
        try
        {
            if (ModelState.IsValid)
            {
                var newOrder = _mapper.Map<OrderViewModel, Order>(model);

                if (newOrder.OrderDate == DateTime.MinValue)
                {
                    newOrder.OrderDate = DateTime.Now;
                }

                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);


				newOrder.User = currentUser;

                _repository.AddEntity(newOrder);

                if (_repository.SaveAll())
                {
                       
                    return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        catch (System.Exception ex)
        {

            _logger.LogError($"Failed to save a new order: {ex}");
        }

        return BadRequest("$\"Failed to save a new order");
    }
}