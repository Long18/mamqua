using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMamQua.EF;

namespace ProjectMamQua.Dao
{
   
    public class SlideTypeDAO
    {
        MamQuaDbContext db =null;

        public SlideTypeDAO()
        {
            db = new MamQuaDbContext();
        }

        public IEnumerable<SlideType> GetAll()
        {
            IQueryable<SlideType> list = db.SlideTypes.OrderByDescending(x => x.ID);
            return list;
        }

    }
}