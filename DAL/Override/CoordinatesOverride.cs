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
   public  class CoordinatesOverride:IAutoMappingOverride<Coordinates>
    {
        public void Override(AutoMapping<Coordinates> mapping) {
            mapping.HasOne(x => x.State).Cascade.All();
        }
    }
}
