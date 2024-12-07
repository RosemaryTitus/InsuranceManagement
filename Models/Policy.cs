using System;
using System.Collections.Generic;

namespace InsuranceManagement.Models;

public partial class Policy
{
    public int Id { get; set; }

    public Guid? Rgid { get; set; }

    public string PolicyNumber { get; set; } = null!;

    public int PolicyTypeId { get; set; }

    public DateTime EffectiveFrom { get; set; }

    public DateTime? EffectiveTo { get; set; }

    public bool Status { get; set; }

    public decimal PremiumAmount { get; set; }

    public int PaymentFrequency { get; set; }

    public string? PolicyType { get; set; }

    public string PolicyTerm { get; set; } = null!;

    public string LastUpdatedBy { get; set; } = null!;

    public DateTime LastUpdatedOn { get; set; }
}
