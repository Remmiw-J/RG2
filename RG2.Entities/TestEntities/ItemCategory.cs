using Signum.Entities;
using Signum.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RG2.Entities
{
    [Serializable, EntityKind(EntityKind.Main, EntityData.Master)]
    public class ItemCategory : Entity
    {
        public string Name { get; set; }

        [AutoExpressionField]
        public override string ToString() => As.Expression(() => Name);

        public Lite<Item>? Item { get; set; }

    }

    [AutoInit]
    public static class ItemCategoryOperation
    {
        public static ExecuteSymbol<ItemCategory> Save;
    }
}
