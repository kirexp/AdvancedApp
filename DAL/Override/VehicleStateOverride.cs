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
    public class VehicleStateOverride:IAutoMappingOverride<VehicleState>
    {
        public void Override(AutoMapping<VehicleState> mapping) {
            mapping.HasOne(x => x.Car).Cascade.All();
            mapping.HasOne(x => x.CurrentPosition).Cascade.All();
        }
    }
}
