using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;


namespace ReactWithDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeaprtmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        
        public DeaprtmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("/GetEmployee")]
        public JsonResult Get()
        {
            string query = @"Select DepartmentId,DepartmentName from dbo.Department ";
            DataTable dt;
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EmployeeCon"));            
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(command);
            dt = new DataTable();
            da.Fill(dt);            

            return new JsonResult(dt);
        }

    }
}
