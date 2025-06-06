﻿using Microsoft.AspNetCore.Identity;
namespace BookLendingSystem.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
    }
}
