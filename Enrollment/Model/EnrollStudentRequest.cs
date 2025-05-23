﻿using System.ComponentModel.DataAnnotations;

namespace Enrollment.Model
{
    public class EnrollStudentRequest
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public string CourseCode { get; set; } = string.Empty;
    }
}
