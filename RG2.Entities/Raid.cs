using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Signum.Entities;
using System.Reflection;
using System.Linq.Expressions;
using Signum.Utilities;
using System.ComponentModel;
using System.Collections.Specialized;
using Signum.Entities.Disconnected;
using Signum.Entities.Scheduler;
using Signum.Entities.Processes;
using Signum.Utilities.ExpressionTrees;

namespace RG2.Entities
{
    [Serializable, EntityKind(EntityKind.Main, EntityData.Transactional)]
    public class Raid : Entity
    {
        [StringLengthValidator(Min = 2, Max = 50)]
        public string Name { get; set; }
    }

    [AutoInit]
    public static class RaidOperation
    {
        public static ExecuteSymbol<Raid> Save;
    }
}
