using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;
using MySql.Data.MySqlClient;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly IConfiguration _configuration; 
        public TaskController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        MySqlConnection connection;
        string server = "10.0.5.83";
        string database = "athirah_db_todos";
        string uid = "docka";
        string password = "123456";
        string connectionString;

        [HttpPost]
        public JsonResult Post(Tasks todos)
        {
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = @"insert into todos
                             (tasks, completion)
                            values (@tasks, @completion)";
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@tasks", todos.tasks);
                        cmd.Parameters.AddWithValue("@completion", todos.completion);
                        myReader = cmd.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new JsonResult("Added Successfully");
        }

        [HttpGet]
        public JsonResult Get()
        {
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = @"select id, tasks, completion
                            from todos";
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        myReader = cmd.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new JsonResult(table);
        }

        [HttpPut]
        public JsonResult Put(Tasks todos)
        {
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = @"update todos
                            set tasks = @tasks,
                            completion = @completion
                            where id = @id";

            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", todos.id);
                        cmd.Parameters.AddWithValue("@tasks", todos.tasks);
                        cmd.Parameters.AddWithValue("@completion", todos.completion);
                        myReader = cmd.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            DataTable table = new DataTable();
            MySqlDataReader myReader;
            string query = @"delete from todos where id = @id";

            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        myReader = cmd.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new JsonResult("Deleted Successfully");
        }


    }
}
