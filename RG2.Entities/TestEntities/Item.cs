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
    public class Item : Entity
    {
        public DateTime CreatedOn { get; set; } = TimeZoneManager.Now;
        public string? Description { get; set; }
        public string Name { get; set; }

        [AutoExpressionField]
        public override string ToString() => As.Expression(() => Name);

        public MList<ItemCategory> ItemCatagories { get; set; } = new MList<ItemCategory>();


    }

    [AutoInit]
    public static class ItemOperation
    {
        public static ExecuteSymbol<Item> Save;
    }

}
