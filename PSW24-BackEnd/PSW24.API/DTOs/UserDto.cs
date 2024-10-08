﻿namespace PSW24.API.DTOs;

public class UserDto
{
    public long Id { get; set; }
    public string Username { get; set; }
    public UserRoleDto Role { get; set; }
    public bool IsActive { get; set; }
    public long? Points { get; set; }
}

public enum UserRoleDto
{
    Tourist,
    Author,
    Admin
}