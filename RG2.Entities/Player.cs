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
        public string Name { get; set; }




    }
}
