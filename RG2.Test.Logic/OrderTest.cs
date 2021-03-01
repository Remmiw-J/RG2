﻿using System;
using System.Linq;
using Signum.Engine;
using Signum.Engine.Authorization;
using Signum.Engine.Operations;
using Signum.Utilities;
using RG2.Entities;
using RG2.Test.Environment;
using Xunit;

namespace RG2.Test.Logic
{
    public class OrderTest
    {
        public OrderTest()
        {
            RG2Environment.StartAndInitialize();
        }

        [Fact]
        public void OrderTestExample()
        {
            

            using (AuthLogic.UnsafeUserSession("Normal"))
            {
                using (Transaction tr = Transaction.Test()) // Transaction.Test avoids nested ForceNew transactions to be independent
                {
                    var john = Database.Query<PersonEntity>().SingleEx(p => p.FirstName == "John");

                    var order = john.ConstructFrom(OrderOperation.CreateOrderFromCustomer);

                    var sonic = Database.Query<ProductEntity>().SingleEx(p=>p.ProductName.Contains("Sonic"));

                    var line = order.AddLine(sonic);

                    order.Execute(OrderOperation.Save);

                    Assert.Equal(order.TotalPrice, sonic.UnitPrice);


                    //tr.Commit();
                }
            }
        }//OrderTestExample
    }
}
