using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LightfeatherBackendChallenge.Models
{
    public class SupervisorSubmission
    {
#nullable enable
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
#nullable disable
        [Required(ErrorMessage = "First name is a required field")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is a required field")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Supervisor is a required field")]
        public string Supervisor { get; set; }
    }
}
