using DutchTreatHW.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace DutchTreatHW.ViewModels;

public class OrderItemViewModel
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    [Required]
    public int ProductId { get; set; }

    public string ProductCategory { get; set; }
    public string ProductSize { get; set; }
    public string ProductTitle { get; set; }
    public string ProductArtist { get; set; }
    public string ProductArtId { get; set; }
}