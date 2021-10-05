using System;
using System.Collections.Generic;
using Moghimi.Core.Json;

namespace Moghimi.Core
{

    class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<Course> Courses { get; set; }
    }

    class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public bool Passed  { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Student std = new Student()
            {
                StudentId = 33120,
                FirstName = "Mahmood",
                LastName = "Moghimi",
                Phone = "123456789",
                Address = "Github: https://github.com/moghimi",
            };

            std.Courses = new List<Course>();
            std.Courses.Add(new Course { CourseId = 100, Name = "Mathematics" });
            std.Courses.Add(new Course { CourseId = 200, Name = "Physics" });


            Console.WriteLine(std.ToJson());
            Console.ReadLine();

            Console.WriteLine(std.ToJson(indented: true, ignoreNulls: false, resolver: ContractResolver.SnakeCase));
            Console.ReadLine();

            Console.WriteLine(std.ToJson(indented: true, ignoreNulls: true, resolver: ContractResolver.Default));
            Console.ReadLine();
        }
    }
}
