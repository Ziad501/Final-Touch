﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTouch.Core.Specifications
{
    public class PagingParams
    {
		private const int MaxPageSize = 100;
		public int PageIndex { get; set; } = 1;


		private int _pageSize = 6;

		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
		}
	}
}
