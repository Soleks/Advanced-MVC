using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.Models;
using WebStore.Interfaces.Services;

namespace WebStore.ServicesHosting.Controllers
{

    [Route("api/employees"), Produces("application/json"), ApiController]
    public class EmloyeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;

        public EmloyeesApiController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }

        [HttpGet, ActionName("Get")]
        public IEnumerable<EmployeeView> GetAll()
        {
            return _employeesData.GetAll();
        }

        [HttpGet("{id}"), ActionName("Get")]
        public EmployeeView GetById(int id)
        {
            return _employeesData.GetById(id);
        }


        [HttpPut("{id}"), ActionName("Put")]
        public EmployeeView UpdateEmployee(int id, [FromBody]EmployeeView entity)
        {
            return _employeesData.UpdateEmployee(id, entity);
        }

        [HttpPost, ActionName("Post")]
        public void AddNew([FromBody]EmployeeView model)
        {
            _employeesData.AddNew(model);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeesData.Delete(id);
        }
    }
}