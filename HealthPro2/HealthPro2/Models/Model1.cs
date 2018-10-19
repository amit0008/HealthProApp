namespace HealthPro2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public partial class Model1 : DbContext
    {
        public Food food { get; set; }
        public FoodPlan foodPlan { get; set; }
        public FoodPlanInfo foodPlanInfo { get; set; }
        [Required]
        public string PlanName { get; set; }
        public int PlanId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int[] FoodId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only Positive Whole Number allowed")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only Positive Whole Number allowed")]
        public int Quantity1 { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only Positive Whole Number allowed")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only Positive Whole Number allowed")]
        public int Quantity2 { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only Positive Whole Number allowed")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only Positive Whole Number allowed")]
        public int Quantity3 { get; set; }
        public string UserID { get; set; }
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<FoodPlan> FoodPlans { get; set; }
        public virtual DbSet<FoodPlanInfo> FoodPlanInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>()
                .HasMany(e => e.FoodPlanInfoes)
                .WithRequired(e => e.Food)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FoodPlan>()
                .HasMany(e => e.FoodPlanInfoes)
                .WithRequired(e => e.FoodPlan)
                .WillCascadeOnDelete(false);
        }
    }
}
