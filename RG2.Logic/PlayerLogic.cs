using Signum.Engine.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RG2.Entities;
using Signum.Engine.Operations;
using Signum.Engine.DynamicQuery;

namespace RG2.Logic
{
    public static class PlayerLogic
    {
        public static void Start(SchemaBuilder sb)
        {
            if (sb.NotDefined(MethodInfo.GetCurrentMethod()))
            {
                sb.Include<Player>()
                    .WithSave(PlayerOperation.Save)
                    .WithQuery(() => e => new
                    {
                        Entity = e,
                        e.Id,
                        e.Name,
                        e.Characters,
                        e.JoinedOn,
                        e.Attendance
                    });
            }  
        }
    }
}
