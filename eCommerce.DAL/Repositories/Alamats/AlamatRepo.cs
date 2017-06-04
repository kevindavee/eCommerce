﻿using eCommerce.Core.CommerceClasses.Alamats;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.Alamats
{
    public class AlamatRepo : RepoBase<Alamat>
    {
        private CommerceContext context;

        public AlamatRepo(CommerceContext _context): base(_context)
        {
            this.context = _context;
        }
    }
}