﻿using System;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
}
public enum UserRole
{
    Admin,
    Librarian,
    Member
}