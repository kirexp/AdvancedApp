using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace DAL.Override
{
    public class RentOverride:IAutoMappingOverride<Rent>
    {
        public void Override(AutoMapping<Rent> mapping) {
            mapping.Map(x => x.RentEndTime).Nullable();
        }
    }
}
