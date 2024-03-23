using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerDemoOne.Models;

public partial class CustomerMaster
{
    [Key]
    [Display(Name ="Sr.No.")]
    public int CustomerId { get; set; }

    [Required(ErrorMessage ="Please Enter Name.")]
    [StringLength(200)]
    [Display(Name ="Name")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage ="Please Enter Email ID.")]
    [StringLength(200)]
    [EmailAddress]
    [Display(Name ="Email ID")]
    public string EmailId { get; set; } = null!;

    [Required(ErrorMessage ="Please Enter Mobile No.")]
    [StringLength(10)]
    [Phone]
    [Display(Name ="Mobile No")]
    public string MobileNumber { get; set; } = null!;

    [StringLength(500)]
    [Display(Name ="Address")]
    public string? Address { get; set; }

    [Display(Name ="Status")]
    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; } = false;

    [Display(Name = "When Entered")]
    public DateTime WhenEntered { get; set; } = DateTime.UtcNow;

    [Display(Name = "When Modified")]
    public DateTime WhenModified { get; set; } = DateTime.UtcNow;
}
