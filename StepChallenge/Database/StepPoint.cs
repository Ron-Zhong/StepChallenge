using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepChallenge.Database
{
    public class StepPoint
    {
        [Key]
        public Guid Id { get; set; }
        public int MileStone { get; set; }
        public int TargetSteps { get; set; }
        public int Points { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
