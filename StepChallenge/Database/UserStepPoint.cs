using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepChallenge.Database
{
    public class UserStepPoint
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; }
        public DateTime StepDate { get; set; }
        public int Steps { get; set; }
        public int Points { get; set; }
    }
}
