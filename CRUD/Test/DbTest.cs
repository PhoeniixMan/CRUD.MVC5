using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUD.Db.Context;
using CRUD.Models;
using NUnit.Framework;

namespace CRUD.Test
{
    [TestFixture]
    public class DbTest
    {
        [Test]
        public void Create()
        {
            try
            {
                var db = new CrudContext();
                var employees = db.Set<Employee>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}