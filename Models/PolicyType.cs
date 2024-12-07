using System;
using System.Collections.Generic;

namespace InsuranceManagement.Models;

public partial class PolicyType
{
    public int Id { get; set; }

    public Guid Rgid { get; set; } = Guid.NewGuid();

    public string TypeName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime EffectiveFrom { get; set; }

    public DateTime? EffectiveTo { get; set; }

    public bool Status { get; set; }

    public string LastUpdatedBy { get; set; } = null!;

    public DateTime LastUpdatedOn { get; set; }
}
