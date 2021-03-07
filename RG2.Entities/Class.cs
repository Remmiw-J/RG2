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
    [Serializable, EntityKind(EntityKind.Main, EntityData.Master)]
    public class Class : Entity
    {
        [StringLengthValidator(Min = 2, Max = 25)]
        public string Name { get; set; }

        [AutoExpressionField]
        public override string ToString() => As.Expression(() => Name);
    }

    [AutoInit]
    public static class ClassOperation
    {
        public static ExecuteSymbol<Class> Save;
    }
}
