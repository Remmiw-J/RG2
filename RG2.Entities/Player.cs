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
    class Player : Entity
    {
        [AutoExpressionField]
        public string Name { get; set; }
        public MList<Character> Characters { get; set; } = new MList<Character>();
        public int MyProperty { get; set; }
        public DateTime JoinedOn { get; set; } = TimeZoneManager.Now;
        public int Attendance { get; set; }





    }

    [AutoInit]
    public static class PlayerOperation
    {
        public static ExecuteSymbol<Item> Save;
    }
}
