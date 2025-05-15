using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class employeeDashboardModel
    {
        [Display(Name = "Select Farmer")]
        public int? SelectedFarmerId { get; set; }
        public IEnumerable<SelectListItem> AvailableFarmers { get; set; } = new List<SelectListItem>();

        [Display(Name = "Product Type/Category")]
        public string? FilterProductType { get; set; } // Product type filter

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? FilterStartDate { get; set; } // Date range start

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? FilterEndDate { get; set; } // Date range end

        public IEnumerable<productModel> Products { get; set; } = new List<productModel>();
    }
}
