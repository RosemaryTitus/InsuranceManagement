using InsuranceManagement.Models;
using InsuranceManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyServiceController : ControllerBase
    {
        public readonly InsuranceManagementDatabaseContext DatabaseContext;
        public readonly IaddPolicy iaddPolicy;

        public PolicyServiceController(InsuranceManagementDatabaseContext databaseContext, IaddPolicy iaddPolicy)
        {
            DatabaseContext = databaseContext;
            this.iaddPolicy = iaddPolicy;
        }

        //Add Policy Type

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("addPolicyType")]
        public async Task<IActionResult> AddPolicyType(PolicyType policyType)
        {
            try
            {
                var result = await iaddPolicy.addPolicyType(policyType);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }


        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("addPolicy")]
        public async Task<IActionResult> AddPolicy(Policy policy)
        {
            try
            {
                var result = await iaddPolicy.addPolicy(policy);
                if(result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("AddPolicyRule")]
        public async Task<IActionResult> AddPolicyRule(Rule rule)
        {

            var result = await iaddPolicy.addRule(rule);
            try
            {
                if(result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("addPolicyRules")]
        public async Task<IActionResult> addRule(Rule policyrule)
        {
            try
            {
                var result = await iaddPolicy.addRule(policyrule);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }


        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("PurchasePolicy")]
        public async Task< IActionResult >PurchasePolicy(Purchase purchase)
        {
            try
            {
                var result =  await iaddPolicy.purchasePolicy(purchase);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }


        }


    }
}
