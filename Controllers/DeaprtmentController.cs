using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ReactWithDotnet.Model;
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
        [HttpPost]
        [Route("/AddRecord")]
        public JsonResult AddNewRecord(Department Depart)
        {
            string query = @" insert into dbo.Department values(@DepartmentName) ";
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EmployeeCon"));
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@DepartmentName",Depart.DepartmentName);
            command.ExecuteNonQuery();
            conn.Close();

            return new JsonResult(dt);
        }
        [HttpPut]
        [Route("/UpdateRecord")]
        public JsonResult UpdateExitingRecord(Department Depart,int id)
        {
            string query = @"Update dbo.Department set DepartmentName =@DepartmentName Where DepartmentId =@DepartmentId";
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EmployeeCon"));
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@DepartmentId", Depart.DepartmentId);
            command.Parameters.AddWithValue("@DepartmentName", Depart.DepartmentName);
            command.ExecuteNonQuery();
            conn.Close();

            return new JsonResult(dt);
        }

        [HttpDelete]
        [Route("/DeleteRecord")]
        public JsonResult DeleteExitingRecord(int id)
        {
            string query = @"delete from dbo.Department  Where DepartmentId =@DepartmentId";
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("EmployeeCon"));
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@DepartmentId",id);
            command.ExecuteNonQuery();
            conn.Close();

            return new JsonResult(dt);
        }



    }
}
