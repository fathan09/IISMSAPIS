using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IISMSBackend.Entities;
public class User {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int userId {get; set;}
    public required string fullName {get; set;}
    public required string email {get; set;}
    public required string password{get; set;} 
    public required string phoneNumber{get; set;} 
    public required string role{get; set;}

}