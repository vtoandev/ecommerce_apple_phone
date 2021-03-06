using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using ecommerce_apple_phone.EF;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ecommerce_apple_phone.DAO
{
    public class PromDAO : EntityDAO<Promotion>
    {
        public PromDAO(PhoneContext context) : base(context){ }

        public object GetDetail(int id, int typeItem)
        {
            if (!CheckConnection()) return null;
            switch (typeItem)
            {
                case 1:
                    return _context.PromProducts.Find(id);
                case 2:
                    return _context.PromBills.Find(id);
                default:
                    return null;
            }
        }

        public List<Promotion> GetListBill()
        {
            if (!CheckConnection()) return null;
            DateTime date = DateTime.Now;
            return _context.Promotions.Where(item => item.Type == 2
                                    && date >= (DateTime)item.FromDate
                                    && date <= (DateTime)item.ToDate)
                                .Include(item => item.PromBill).ToList();
        }

        public List<Promotion> GetListProduct()
        {
            if (!CheckConnection()) return null;
            DateTime date = DateTime.Now;
            return _context.Promotions.Where(item => item.Type == 1
                                    && date >= (DateTime)item.FromDate
                                    && date <= (DateTime)item.ToDate)
                                .Include(item => item.PromProduct).ToList();
        }

        // public List<Promotion> GetListPoint()
        // {
        //     if (!CheckConnection()) return null;
        //     DateTime date = DateTime.Now;
        //     return _context.Promotions.Where(item => item.Type == 3
        //                             && date >= (DateTime)item.FromDate
        //                             && date <= (DateTime)item.ToDate)
        //                         .Include(item => item.PromPoint).ToList();
        // }

        public bool ChangePromProduct(int PromOld, int PromNew, int ProdId)
        {
            if (!CheckConnection()) return false;
            var OldObj = _context.PromProducts.Find(PromOld);
            var NewObj = _context.PromProducts.Find(PromNew);
            if (PromOld != 0)
            {
                if (OldObj == null) return false;
                var arOld = JsonSerializer.Deserialize<List<int>>(OldObj.ProductInProms);
                if (!arOld.Remove(ProdId)) return false;
                OldObj.ProductInProms = JsonSerializer.Serialize(arOld);
            }
            if (NewObj != null)
            {
                List<int> arNew = new List<int>();
                if (NewObj.ProductInProms != null && NewObj.ProductInProms != "")
                    arNew = JsonSerializer.Deserialize<List<int>>(NewObj.ProductInProms);
                if (!arNew.Contains(ProdId)) arNew.Add(ProdId);
                NewObj.ProductInProms = JsonSerializer.Serialize(arNew);
            }
            _context.SaveChanges();
            return true;
        }
    }
}