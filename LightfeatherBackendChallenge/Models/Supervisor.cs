using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LightfeatherBackendChallenge.Models
{
    public class Supervisor
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Jurisdiction { get; set; }
        public string IdentificationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
