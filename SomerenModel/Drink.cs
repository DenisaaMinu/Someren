﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Drink
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal VATRate { get; set; }
        public bool IsAlcoholic { get; set; }
        public int Stock {  get; set; }
        public string StockStatus {  get; set; }
    }
}