namespace HealthPro2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FoodPlanInfo")]
    public partial class FoodPlanInfo
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FoodId { get; set; }

        [Required]
        public string UserId { get; set; }

        public int Quantity { get; set; }

        public virtual Food Food { get; set; }

        public virtual FoodPlan FoodPlan { get; set; }
    }
}
