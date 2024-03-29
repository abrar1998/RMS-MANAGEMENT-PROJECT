﻿using System.ComponentModel.DataAnnotations;

namespace RMS_Management_System.Models
{
    public class CourseViewModel
    {
        public string CourseName { get; set; }

        [Required]
        public string CourseFee { get; set; }

        [Display(Name ="Upload Only A Pdf")]
        [Required]
        public IFormFile UploadPdf { get; set; }
    }
}
