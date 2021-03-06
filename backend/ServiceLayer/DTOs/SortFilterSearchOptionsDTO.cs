﻿using ServiceLayer.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTOs
{
    public class SortFilterSearchOptionsDTO
    {
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public string Search { get; set; }
        public SortOrderDTO? SortOrder { get; set; }
    }
}

