using FreeAD.Infrastructure.Mapping;
using FreeADPortable.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FreeAD.Models.ViewModels
{
    public class AdvertisementViewModel: IMapFrom<Advertisement>
    {
        public long Id { get; set; }
        [Required, MaxLength(80)]
        public string KeyWord { get; set; }
        [Display(Name = "StartDate")]
        public DateTime ValidStartDate { get; set; }
        [Display(Name = "EndDate")]
        public DateTime ValidEndDate { get; set; }
        public string Area { get; set; }
        [Required, MaxLength(600)]
        public string ShortDescription { get; set; }
        public string Detail { get; set; }
        [MaxLength(80)]
        public string Company { get; set; }
        [Required, MaxLength(50)]
        public string Contact { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Website { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        public bool Deleted { get; set; }
        [DataType("CategoryId"), Display(Name = "Category")]
        public long CategoryId { get; set; }
        public long DefaultOrder { get; set; }
        public long Popular { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public string Operator { get; set; }
        public DateTime OperationDate { get; set; }
    }
}