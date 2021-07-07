using System;
using System.ComponentModel.DataAnnotations;

namespace BMS.Models
{
    public class Account
    {
        [Required]
        public string AccountNumber { get; set; }
        [Key]
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string GuardianType { get; set; }
        [Required]
        public string GuardianName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Citizenship { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string MaritalStatus { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public DateTime DateofBirth { get; set; }
        [Required]
        public DateTime RegisterationDate { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Required]
        public string BranchName { get; set; }
        [Required]
        public string CitizenStatus { get; set; }
        [Required]
        public long InitialDeposit { get; set; }
        [Required]
        public string IdentificationType { get; set; }
        [Required]
        public string IdCardNumber { get; set; }
        [Required]
        public string ReferenceAccountHolderName { get; set; }
        [Required]
        public string ReferenceAccountHolderAccountNumber { get; set; }
        [Required]
        public string ReferenceAccountHolderAddress { get; set; }
       
    }
}
