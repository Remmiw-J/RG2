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
    public class Character : Entity
    {
        [StringLengthValidator(Min = 2, Max = 12)]
        public string Name { get; set; }

        [AutoExpressionField]
        public override string ToString() => As.Expression(() => Name);
        public Race Race { get; set; }
        public Lite<Class> Class { get; set; }
        public Lite<Spec> Spec { get; set; }
        public Lite<Player>? Player { get; set; }
    }

    public enum Race
    {
        Tauren,
        Orc,
        Troll,
        Undead,
        Bloodelf
    }

    [AutoInit]
    public static class CharacterOperation
    {
        public static ExecuteSymbol<Character> Save;
        public static DeleteSymbol<Character> Delete;
    }
}
