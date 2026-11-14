using System.Numerics;
using System.Security.Permissions;
using Microsoft.AspNetCore.Components.Web;

public class Course
{
   public int Id { get; set; }
   public string Code { get; set; }
   public string Title { get; set; }
   public int Credits { get; set; }
   public string Instructor { get; set; }

   public Course(int id, string code, string title, int credits, string instructor)
    {
        Id = id;
        Code = code;
        Title = title;
        Credits = credits;
        Instructor = instructor;
    }
}