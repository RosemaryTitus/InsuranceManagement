using System;
using System.Collections.Generic;

namespace InsuranceManagement.Models;

public partial class Purchase
{
    public int Id { get; set; }

    public Guid? Rgid { get; set; }

    public int CustomerId { get; set; }

    public int PolicyId { get; set; }

    public string PolicyType { get; set; } = null!;

    public DateTime PurchaseDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal TotalPremiumAmount { get; set; }

    public bool Status { get; set; }

    public int PaymentId { get; set; }

    public int? RuleId { get; set; }
}
