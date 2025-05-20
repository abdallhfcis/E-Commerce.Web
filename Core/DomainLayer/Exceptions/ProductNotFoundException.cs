﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class ProductNotFoundException(int id ):NotFoundException($"No Product Fount with Id = {id}")
    {
    }
}
