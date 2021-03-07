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
    public class WishItem : Entity
    {
        public Lite<Item> Item { get; set; }
        public int PriorityNumber { get; set; }
        public Lite<Player> OfPlayer  { get; set; }

    }

    [AutoInit]
    public static class WishItemOperation
    {
        public static ExecuteSymbol<WishItem> Save;
    }
}
