using InsuranceManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;

namespace InsuranceManagement.Services
{
    public interface IaddPolicy
    {
        public Task<string> addPolicyType(PolicyType policytype);
        public Task<string> addPolicy(Policy policy);

        public Task<string> addRule(Rule rule);

        public Task<string> purchasePolicy(Purchase purchase);
    }
    public class PolicyService : IaddPolicy
    {
        public readonly InsuranceManagementDatabaseContext DatabaseContext;
        public PolicyService(InsuranceManagementDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }


        public async Task<string> addPolicyType(PolicyType policytype)
        {
            string result = "";
            var policyCheck = await DatabaseContext.PolicyTypes.Where(x => x.Id == policytype.Id).FirstOrDefaultAsync();
            if (policyCheck == null)
            {
                policytype.EffectiveTo = policytype.EffectiveFrom.AddYears(1);
                policytype.LastUpdatedOn = DateTime.Now;
                await DatabaseContext.PolicyTypes.AddAsync(policytype);
                //await DatabaseContext.SaveChangesAsync();
                result = "Policy Added Successfully";
            }
            else
            {

                if (policyCheck != null && policyCheck.Status == true)
                {
                    if (policytype.Status == false)
                    {
                        policytype.EffectiveTo = DateTime.Now;
                    }
                    policytype.LastUpdatedOn = DateTime.Now;

                    DatabaseContext.PolicyTypes.Update(policytype);
                    //await DatabaseContext.SaveChangesAsync();
                    result = "Changes are added Successfully";
                }
                else
                {
                    result = "Policy Expired";
                }


                



            }
            await DatabaseContext.SaveChangesAsync();
            return result;

        }

        public async Task<string> addPolicy(Policy policy)
        {
            string result = "";
            var addPolicyCheck = DatabaseContext.Policies.Where(x => x.Id == policy.Id).FirstOrDefault();
            if (addPolicyCheck == null)
            {
                
                var policytype = (from i in DatabaseContext.PolicyTypes
                                 where i.Id == policy.PolicyTypeId
                                 select new
                                 {
                                     i.EffectiveFrom,
                                     i.EffectiveTo,
                                     i.TypeName,
                                     i.Status,
                                     i.Id
                                 }).FirstOrDefault();   


                if (policytype != null)
                {
                    if (policytype.Status == true)
                    {
                        policy.EffectiveFrom = policytype.EffectiveFrom;
                        policy.EffectiveTo = policytype.EffectiveTo;
                        policy.PolicyType = policytype.TypeName;
                        policy.LastUpdatedOn = DateTime.Now;
                        await DatabaseContext.Policies.AddAsync(policy);
                        result = $"Poicy is added successfully for policy type {policy.PolicyTypeId}";
                    }
                    else
                    {
                        result = $"Policy with id {policytype.Id} is expired";
                    }
                    
                }
                else
                {
                    result = $"There is no policy type with id {policy.PolicyTypeId}";
                }

               
            }
            else
            {
                result =  $"Policy with {policy.Id} already exist";
            }
            await DatabaseContext.SaveChangesAsync();
            return result;
        }

        public async Task<string> addRule(Rule rule)
        {
            string result = "";
            var findRule =   DatabaseContext.Rules.Where(x=>x.Id == rule.Id).FirstOrDefault();
            if(findRule == null)
            {
                if (rule.PolicyType == "Car")
                {
                    var carPolicy = from i in DatabaseContext.Policies
                                    where (i.PolicyTerm == "Car" && i.Status == true)
                                    select new
                                    {
                                        i.PaymentFrequency,
                                        i.PremiumAmount
                                    };

                    if(carPolicy == null)
                    {
                        result = "There is no active policy created for Car";
                    }
                    else
                    {
                        await DatabaseContext.Rules.AddAsync(rule);
                        result = "Rule is added successfully";
                    }
                }
                else if(rule.PolicyType =="Bike")
                {
                    var bikePolicy = (from i in DatabaseContext.Policies
                                    where (i.PolicyTerm == "Bike" && i.Status == true)
                                    select new
                                    {
                                        i.PaymentFrequency,
                                        i.PremiumAmount
                                    }).FirstOrDefault();

                    if (bikePolicy == null)
                    {
                        result = "There is no active policy created for Bike";
                    }
                    else
                    {
                        await DatabaseContext.Rules.AddAsync(rule);
                        result = "Rule is added successfully";
                    }
                }
            }
            else
            {
                result = $"Rule with id {rule.Id} already exist";
            }

            await DatabaseContext.SaveChangesAsync();
            return result;
        }

        public async Task<string> purchasePolicy(Purchase purchase)
        {
            string result = "";
            var purchasepolicy = DatabaseContext.Purchases.Where(x=>x.Id == purchase.Id).FirstOrDefault();
            if(purchasepolicy == null)
            {
                var ruleinfo = (from i in DatabaseContext.Rules
                               where i.Id == purchase.RuleId
                               select new
                               {
                                   i.ConditionValue,
                                   i.ConditionOperator,
                                   i.ActionType,
                                   i.ActionValue
                               }).FirstOrDefault();

                var policyinfo = (from i in DatabaseContext.Policies
                                 where i.Id == purchase.PolicyId
                                 select new
                                 {
                                     i.PolicyType,
                                     i.PaymentFrequency,
                                     i.PremiumAmount,
                                     i.EffectiveFrom,
                                     i.EffectiveTo
                                 }).FirstOrDefault();

                if(ruleinfo == null)
                {
                    result = "There is no rule for this rule id";
                }
                else
                {
                    if(policyinfo == null)
                    {
                        result = "Please check policy information";
                    }
                    else
                    {
                        purchase.PolicyType = policyinfo.PolicyType;
                        if(purchase.StartDate >= policyinfo.EffectiveFrom && purchase.EndDate <= policyinfo.EffectiveTo && ruleinfo.ActionType == "Add")
                        {
                            purchase.TotalPremiumAmount = Convert.ToDecimal( policyinfo.PremiumAmount + ((ruleinfo.ActionValue / 100)*policyinfo.PremiumAmount));
                            result = "Policy purchased successfully";
                            await DatabaseContext.Purchases.AddAsync(purchase);
                        }
                        else
                        {
                            result = "Please check the start and end date";

                        }
                    }
                }
            }
            else
            {
                result = $"Entry with Id {purchase.Id} is already created.";
            }
            
            DatabaseContext.SaveChanges();
            return result;
        }
    }
}
