var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

var courses = new List<Course>
{
    new Course(1, "CMPS 101 OAON", "Introduction to Applied Computer Science", 3, "Dr. Voortman"),
    new Course(2, "CMPS 160 OAON", "Databases", 3, "Professor Seaman"),
    new Course(3, "CMPS 162 OAON", "Introduction to Programming", 3, "Dr. Voortman"),
    new Course(4, "CMPS 163 OAON", "Business Analytics", 3, "Dr. Oliveira"),
    new Course(5, "CMPS 260 OAON", "Data Structures", 3, "Dr. Oliveira"),
    new Course(6, "CMPS 262 OAON", "Advanced Programming", 3, "Professor Seaman"),
    new Course(7, "CMPS 161 OAON", "Networking and Security", 3, "Dr. Oliveira"),
    new Course(8, "CMPS 261 OAON", "Server Management", 3, "Dr. Oliveira"),
    new Course(9, "CMPS 360 OAON", "Survey of Programming Languages", 3, "Professor Seaman")
};

app.MapGet("/api/courses", () => Results.Ok(courses));

app.MapGet("/api/courses/{id:int}", (int id) =>
{
    var course = courses.FirstOrDefault(c => c.Id == id);
    return course is null ? Results.NotFound() : Results.Ok(course);
});

app.MapPost("/api/courses", (Course request) =>
{
    if (string.IsNullOrWhiteSpace(request.Code) || string.IsNullOrWhiteSpace(request.Title) || request.Credits < 1 || request.Credits > 6 || string.IsNullOrWhiteSpace(request.Instructor))
        return Results.BadRequest("Course code, title, and instructor are required and credits value must be 1-6.");

    int id = courses.Count == 0 ? 1 : courses.Max(c => c.Id) + 1;
    var course = new Course(id, request.Code, request.Title, request.Credits, request.Instructor);
    courses.Add(course);
    return Results.Created($"/api/courses/{id}", course);
});

app.MapPut("/api/courses/{id:int}", (int id, Course request) =>
{
    var course = courses.FirstOrDefault(c => c.Id == id);

    if (course == null)
        return Results.NotFound();

    else if (string.IsNullOrWhiteSpace(request.Code) || string.IsNullOrWhiteSpace(request.Title) || request.Credits < 1 || request.Credits > 6 || string.IsNullOrWhiteSpace(request.Instructor))
        return Results.BadRequest("Course code, title, and instructor are required and credits value must be 1-6.");

    course.Code = request.Code;
    course.Title = request.Title;
    course.Credits = request.Credits;
    course.Instructor = request.Instructor;

    return Results.Ok(course);
});

app.MapDelete("/api/courses/{id:int}", (int id) =>
{
    var course = courses.FirstOrDefault(c => c.Id == id);

    if (course is null)
        return Results.NotFound();
    
    courses.Remove(course);
    return Results.NoContent();
});

app.Run();
