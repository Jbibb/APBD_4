using System;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{

    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Is_Missing()
    {
        // Arrange
        var userService = new UserService();
        // Act
        var addResult = userService.AddUser(null, "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 4);
        // Assert
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_LastName_Is_Missing()
    {
        // Arrange
        var userService = new UserService();
        // Act
        var addResult = userService.AddUser("John", null, "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 4);
        // Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Email_Is_Incorrect()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("John", "Doe", "johndoegmailcom", DateTime.Parse("1982-03-21"), 4);
        //Assert
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Age_From_Birthdate_Is_Less_Than_21()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Today, 1);
        //Assert
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Calculated_User_Credit_Limit_Is_Less_Than_500()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("Maciej", "Kowalski", "kowalski@wp.pl", DateTime.Parse("1982-03-21"), 1);
        //Assert
        Assert.False(addResult);
        
    }
    
    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_ClientID_Does_Not_Exist()
    {
        //Arrange
        var userService = new UserService();
        //Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 99);
        });
    }
    
    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_Client_LastName_Does_Not_Exist()
    {
        //Arrange
        var userService = new UserService();
        //Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            userService.AddUser("John", "Woe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        });
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Very_Important_Client_Is_Added()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("John", "Malewski", "malewski@gmail.com", DateTime.Parse("1982-03-21"), 2);
        //Assert
        Assert.True(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Important_Client_Is_Added()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("John", "Smith", "smith@gmail.com", DateTime.Parse("1982-03-21"), 3);
        //Assert
        Assert.True(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Normal_Client_Is_Added()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("John", "Kwiatkowski", "kwiatkowski@wp.pl", DateTime.Parse("1982-03-21"), 5);
        //Assert
        Assert.True(addResult);
    }
   
    /*
    [Fact]
    public void AddUser_Should_Return_False_When_()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        //Assert
        
    }
    */
}