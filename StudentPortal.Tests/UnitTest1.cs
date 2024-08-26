using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Controllers;

using StudentPortal.Web.Data;
using StudentPortal.Web.Models;



namespace StudentPortal.Tests;

public class ControllerTests
{

    
    [Fact]
    public async Task PingTests()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var Response1 = await client.GetAsync("/Students");
        var Response2 = await client.GetAsync("/Students/Add");
        var Response3 = await client.GetAsync("/Students/Edit/1");

        var Response4 = await client.GetAsync("/Home");
        var Response5 = await client.GetAsync("/Home/Privacy");

        Assert.True(Response1.IsSuccessStatusCode);
        Assert.True(Response2.IsSuccessStatusCode);
        Assert.True(Response3.IsSuccessStatusCode);

        Assert.True(Response4.IsSuccessStatusCode);
        Assert.True(Response5.IsSuccessStatusCode);
    }

    [Fact]
    public void ControllertoViewTest()
    {

        var context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "StudentDatabase").Options);
        
        Student ToEdit = new Student {Id = 3, Email = "Test3@Test", Name="Test3 test", Phone="06773", Subscribed=true};
        context.students.Add(new Student {Id = 1, Email = "Test@Test", Name="Test test", Phone="0677", Subscribed=false});
        context.students.Add(new Student {Id = 2, Email = "Test2@Test", Name="Test2 test", Phone="06772", Subscribed=false});
        context.students.Add(ToEdit);
        context.SaveChanges();
        

        StudentsController StudentC = new StudentsController(context);
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Form = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues> {{"name", ""},{"email", ""}, {"phone", ""}, {"subscribed", ""} });
        
        
        StudentC.ControllerContext = new ControllerContext() { HttpContext =  httpContext};
        
        
        
        
        
        Assert.Equal("Add", ((ViewResult) StudentC.AddStudent()).ViewName);
        Assert.Null(((ViewResult) StudentC.Add()).ViewName);
        Assert.Null(((ViewResult) StudentC.Index()).ViewName);
        Assert.Equal(typeof(Student), ((ViewResult) StudentC.Edit(1)).Model.GetType());
        Assert.Equal(typeof(Student), ((ViewResult) StudentC.Edit(1)).Model.GetType());

        ToEdit.Subscribed=true;
        Assert.Equal("Index", ((RedirectToActionResult) StudentC.Edit(ToEdit)).ActionName);

        Assert.Equal("Index", ((RedirectToActionResult) StudentC.Delete(1)).ActionName);
    }
}