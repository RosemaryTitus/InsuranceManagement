using System;
using System.Collections.Generic;

namespace InsuranceManagement.Models;

public partial class Rule
{
    public int Id { get; set; }

    public Guid? Rgid { get; set; }

    public string PolicyType { get; set; } = null!;

    public string ConditionType { get; set; } = null!;

    public string? ConditionOperator { get; set; }

    public string? ConditionValue { get; set; }

    public string? ActionType { get; set; }

    public decimal? ActionValue { get; set; }

    public string? Description { get; set; }
}
