﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class DayOfWeek
    {
        [Key]
        public int Id { get; set; }
        public string Day { get; set; }
    }
}