using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Storedprocedure.Models
{
    public interface IStudent
    {
        public void AddStudent(Student student);
        IEnumerable<Student> GetStudents();
        public void deletestudent(int id);
        public void updateStudent(Student updatestudent,int id);

    }
    public class StudentRepository : IStudent
    {
        private string connectionString { get; set; }

        private readonly IConfiguration _config;


        public StudentRepository(IConfiguration configuration)
        {
            _config = configuration;
            connectionString = _config["ConnectionStrings:DefaultConnection"];
        }
       public void AddStudent(Student student)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("InsertStudent", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@fullname", student.fullname);
                command.Parameters.AddWithValue("@age", student.age);
                command.Parameters.AddWithValue("@gender", student.gender);
                command.ExecuteNonQuery();
            }
        }
        public IEnumerable<Student> GetStudents()
        {
            List<Student> students= new List<Student>();
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("selectStudent", conn);
                    command.CommandType=CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Student student = new Student();
                    student.id = Convert.ToInt32(reader["id"]);
                    student.fullname = (reader["fullname"]).ToString();
                    student.age = Convert.ToInt32(reader["age"]);
                    student.gender =(reader["gender"]).ToString();
                     students.Add(student);
                    
                }
                return students;
            }
        }
        public void deletestudent(int id)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("deletestudent", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue ("@studentid", id); 
                command.ExecuteNonQuery();

            }

        }
        public void updateStudent(Student updatestudent,int id)
        {
            using(SqlConnection con = new SqlConnection(connectionString)) {
                con.Open();
                SqlCommand command = new SqlCommand("updateStudent", con);
                command.CommandType=CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@studentid", id);
                command.Parameters.AddWithValue("@fullname", updatestudent.fullname);
                command.Parameters.AddWithValue("@age", updatestudent.age);
                command.Parameters.AddWithValue("@gender", updatestudent.gender);
                command.ExecuteNonQuery();







            }
        }


    }

}

